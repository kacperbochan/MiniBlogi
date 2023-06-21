using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;
using MiniBlogi.Pages.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public BlogPostMini BlogPostMini { get; set; }
        [BindProperty]
        public List<Comment> Comments { get; set; } = new List<Comment>();
        [BindProperty]
        public string newComment { get; set; } = "";

        public DetailsModel(IUnitOfWork unitOfWork, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {

            BlogPostMini = new BlogPostMini();

            if (int.TryParse(id, out int Id))
            {
                var resoult = await _unitOfWork.BlogPostRepository.GetByIdAsync(Id);
                if (resoult != null)
                {
                    BlogPostMini.Id = resoult.Id;
                    BlogPostMini.Title = resoult.Title;
                    BlogPostMini.Description = resoult.Description;
                    BlogPostMini.Tags = string.Join(",", resoult.Tags.Select(x => x.Name));
                    BlogPostMini.Images = resoult.Images.Select(i => i.FilePath).ToList();
                    BlogPostMini.UserId = resoult.UserId;


                    var wwwrootPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
                    BlogPostMini.Images = resoult.Images
                        .Select(i => Path.GetRelativePath(wwwrootPath, i.FilePath))
                        .ToList();

                    Comments = await _unitOfWork.CommentRepository.GetAllOfBlog(Id);

                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (int.TryParse(id, out int Id))
            {
                BlogPost resoult = await _unitOfWork.BlogPostRepository.GetByIdAsync(Id);

                var comment = new Comment
                {
                    Content = newComment,
                    DatePosted = DateTime.UtcNow,
                    UserId = _userManager.GetUserId(User),
                    User = (ApplicationUser?)_userManager.Users.Where(x => x.Id.ToString() == _userManager.GetUserId(User)).First(),
                    IPAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                    BlogPostId = Id,
                    BlogPost = resoult
                };

                resoult.Comments.Add(comment);

                await _unitOfWork.BlogPostRepository.UpdateAsync(resoult);
                await _unitOfWork.BlogPostRepository.SaveAsync();
            }


            return RedirectToPage("",new { Id = Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int commentId)
        {
            await _unitOfWork.CommentRepository.DeleteAsync(commentId);
            await _unitOfWork.CommentRepository.SaveAsync();
            return RedirectToPage();
        }
    }

}
