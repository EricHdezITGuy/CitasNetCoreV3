using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Error capturado por el filtro de excepciones global.");
        context.Result = new ViewResult
        {
            ViewName = "Error"
        };
    }
}