using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BlogPost BlogPost { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BlogPost = await _unitOfWork.BlogPostRepository.GetByIdAsync(id);

            if (BlogPost == null)
            {
                return NotFound();
            }

            return Page();
        }
    }

}
