using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using UserApplication.Model;

namespace UserApplication.Pages.User
{
    public class SortingModel : PageModel
    {
        private readonly IConfiguration configuration;

        public SortingModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<Users> users { get; set; }
      

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }


        [BindProperty(SupportsGet =true)]
        public string SortBy { get; set; }


        
        public List<SelectListItem> SortOrderOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem("Ascending", "asc"),
            new SelectListItem("Descending", "desc")
        };

        public List<SelectListItem> SortByOptions { get; } = new List<SelectListItem> 
        { 
            new SelectListItem("ID","Id"),
            new SelectListItem("Name","Name"),
            new SelectListItem("Phone","Phone")
        
        };
        public void OnGet()
        {
            DataAccessLayer dal = new DataAccessLayer();
            users = dal.GetUsers(configuration);

            if (SortOrder == "asc")
            {
                switch (SortBy) 
                {
                    case "Id":
                        users=users.OrderBy(s => s.Id).ToList();
                        break;

                    case "Name":
                        users=users.OrderBy(s => s.Name,StringComparer.OrdinalIgnoreCase).ToList();
                        break;

                    case "Phone":
                        users=users.OrderBy(s=>s.Phone).ToList();
                        break;
                }
                
            }
            else if (SortOrder == "desc")  
            {
                switch(SortBy)
                {
                    case "Id":
                        users=users.OrderByDescending(s => s.Id).ToList();
                        break;

                    case "Name":
                        users=users.OrderByDescending(s => s.Name,StringComparer.OrdinalIgnoreCase).ToList();
                        break;


                    case "Phone":
                        users=users.OrderByDescending(s => s.Phone).ToList();
                        break;
                }
            }
        }
    }
}
