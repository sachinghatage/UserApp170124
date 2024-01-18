using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApplication.Model;

namespace UserApplication.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration configuration;
        public Users user = new Users();
        public EditModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnGet()
        {
            int id =Convert.ToInt32( Request.Query["id"]);
            DataAccessLayer dal = new DataAccessLayer();
            user=dal.GetUser(id,configuration);
        }

        public void OnPost() {
            user.Name = Request.Form["name"];
            user.Email = Request.Form["Email"];
            user.Phone =Convert.ToInt32( Request.Form["Phone"]);

            DataAccessLayer dal=new DataAccessLayer();
            dal.UpdateUser(user,configuration);

            Response.Redirect("/User/list");

        }
    }
}
