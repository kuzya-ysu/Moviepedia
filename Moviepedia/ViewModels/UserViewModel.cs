using Microsoft.AspNetCore.Identity;
namespace Moviepedia.ViewModels
{
    public class UserViewModel
    {
        public IdentityUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
