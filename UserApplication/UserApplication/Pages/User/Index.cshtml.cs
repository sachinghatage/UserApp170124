using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApplication.Model;

namespace UserApplication.Pages.User
{
    public class IndexModel : PageModel
    {

        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Users user = new Users();
        private readonly IConfiguration configuration;

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            user.Name = Request.Form["Name"];
            user.Email = Request.Form["Email"];
            user.Phone =Convert.ToInt32( Request.Form["Phone"]);

            try
            {
                DataAccessLayer dal= new DataAccessLayer();
                dal.Saveuser(user, configuration);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
