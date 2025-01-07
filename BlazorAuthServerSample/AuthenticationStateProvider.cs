
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly NavigationManager _navigationManager;

    public MyAuthenticationStateProvider(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }

    //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //{
    //    var principal = new ClaimsPrincipal();

    //    // Simulate getting the authentication state from an external source
    //    var isAuthenticated = true; // Replace with actual authentication check
    //    var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name, "username") // Replace with actual user information
    //    };

    //    if (isAuthenticated)
    //    {
    //        claims.Add(new Claim(ClaimTypes.Role, "AdminRole"));
    //        claims.Add(new Claim(ClaimTypes.Role, "Role1"));
    //        var claimsIdentity = new ClaimsIdentity(claims, "apiauth_type");
    //        principal = new ClaimsPrincipal(claimsIdentity);

    //        // Redirect to the specified URL after login
    //        _navigationManager.NavigateTo("/redirect-url"); // Replace with actual redirect URL
    //    }

    //    return await Task.FromResult(new AuthenticationState(principal));
    //}
}
