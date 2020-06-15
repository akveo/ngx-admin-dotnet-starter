/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Common.WebApi.Filters
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILogger _logger;
        private ILogger Logger => _logger ?? (_logger =
                                      (ILogger) GlobalConfiguration.Configuration.DependencyResolver.GetService(
                                          typeof(ILogger)));

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var exception = actionExecutedContext.Exception;
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
#if DEBUG
                Content = new StringContent($"Internal Api Error: {exception.Message}"),
#else
                Content = new StringContent($"An error occurred, please try again or contact the administrator."),
#endif
            };
            
            Logger.LogError(exception.Message, HttpContext.Current.User.Identity.GetUserId<int>(), exception);

            throw new HttpResponseException(response);
        }
    }
}
