using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eAgenda.WebApp.ActionFilters
{
    public class LogarAcaoAttribute : ActionFilterAttribute
    {
        private readonly ILogger<LogarAcaoAttribute> logger;

        public LogarAcaoAttribute(ILogger<LogarAcaoAttribute> logger)
        {
            this.logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var resultado = context.Result;

            if(resultado is ViewResult viewResult && viewResult.Model is not null)
            {
                logger.LogInformation(
                    "Açãp de endpoint execultada com sucesso! {@Modelo}", viewResult.Model);
            }

            base.OnActionExecuted(context);
        }
    }
}
