using MarketProgram.UserSide.Services;
using System.Text;

namespace MarketProgram.UserSide
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            ControlPanel panel = new();

            panel.Start();
        }
    }
}
