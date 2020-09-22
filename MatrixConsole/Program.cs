using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixConsole
{
    class Program
    {
        static object locker = new object();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            for (int i = 0; i < Console.WindowWidth - 2; i += 5)
            {
                Thread thread1 = new Thread(new ParameterizedThreadStart(makeSimbols));
                thread1.Start(i);
            }
        }

        public static void makeSimbols(object column)
        {
            Thread.Sleep(300);
            Random rand = new Random();
            string symbols = "!@#$%^&*()1234567890_+ABCDEFGHJKLOP:";
            int row = 0;
            int col = (int)column;
            while (true)
            {
                lock (locker)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(col, row);
                    Console.Write(symbols[rand.Next(symbols.Length)]);
                    if (row < Console.WindowHeight - 2) row++;
                    else
                    {
                        row = 0;
                        for (int i = 0; i < Console.WindowHeight; i++)
                        {
                            Console.SetCursorPosition(col, i);
                            Console.Write(" ");
                            Console.SetCursorPosition(col + 1, i);
                            Console.Write(" ");
                            if (col - 1 >= 0)
                            {
                                Console.SetCursorPosition(col - 1, i);
                                Console.Write(" ");
                            }
                        }
                    }
                }

                Thread.Sleep(rand.Next(7) * 100);
            }
        }
    }
}
