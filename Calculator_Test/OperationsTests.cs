using Calculator;
using Calculator.Exeptions;

namespace Calculator_Test
{
    [TestClass]
    public class OperationsTests
    {
        [TestMethod]
        [DataRow(data1: "1 + 2", moreData: 3)]
        [DataRow(data1: "1 + 2*2", moreData: 5)]
        [DataRow(data1: "10/5 + 2*2", moreData: 6)]
        [DataRow(data1: "1 + (4 + 2) * 2", moreData: 13)]
        [DataRow(data1: "1 + 2 * (3 + 4 / 2 - (1 + 2)) * 2 + 1", moreData: 10)]
        [DataRow(data1: "12 + 2 * (35 + 400 / 20 - (121 + 23)) * 2 + 122", moreData: -222)]
        public void Calculator_InputExpression_OutputAnswer(string expression, double expected)
        {
            char[] input = expression.ToCharArray();
            double actual = Operations.Calculator(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ErrorInExpression), "Error in expression")]
        public void Calculator_InputExpressionWithException_ExceptionMessage()
        {
            string expression = "1+x+1";
            char[] input = expression.ToCharArray();
            Operations.Calculator(input);
        }

        [TestMethod]
        [ExpectedException(typeof(DivisionByZeroException), "Cannot be divided by zero")]
        public void Calculator_InputExpressionWithdivisionByZero_ExceptionMessage()
        {
            string expression = "1/0";
            char[] input = expression.ToCharArray();
            Operations.Calculator(input);
        }
    }
}