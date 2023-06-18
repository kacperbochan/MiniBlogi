using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;
using MiniBlogi.Pages.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Pages.Blog
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public BlogPost BlogPost { get; set; }

        [BindProperty]
        public BlogPostMini BlogPostMini { get; set; }

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment environment, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _environment = environment;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            BlogPostMini = new BlogPostMini();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ICollection<IFormFile> addedImages)
        {


            var files = Request.Form.Files;

            BlogPost = new BlogPost();

            if (_signInManager.IsSignedIn(User))
            {
                BlogPost.UserId = _userManager.GetUserId(User);
                BlogPost.User = (ApplicationUser?)_userManager.Users.Where(x => x.Id == BlogPost.UserId).First();

                BlogPost.Title = BlogPostMini.Title;
                BlogPost.Description = BlogPostMini.Description;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (BlogPostMini.Tags != null && BlogPostMini.Tags.Length > 0)
            {
                foreach (string x in BlogPostMini.Tags.Split(','))
                {
                    Tag tag = await _unitOfWork.TagRepository.GetByNameAsync(x) ?? new Tag() { Name = x };
                    BlogPost.Tags!.Add(tag);
                }
            }

            if (addedImages != null && addedImages.Count > 0)
            {
                foreach (var addedImage in addedImages)
                {
                    var imagePath = Path.Combine(_environment.WebRootPath, "images", addedImage.FileName);
                    Image image = new Image();
                    image.FilePath = imagePath;
                    image.FileName = addedImage.FileName;
                    image.FileFormat = addedImage.ContentType;

                    Image resoult = await _unitOfWork.ImageRepository.GetByDataAsync(image);

                    if (resoult == null)
                    {
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await addedImage.CopyToAsync(stream);
                        }
                        BlogPost.Images!.Add(image);
                    }
                    else
                    {
                        BlogPost.Images!.Add(resoult);
                    }
                }
            }


            await _unitOfWork.BlogPostRepository.AddAsync(BlogPost);
            await _unitOfWork.BlogPostRepository.SaveAsync();

            return RedirectToPage("./Index");
        }
    }


}
