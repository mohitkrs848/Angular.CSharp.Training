using Angular.CSharp.Training.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

public class AuthorizeRoleAttribute : AuthorizationFilterAttribute
{
    private readonly string[] _roles;

    public AuthorizeRoleAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public override void OnAuthorization(HttpActionContext actionContext)
    {
        var user = (User)actionContext.RequestContext.Principal.Identity;
        if (user == null || !_roles.Contains(user.Role))
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
        }
    }
}