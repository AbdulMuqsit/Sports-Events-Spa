using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using SportsEvents.Common.Entities;
using SportsEvents.EntityFramework;

namespace SportsEvents.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                OAuthDefaults.AuthenticationType);
            var cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);
            var roles = oAuthIdentity.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(e => new RoleModel {Role = e.Value}).ToList();
            var properties = CreateProperties(user, roles);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                var expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(User user, IList<RoleModel> roles)
        {
            var rolesString = string.Empty;
            if (roles != null)
            {
                for (var i = 0; i < roles.Count; i++)
                {
                    rolesString += roles[i].Role;
                    if (roles.Count - 1 != i)
                    {
                        rolesString += ", ";
                    }
                }
            }

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userName", user.UserName},
                {"email", user.Email},
                {"roles", rolesString},
             
            };
            return new AuthenticationProperties(data);
        }
    }

    [DataContract]
    public class RoleModel
    {
        [DataMember(Name = "Role")]
        public string Role { get; set; }
    }
}