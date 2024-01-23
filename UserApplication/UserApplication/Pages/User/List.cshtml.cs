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
        public void OnGet(int? page)     //int? means that the parameter page can either be a valid integer or null
        {
            DataAccessLayer dal = new DataAccessLayer();
                  
            users = dal.GetUsers(configuration);
        }
    }



}
