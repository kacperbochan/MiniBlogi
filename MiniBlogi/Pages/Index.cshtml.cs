using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Pages.Blog
{
    public class IndexModel : PageModel
    {


        public int CurrentPage { get; set; } = 1;
        public int PageAmount { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IEnumerable<BlogPost> BlogPosts { get; set; }

        public async Task<IActionResult> OnGetAsync(string username="", int currentPage = 1)
        {
            var user = await _userManager.FindByNameAsync(username+"@gmail.com");

            if (user == null)
            {

                if (await _unitOfWork.BlogPostRepository.IsBlogNotNull())
                {
                    PageAmount = await _unitOfWork.BlogPostRepository.GetPageAmount();

                    CurrentPage = (currentPage > PageAmount) ? PageAmount : currentPage;

                    BlogPosts = await _unitOfWork.BlogPostRepository.GetCurrentPage(currentPage);


                }
                return Page();
            }
            else{
                if (await _unitOfWork.BlogPostRepository.IsBlogNotNull())
                {
                    PageAmount = await _unitOfWork.BlogPostRepository.GetUserPageAmount(user.Id);

                    CurrentPage = (currentPage > PageAmount) ? PageAmount : currentPage;

                    BlogPosts = await _unitOfWork.BlogPostRepository.GetCurrentPageOfUser(currentPage, user.Id);

                    return Page();
                }
            }
            return Page();
        }
    }
}
