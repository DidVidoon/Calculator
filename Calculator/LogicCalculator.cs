using Calculator.Interfaces;

namespace Calculator
{
    public class LogicCalculator
    {
        private readonly IInputAndOutput _inputAndOutput;

        public LogicCalculator(IInputAndOutput inputAndOutput)
        {
            _inputAndOutput = inputAndOutput;
        }

        public void CalculatorLogic()
        {
            try
            {
                if (_inputAndOutput.RequestToReadFromFile())
                {
                    string pathToFile = _inputAndOutput.InputPathToFile();
                    string pathToOutputFile = _inputAndOutput.OutputPathtoFile();

                    List<string> fileInList = _inputAndOutput.ReadFile(pathToFile);

                    for (int i = 0; i < fileInList.Count; i++)
                    {
                        try
                        {
                            char[] charsOfLine = fileInList[i].ToCharArray();
                            string mathExpressionWithAnswer = fileInList[i] + "=" + Operations.Calculator(charsOfLine);
                            _inputAndOutput.WriteAnswerToNewFile(mathExpressionWithAnswer, pathToOutputFile);
                        }
                        catch (Exception e)
                        {
                            string mathExpressionWithAnswer = fileInList[i] + "=" + e.Message;
                            _inputAndOutput.WriteAnswerToNewFile(mathExpressionWithAnswer, pathToOutputFile);
                        }
                    }

                    _inputAndOutput.WhenOperationDone(IInputAndOutput.OperationDoneFrom.File);
                }
                else
                {
                    char[] charsOfLine = _inputAndOutput.InputLineToChars();
                    _inputAndOutput.WhenOperationDone(IInputAndOutput.OperationDoneFrom.Console);
                    Console.WriteLine(Operations.Calculator(charsOfLine));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
