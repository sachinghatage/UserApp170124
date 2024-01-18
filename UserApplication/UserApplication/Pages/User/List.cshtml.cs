using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApplication.Model;
    
namespace UserApplication.Pages.User
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration configuration;
        public List<Users> users = new List<Users>();
        public ListModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnGet()
        {
            DataAccessLayer dal = new DataAccessLayer();
            users=dal.GetUsers(configuration);
        }
    }
}
