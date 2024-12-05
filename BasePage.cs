using System.Security.Claims;
using Microsoft.AspNetCore.Components;

public class BasePage : ComponentBase
{
    [Inject]
    protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    protected string _authMessage;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            _authMessage = $"{user.Identity.Name} is authenticated.";

            // Add custom claim here
            var claimsIdentity = (ClaimsIdentity)user.Identity;
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "AdminRole"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Role1"));
        }
    }
}
