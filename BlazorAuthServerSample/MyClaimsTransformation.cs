
    using Microsoft.AspNetCore.Authentication;
    using System.Security.Claims;

namespace BlazorAuthServerSample
{
    public class MyClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
           
            // get user name 
           var userName = principal.Identity?.Name;
           

            if (principal.Identity is ClaimsIdentity claimsIdentity)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "AdminRole"));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Role1"));
           }

            return Task.FromResult(principal);
        }
    }
}
