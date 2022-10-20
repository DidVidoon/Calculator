using System.Text;
using Calculator.Interfaces;

namespace Calculator
{
    public class Action
    {
        public string Message { get; private set; }
        public Action(string message)
        {
            Message = message;
        }
    }

    public class InputAndOutput : IInputAndOutput
    {
        public event Action<string> Message;

        public bool RequestToReadFromFile()
        {
            Message?.Invoke("\nTo read data from a file press 'F', to enter an expression into the console press 'C': ");

            ConsoleKey pressedButton = Console.ReadKey().Key;

            if (pressedButton == ConsoleKey.F)
            {
                return true;
            }
            else if (pressedButton == ConsoleKey.C)
            {
                return false;
            }
            else
                return RequestToReadFromFile();
        }

        public string InputPathToFile()
        {
            string pathToFile = "";
            bool IsPathToFileCorrected = false;

            while (IsPathToFileCorrected == false)
            {
                try
                {
                    pathToFile = CheckInputPathToFile();
                    IsPathToFileCorrected = true;
                }
                catch (FileNotFoundException e)
                {
                    Message?.Invoke(e.Message);
                }
            }

            return pathToFile;
        }

        public string CheckInputPathToFile()
        {
            Message?.Invoke("\nEnter the path to file: ");

            var pathToFile = Console.ReadLine();

            if (!File.Exists(pathToFile))
                throw new FileNotFoundException();

            return pathToFile;
        }

        public string OutputPathtoFile()
        {
            string pathToFile = "";
            bool IsPathToNewFileCorrected = false;

            while (IsPathToNewFileCorrected == false)
            {
                try
                {
                    pathToFile = CheckOutputPathtoFile();
                    IsPathToNewFileCorrected = true;
                }
                catch (Exception e)
                {
                    Message?.Invoke(e.Message);
                }
            }

            return pathToFile;
        }

        public string CheckOutputPathtoFile()
        {
            Message?.Invoke("Enter the path to create file with answer: ");

            var pathToNewFile = Console.ReadLine();

            if ((pathToNewFile == null) || (pathToNewFile.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                throw new Exception();

            StreamWriter ReformatedFile = new StreamWriter($"{pathToNewFile}" + @"\FileWithAnswer.txt", false, Encoding.Default);
            ReformatedFile.Close();

            return pathToNewFile;
        }

        public List<string> ReadFile(string pathToFile)
        {
            using StreamReader inputFile = new StreamReader(pathToFile, Encoding.Default);
            List<string> arrayCharsOfLines = new List<string>();

            while (!inputFile.EndOfStream)
            {
                arrayCharsOfLines.Add(inputFile.ReadLine());
            }

            return arrayCharsOfLines;
        }

        public void WriteAnswerToNewFile(string mathExpressionWithAnswer, string pathToNewFile)
        {
            using (StreamWriter ReformatedFile = new StreamWriter($"{pathToNewFile}" + @"\FileWithAnswer.txt", true, Encoding.Default))
            {
                ReformatedFile.WriteLineAsync(mathExpressionWithAnswer);
            }
        }

        public char[] InputLineToChars()
        {
            Message?.Invoke("\nEnter math expression: ");
            char[] LineInChars = Console.ReadLine().ToCharArray();
            return LineInChars;
        }

        public void WhenOperationDone(IInputAndOutput.OperationDoneFrom caseName)
        {
            switch (caseName)
            {
                case IInputAndOutput.OperationDoneFrom.File: Message?.Invoke("The answer is written to the file.\n"); break;
                case IInputAndOutput.OperationDoneFrom.Console: Message?.Invoke("Answer: "); break;
            }
        }
    }
}
