using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace App
{
    public sealed class PerformanceMetricsCollector : IHostedService
    {
        private readonly IProcessorMetricsCollector _processorMetricsCollector;
        private readonly IMemoryMetricsCollector _memoryMetricsCollector;
        private readonly INetworkMetricsCollector _networkMetricsCollector;
        private readonly IMetricsDeliverer _MetricsDeliverer;
        private IDisposable _scheduler;

        /// <summary>
        /// 构造函数（采用构造函数注入的方式）
        /// </summary>
        /// <param name="processorMetricsCollector"></param>
        /// <param name="memoryMetricsCollector"></param>
        /// <param name="networkMetricsCollector"></param>
        /// <param name="MetricsDeliverer"></param>
        public PerformanceMetricsCollector(
            IProcessorMetricsCollector processorMetricsCollector,
            IMemoryMetricsCollector memoryMetricsCollector,
            INetworkMetricsCollector networkMetricsCollector,
            IMetricsDeliverer MetricsDeliverer)
        {
            _processorMetricsCollector = processorMetricsCollector;
            _memoryMetricsCollector = memoryMetricsCollector;
            _networkMetricsCollector = networkMetricsCollector;
            _MetricsDeliverer = MetricsDeliverer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;

            async void Callback(object state)
            {
                // 和S1001相比，进行了服务功能的拆分，同时进行了接口化抽象
                var counter = new PerformanceMetrics
                {
                    Processor = _processorMetricsCollector.GetUsage(),
                    Memory = _memoryMetricsCollector.GetUsage(),
                    Network = _networkMetricsCollector.GetThroughput()
                };
                // 和S1001相比，不直接输出，而是将输出的操作也接口化独立出来
                await _MetricsDeliverer.DeliverAsync(counter);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _scheduler?.Dispose();
            return Task.CompletedTask;
        }
    }
}
