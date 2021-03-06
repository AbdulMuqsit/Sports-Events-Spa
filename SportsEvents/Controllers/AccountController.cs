﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using SportsEvents.Common.Entities;
using SportsEvents.EntityFramework;
using SportsEvents.Infrastructure;
using SportsEvents.Models;
using SportsEvents.Providers;
using SportsEvents.Results;

namespace SportsEvents.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; }
        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [Route("UpdateUser")]
        public async Task<IHttpActionResult> UpdateUserAsync(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await UserManager.FindByNameAsync(User.Identity.Name).ConfigureAwait(false);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.Phone;

            City city = null;
            if (model.CityId != null)
            {
                city = await DbContext.Cities.FindAsync(model.CityId.Value).ConfigureAwait(false);
            }
            user.Address = new Address
            {
                LineOne = model.LineOne,
                LineTwo = model.LineTwo,
                CityId = city?.Id ?? 0,
                CityName = city?.Name,
                CountryName = city?.CountryName,
                Zip = model.Zip,
                State = model.State,
            };
            var result = await UserManager.UpdateAsync(user).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        [Route("UpdateOrganizer")]
        public async Task<IHttpActionResult> UpdateOrganizerAsync(UpdateOrganizerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await UserManager.FindByNameAsync(User.Identity.Name).ConfigureAwait(false);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.Phone;
            user.Email = model.Email;

            City city = null;
            var contactCity = await DbContext.Cities.FindAsync(model.ContactCityId).ConfigureAwait(false);

            if (model.CityId != null)
            {
                city = await DbContext.Cities.FindAsync(model.CityId.Value).ConfigureAwait(false);
            }
            user.Address = new Address
            {
                LineOne = model.LineOne,
                LineTwo = model.LineTwo,
                CityId = city?.Id ?? 0,
                CityName = city?.Name,
                Zip = model.Zip,
                State = model.State,
                CountryName = city?.CountryName
            };

            //Specific to organizer
            user.OrganiztionName = model.OrganizationName;
            user.OrganizationDecription = model.OrganizationDecription;
            user.OrganaiztionLogo = model.OrganaiztionLogo;
            user.Link = model.Link;

            user.ContactDetails = new ContactDetails
            {
                BillingAddress =
                    new Address
                    {
                        CityId = model.ContactCityId,
                        CityName = contactCity.Name,
                        CountryName = contactCity.CountryName,
                        LineOne = model.ContactLineOne,
                        LineTwo = model.ContactLineTwo,
                        State = model.ContactState,
                        Zip = model.ContactZip
                    },
                Email = model.ContactEmail,
                FirstName = model.ContactFirstName,
                LastName = model.ContactLastName,
                Phone = model.ContactPhone
            };

            var result = await UserManager.UpdateAsync(user).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("RegisterOrganizer")]
        public async Task<IHttpActionResult> RegisterOrganizerAsync(RegisterOrganizerViewModel model)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            City city = null;
            var contactCity = await DbContext.Cities.FindAsync(model.ContactCityId).ConfigureAwait(false);

            var user = await UserManager.FindByNameAsync(User.Identity.Name).ConfigureAwait(false);
            var identity = await UserManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType).ConfigureAwait(false);

            if (identity.HasClaim(ClaimTypes.Role, "Organizer"))
            {
                return Conflict();
            }
            if (model.CityId != null)
            {
                city = await DbContext.Cities.FindAsync(model.CityId.Value).ConfigureAwait(false);
            }


            user.OrganiztionName = model.OrganizationName;
            user.OrganizationDecription = model.OrganizationDecription;

            user.Address = new Address
            {
                LineOne = model.LineOne,
                LineTwo = model.LineTwo,
                CityId = city?.Id ?? 0,
                CityName = city?.Name,
                Zip = model.Zip,
                CountryName = city?.CountryName
            };
            user.Link = model.Link;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.Phone;


            user.ContactDetails = new ContactDetails
            {
                BillingAddress =
                    new Address
                    {
                        CityId = model.ContactCityId,
                        CityName = contactCity.Name,
                        CountryName = contactCity.CountryName,
                        LineOne = model.ContactLineOne,
                        LineTwo = model.ContactLineTwo,
                        Zip = model.ContactZip
                    },
                Email = model.ConatactEmail,
                FirstName = model.ContactFirstName,
                LastName = model.ContactLastName,
                Phone = model.ContactPhone
            };


            var result = await UserManager.UpdateAsync(user).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            var claimResult = await UserManager.AddClaimAsync(User.Identity.GetUserId(), new Claim(ClaimTypes.Role, "Organizer")).ConfigureAwait(false);
            if (!claimResult.Succeeded)
            {
                return GetErrorResult(claimResult);
            }
            return Ok(user);
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfoAsync(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId()).ConfigureAwait(false);

            if (user == null)
            {
                return null;
            }

            var logins = new List<UserLoginInfoViewModel>();

            foreach (var linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePasswordAsync(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPasswordAsync(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLoginAsync(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            var ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                                                              && ticket.Properties.ExpiresUtc.HasValue
                                                              &&
                                                              ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            var externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey)).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLoginAsync(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId()).ConfigureAwait(false);
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey)).ConfigureAwait(false);
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLoginAsync(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            var user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey)).ConfigureAwait(false);

            var hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType).ConfigureAwait(false);
                var cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType).ConfigureAwait(false);

                var properties = ApplicationOAuthProvider.CreateProperties(user, user.Roles.Select(r => new RoleModel() { Role = r.RoleId }).ToList());
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                var identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            var descriptions = Authentication.GetExternalAuthenticationTypes();
            var logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (var description in descriptions)
            {
                var login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Address = new Address(),
                ContactDetails = new ContactDetails() { BillingAddress = new Address() }
            };

            var result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            var claimResult = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Visitor"));
            if (!claimResult.Succeeded)
            {
                return GetErrorResult(claimResult);
            }
            return Ok(user);
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new User { UserName = model.Email, Email = model.Email };

            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                var providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || string.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || string.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static readonly RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                var strengthInBytes = strengthInBits / bitsPerByte;

                var data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}