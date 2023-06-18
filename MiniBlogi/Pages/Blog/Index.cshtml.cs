using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<BlogPost> BlogPosts { get; set; }

        public async Task OnGetAsync()
        {
            BlogPosts = (await _unitOfWork.BlogPostRepository.GetAllAsync()).ToList();
        }
    }
}
