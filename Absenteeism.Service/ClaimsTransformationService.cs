using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Absenteeism.Service
{
    public class ClaimsTransformationService : IClaimsTransformation
    {
        private readonly MasterService _masterService;

        public ClaimsTransformationService(MasterService masterService)
        {
            _masterService = masterService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated != true)
            {
                return principal;
            }

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "UserNumber")?.Value;
            var roles = await _masterService.GetUser(userId);

            if (roles == null)
            {
                return principal;
            }

            if (!principal.HasClaim(ClaimTypes.Role, roles.Role))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, roles.Role));
            }
            return principal;
        }
    }
}
