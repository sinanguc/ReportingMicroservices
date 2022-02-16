using Common.Dto.Shared;
using Common.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Common.Dto.Shared.GenericResult.WithValidationErrorMessage;

namespace Common.Filters.Validation
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var errorsInModelState = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            GenericResult.WithValidationErrorMessage errorReponse = new GenericResult.WithValidationErrorMessage();

            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {
                    ValidationErrorModel errorModel = new ValidationErrorModel
                    {
                        FieldName = error.Key,
                        Message = subError
                    };

                    errorReponse.ValidationErrors.Add(errorModel);
                }
            }

            errorReponse.Message = GenericMessages.Please_Fill_In_All_Required_Fields;
            errorReponse.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
            context.HttpContext.Response.StatusCode = errorReponse.StatusCode;
            context.Result = new JsonResult(errorReponse) { StatusCode = HttpStatusCode.BadRequest.GetHashCode() };
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
                await next();

            var errorsInModelState = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            GenericResult.WithValidationErrorMessage errorReponse = new GenericResult.WithValidationErrorMessage();

            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {
                    ValidationErrorModel errorModel = new ValidationErrorModel
                    {
                        FieldName = error.Key,
                        Message = subError
                    };

                    errorReponse.ValidationErrors.Add(errorModel);
                }
            }

            errorReponse.Message = GenericMessages.Please_Fill_In_All_Required_Fields;
            errorReponse.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
            context.HttpContext.Response.StatusCode = errorReponse.StatusCode;
            context.Result = new JsonResult(errorReponse) { StatusCode = HttpStatusCode.BadRequest.GetHashCode() };
        }
    }
}
