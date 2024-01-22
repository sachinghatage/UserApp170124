using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApplication.Model;
    
namespace UserApplication.Pages.User
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration configuration;
        public List<Users> users = new List<Users>();

        //pagination properties
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }     
        public int TotalRecords { get; set; }
        public ListModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnGet(int? page)     //int? means that the parameter page can either be a valid integer or null
        {
            DataAccessLayer dal = new DataAccessLayer();
            TotalRecords = dal.GetTotalUserCounts(configuration);
            TotalPages =(int) System.Math.Ceiling((double)TotalRecords / PageSize);

            CurrentPage=page.HasValue && page > 0 ? page.Value : 1;
            users=dal.GetUsers(configuration,CurrentPage,PageSize);
        }
    }
}
