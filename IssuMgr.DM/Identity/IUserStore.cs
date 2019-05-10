using System.Threading;
using System.Threading.Tasks;
using IssuMgr.Model.Models;
using Microsoft.AspNetCore.Identity;

namespace IssuMgr.Web.Identity {
    public interface IUserStore {
        Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken);
        void Dispose();
        Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken);
        Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken);
        Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken);
        Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken);
        Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken);
        Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken);
        Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken);
        Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken);
        Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken);
    }
}