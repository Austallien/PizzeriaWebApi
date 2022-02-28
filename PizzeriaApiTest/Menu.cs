using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaApiTest
{
    class Menu
    {
        public static void Controller()
        {
            int code = -1;
            while (code != 0)
            {
                Display();
                code = Read();
                Excecute(code);
            //Console.ReadKey();
            }
        }

        private static void Display()
        {
            Console.WriteLine("**Menu**");
            Console.WriteLine("1: OrderList");
            Console.WriteLine("2: OrderItem");
            Console.WriteLine("");
            Console.WriteLine("0: Exit");
        }

        private static int Read()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception ex)
            {
                Console.WriteLine("Введено неверное значение");
                return -1;
            }
        }

        private static bool Excecute(int code)
        {
            try
            {
                switch (code)
                {
                    case 1:
                        Console.WriteLine(Operation.GetOrderList());
                        return true;
                    case 2:
                        Console.Write("id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(Operation.GetOrderItem(id));
                        return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return false;
        }
    }
}
