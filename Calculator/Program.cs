using Calculator.Interfaces;
using Calculator;
using Microsoft.Extensions.DependencyInjection;

var InputAndOutputContainer = new ServiceCollection()
    .AddSingleton<IInputAndOutput, InputAndOutput>()
    .AddSingleton<LogicCalculator>()
    .BuildServiceProvider();

var logicCalculator = InputAndOutputContainer.GetService<LogicCalculator>();

InputAndOutputContainer.GetService<IInputAndOutput>().Message += SendMessage.Message;

while (true)
{
    logicCalculator.CalculatorLogic();
}