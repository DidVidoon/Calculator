using Calculator.Exeptions;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public static class Operations
    {
        public static double Calculator(char[] charsOfLine)
        {
            Stack<char> operatorStack = new Stack<char>();
            Stack<double> numberStack = new Stack<double>();
            List<int> ElementNumToSkip = new List<int>();

            for (int i = 0; i < charsOfLine.Length; i++)
            {
                if (IsDelimeter(charsOfLine[i]) || ElementNumToSkip.Contains(i))
                    continue;

                if (Char.IsDigit(charsOfLine[i]) || charsOfLine[i] == '.')
                {
                    StringBuilder number = new StringBuilder();
                    int nextElementNumber = i + 1;
                    number.Append(charsOfLine[i]);

                    while (nextElementNumber < charsOfLine.Length && (Char.IsDigit(charsOfLine[nextElementNumber]) || charsOfLine[nextElementNumber] == '.'))
                    {
                        number.Append(charsOfLine[nextElementNumber]);
                        ElementNumToSkip.Add(nextElementNumber);
                        nextElementNumber++;
                    }

                    numberStack.Push(Convert.ToDouble(number.ToString()));
                }
                else if (IsOperator(charsOfLine[i]))
                {
                    if (operatorStack.Count == 0 || charsOfLine[i] == '(')
                    {
                        operatorStack.Push(charsOfLine[i]);
                    }
                    else if (GetPriority(operatorStack.Peek()) < GetPriority(charsOfLine[i]))
                    {
                        operatorStack.Push(charsOfLine[i]);
                    }
                    else if (charsOfLine[i] == ')')
                    {
                        while (operatorStack.Peek() != '(')
                        {
                            numberStack.Push(Calculate(numberStack, operatorStack.Pop()));
                        }

                        operatorStack.Pop();
                    }
                    else
                    {
                        while (GetPriority(operatorStack.Peek()) >= GetPriority(charsOfLine[i]) && operatorStack.Peek() != '(')
                        {
                            numberStack.Push(Calculate(numberStack, operatorStack.Pop()));

                            if (operatorStack.Count == 0)
                                break;
                        }

                        operatorStack.Push(charsOfLine[i]);
                    }
                }
                else
                {
                    throw new ErrorInExpression("Error in expression");
                }
            }

            while (operatorStack.Count != 0)
            {
                char operatorInStack = operatorStack.Pop();
                numberStack.Push(Calculate(numberStack, operatorInStack));
            }

            return (numberStack.Peek());
        }

        private static bool IsDelimeter(char c)
        {
            string s = c.ToString();
            Regex regex = new Regex(@"[\s\=]");

            return regex.IsMatch(s);
        }

        private static bool IsOperator(char c)
        {
            string s = c.ToString();
            Regex regex = new Regex(@"[\+\-\/\*\(\)]");

            return regex.IsMatch(s);
        }

        private static byte GetPriority(char s)
        {
            switch (s)
            {
                case '+':
                case '-': return 1;
                case '*':
                case '/': return 2;
                default: return 0;
            }
        }

        private static double Calculate(Stack<double> numberStack, char operatorInStack)
        {
            double result = 0;
            double a = numberStack.Pop();
            double b = numberStack.Pop();

            switch (operatorInStack)
            {
                case '+': result = b + a; break;
                case '-': result = b - a; break;
                case '*': result = b * a; break;
                case '/':
                    if (a != 0) result = b / a;
                    else throw new DivisionByZeroException("Cannot be divided by zero");
                    break;
            }

            return (result);
        }
    }
}
