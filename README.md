Math-Eval [![NuGet Version](https://buildstats.info/nuget/Math-Eval)](https://www.nuget.org/packages/Math-Eval/)
===============

A .NET library for parsing and evaluating mathematical expressions with support for user-defined functions, operators and number constants.

## Installation

Easily done using the dotnet CLI.

### Via Nuget

```
dotnet add package Math-Eval
```

### Build from source

Build the project

```
dotnet publish --output <output-path>
```

Then reference the dll in your project's .csproj file

```html
<ItemGroup>
    <Reference Include="DllFile">
      <HintPath>Path\To\DllFile.dll</HintPath>
    </Reference>
</ItemGroup>
```

## Usage

```C#
using MathEval;

// Standard mathematical operations
var addition = new Operator('+', (a, b) => a + b, 0);
var subtraction = new Operator('-', (a, b) => a - b, 0);
var multiplication = new Operator('*', (a, b) => a * b, 1);
var division = new Operator('/', (a, b) => {

    if (b == 0) {
        throw new Exception("Division by zero is not allowed");
    }

    return a / b;
}, 1);

// Natural logarithm
var ln = new Function("ln", arguments => {

    if (arguments.Count() != 1) {
        throw new Exception("The natural logarithm takes exactly 1 argument");
    }

    var argument = arguments.First();

    if (argument <= 0) {
        throw new Exception("The natural logarithm only takes strictly positive numbers");
    }

    return Math.Log(argument);
});

// Example of a multi-variable function
var mv = new Function("mv", arguments => {

    if (arguments.Count() != 2) {
        throw new Exception("'mv' takes exactly 2 arguments");
    }

    return arguments[0] + arguments[1];
});

// Example of a number constant
var pi = new NumberConstant('π', Math.PI);

var lexer = new Lexer();

var parser = new Parser(
    new[] { ln, mv },
    new[] { addition, subtraction, multiplication, division },
    new[] { pi }
);

var calculator = new Calculator(lexer, parser);

Console.WriteLine(calculator.GetResult("1+ln(π+2)/(mv(3,4)*5)")); // Outputs 1.0467817967501714
```
