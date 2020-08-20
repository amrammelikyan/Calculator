using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input Example: 5,5 * (((3 + 2) * (2 + 2)) + 4)");
            Console.Write("Input the operetion: ");
            string text = Console.ReadLine();

            Console.Write("Result: ");
            Console.WriteLine(GetResult(text));

            Console.ReadKey();
        }

        private static List<string> OperationToList(string text)
        {
            var list = new List<string>();
            string num = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]) || text[i] == ',')
                {
                    num = num + text[i];
                    if (i == text.Length - 1 || (!Char.IsDigit(text[i + 1]) && text[i + 1] != ','))
                    {
                        list.Add(num);
                        num = string.Empty;
                    }
                }
                else if (IsOperator(text[i]))
                {
                    list.Add(text[i].ToString());
                }
            }

            return list;
        }

        private static string Calculate(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == "(")
                {
                    list = CalculateInBrackets(list, i);
                    i--;
                }
                if (MulOrDiv(list[i]) && Double.TryParse(list[i + 1], out double resalt2))
                {
                    HelperOperation(list, i, resalt2);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (AddOrSub(list[i]) && Double.TryParse(list[i + 1], out double resalt2))
                {
                    HelperOperation(list, i, resalt2);
                }
            }

            return list[0];
        }

        private static List<string> CalculateInBrackets(List<string> list, int index = 0)
        {
            for (int i = index + 1; i < list.Count; i++)
            {
                if (list[i] == "(")
                {
                    list = CalculateInBrackets(list, i);
                    i = index;
                }
                else if (list[i] == ")") break;

               if (MulOrDiv(list[i]) && Double.TryParse(list[i + 1], out double resalt2))
                {
                    HelperOperation(list, i, resalt2);
                }
            }

            for (int i = index + 1; i < list.Count; i++)
            {
                if (list[i] == ")") break;

                if (AddOrSub(list[i]) && Double.TryParse(list[i + 1], out double resalt2))
                {
                    HelperOperation(list, i, resalt2);
                }
            }

            list.RemoveAt(index);
            list.RemoveAt(index + 1);
            return list;
        }

        private static void HelperOperation(List<string> list, int i, double resalt2)
        {
            Double.TryParse(list[i - 1], out double resalt1);
            list[i - 1] = DoOperation(resalt1, list[i], resalt2).ToString();
            list.RemoveRange(i, 2);
            i--;
        }

        private static string GetResult(string text)
        {
            return Calculate(OperationToList(text));
        }

        private static bool MulOrDiv(string symbol)
        {
            switch (symbol)
            {
                case "*": return true;
                case "/": return true;
                default: return default;
            }
        }

        private static bool AddOrSub(string symbol)
        {
            switch (symbol)
            {
                case "+": return true;
                case "-": return true;
                default: return default;
            }
        }

        private static bool IsOperator(char symbol)
        {
            var operators = new List<char> { '+', '-', '*', '/', '(', ')' };
            if (operators.Contains(symbol)) return true;
            return false;
        }

        private static double DoOperation(double num1, string symbol, double num2)
        {
            switch (symbol)
            {
                case "+": return num1 + num2;
                case "-": return num1 - num2;
                case "*": return num1 * num2;
                case "/": return num1 / num2;
                default: return 0;
            }
        }
    }
}
