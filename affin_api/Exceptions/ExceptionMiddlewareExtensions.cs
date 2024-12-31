using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using utils_library.CustomConstants;
using utils_library.Exceptions;
using utils_library.Responses;

namespace affin_api.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is CustomException)
                        {
                            var customException = (CustomException)contextFeature.Error;
                            var response = JsonSerializer.Serialize(new SimpleResponse()
                            {
                                Code = customException.Code,
                                BusinessMeaning = customException.BusinessMeaning,
                            });

                            await context.Response.WriteAsync(response);
                        }
                        else
                        {
                            var response = JsonSerializer.Serialize(new SimpleResponse()
                            {
                                Code = (int)RESPONSE.UNKNOW_ERROR,
                                BusinessMeaning = CustomConstants.UNKNOW_ERROR
                            });

                            await context.Response.WriteAsync(response);
                        }
                    }
                    else
                    {
                        var response = JsonSerializer.Serialize(new SimpleResponse()
                        {
                            Code = (int)RESPONSE.UNKNOW_ERROR_BY_ERROR_HANDLER,
                            BusinessMeaning = CustomConstants.UNKNOW_ERROR_BY_ERROR_HANDLER
                        });

                        await context.Response.WriteAsync(response);
                    }
                });
            });
        }
    }
}
