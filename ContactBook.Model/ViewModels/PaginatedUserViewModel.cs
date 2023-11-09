
using ContactBook.Model.Entity;

namespace ContactBook.Model.ViewModels
{
    public class PaginatedUserViewModel
    {
        public int TotalUsers { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
        public List<User> Users { get; set; }


    }
}
