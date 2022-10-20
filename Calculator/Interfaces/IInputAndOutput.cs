using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Interfaces
{
    public interface IInputAndOutput
    {
        event Action<string> Message;

        bool RequestToReadFromFile();

        string InputPathToFile();

        string CheckInputPathToFile();

        string OutputPathtoFile();

        string CheckOutputPathtoFile();

        List<string> ReadFile(string pathToFile);

        char[] InputLineToChars();

        void WriteAnswerToNewFile(string mathExpressionWithAnswer, string pathToNewFile);

        void WhenOperationDone(OperationDoneFrom caseName);

        public enum OperationDoneFrom
        {
            File,
            Console
        }
    }
}
