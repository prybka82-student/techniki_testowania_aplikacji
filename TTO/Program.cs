// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using TTO;

[assembly: InternalsVisibleTo("TTO.UnitTests")] //nazwa assembly, które ma widzieć 

var calculator = new Calculator();

Console.WriteLine(calculator.Sum(1, 2));
