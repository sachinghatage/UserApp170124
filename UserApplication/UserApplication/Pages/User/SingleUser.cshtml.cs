using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApplication.Model;

namespace UserApplication.Pages.User
{
    public class SingleUserModel : PageModel
    {
        private readonly IConfiguration configuration;
        public Users user = new Users();
        public SingleUserModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [BindProperty(SupportsGet =true)]
        public int Id { get; set; } 
        public void OnGet()
        {
            DataAccessLayer dal=new DataAccessLayer();
            user=dal.GetUser(Id,configuration);
        }
    }
}
