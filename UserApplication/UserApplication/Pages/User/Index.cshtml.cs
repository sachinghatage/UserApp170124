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


        [BindProperty]   //instead of using this property we can create only object,but to simplify we have added
        public Users user { get; set; } = new Users();
        private readonly IConfiguration configuration;

        public void OnGet()
        {
        }

        public IActionResult OnPost()             //return type can be IActionresult but here void can be used,both will redirect default to same page
        {
            try
            {
                user.Name = Request.Form["Name"];
                user.Email = Request.Form["Email"];
                user.Phone =Convert.ToInt32( Request.Form["Phone"]);

                IFormFile file = Request.Form.Files["FileContent"];
                                
                    using(var memoryStream=new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        user.FileContent = memoryStream.ToArray();
                    }
                

            
                DataAccessLayer dal= new DataAccessLayer();
                dal.Saveuser(user, configuration);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Page();

        }

       
    }
}
