using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace H5_ServerProject.Services
{
    public interface IGuid
    {
        Task<Guid> GetGuidID();
    }
    
    public class GuidService : IGuid
    {
        Guid userId;
        private readonly AuthenticationStateProvider authenticationState;
        public GuidService(AuthenticationStateProvider AuthenticationStateProvider) {
            authenticationState = AuthenticationStateProvider;
        }
        public async Task<Guid> GetGuidID()
        {
            var authState = await authenticationState.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                userId =  Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            return userId;
        }

    }
}
