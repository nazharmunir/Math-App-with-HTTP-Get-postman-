using System.Diagnostics.Eventing.Reader;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        int firstNumber = 0, secondNumber = 0;
        string? operation = null;
        long? result = null;

        //reading first number
        if (context.Request.Query.ContainsKey("firstnumber"))
        {
            string firstNumberString = context.Request.Query["firstnumber"][0];
            if (!string.IsNullOrEmpty(firstNumberString))
            {
                firstNumber = int.Parse(firstNumberString);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid value for first number'\n");
            }
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid value for first number'\n");
        }
        //reading second number 
        if (context.Request.Query.ContainsKey("secondnumber"))
        {
            string secondNumberString = context.Request.Query["secondnumber"][0];
            if (!string.IsNullOrEmpty(secondNumberString))
            {
                secondNumber = int.Parse(secondNumberString);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid value for second number '\n");
            }
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid value for second number '\n");
        }
        if (context.Request.Query.ContainsKey("operation"))
        {
            
            operation = Convert.ToString(context.Request.Query["operation"][0]);
            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "add": result = firstNumber + secondNumber; break;
                    case "subtract": result = firstNumber - secondNumber; break;
                    case "multiply": result = firstNumber * secondNumber; break;
                    case "divide": result = (secondNumber != 0) ? firstNumber / secondNumber : 0; break;
                    case "mod": result = (secondNumber != 0) ? firstNumber % secondNumber : 0; break;
                }
                if (result.HasValue)
                {
                    await context.Response.WriteAsync($"{result.Value.ToString()}");
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid value for operation");
                }

            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid value for operation");
            }
        }
    }
}

);

app.Run();