using System.Data;

namespace App
{
    class Program
    {
        static void Main()
        {
            _ = new DatabaseSourceListener();// 这里不需要显式注册
            DatabaseSource.Instance.OnCommandExecute(CommandType.Text, "SELECT * FROM T_USER");
        }
    }
}
