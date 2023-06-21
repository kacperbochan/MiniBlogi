using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;
using MiniBlogi.Pages.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Pages.Blog
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public BlogPostMini BlogPostMini { get; set; }

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment environment, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _environment = environment;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            BlogPostMini = new BlogPostMini();
            
            if(int.TryParse(id,out int Id))
            {
                var resoult = await _unitOfWork.BlogPostRepository.GetByIdAsync(Id);
                if(resoult != null)
                {
                    BlogPostMini.Id = resoult.Id;
                    BlogPostMini.Title = resoult.Title;  
                    BlogPostMini.Description = resoult.Description;  
                    BlogPostMini.Tags = string.Join(",",resoult.Tags.Select(x=>x.Name));
                    BlogPostMini.Images = resoult.Images.Select(i => i.FilePath).ToList();


                    var wwwrootPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
                    BlogPostMini.Images = resoult.Images
                        .Select(i => Path.GetRelativePath(wwwrootPath, i.FilePath))
                        .ToList();


                    return Page();
                }
            }
            return RedirectToPage("./Index");
            
        }

        public async Task<IActionResult> OnPostAsync(ICollection<IFormFile?>? addedImages, string? RemovedImages = "", string? RemovedTags = "")
        {
            var files = Request.Form.Files;

            BlogPost BlogPost = await _unitOfWork.BlogPostRepository.GetByIdAsync(BlogPostMini.Id);

            if (_signInManager.IsSignedIn(User))
            {
                BlogPost.Title = BlogPostMini.Title;
                BlogPost.Description = BlogPostMini.Description;
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }

            if (BlogPostMini.Tags != null && BlogPostMini.Tags.Length > 0)
            {
                foreach (string x in BlogPostMini.Tags.Split(','))
                {
                    Tag tag = await _unitOfWork.TagRepository.GetByNameAsync(x) ?? new Tag() { Name = x };
                    BlogPost.Tags!.Add(tag);
                }
            }
            else
            {
                BlogPost.Tags.Clear();
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

            if (RemovedTags != null)
            {
                var removedTagsList = RemovedTags.Split(',').ToList();

                // Usuwamy zdjêcia z bazy danych lub z dysku
                foreach (var tag in removedTagsList)
                {
                    Tag resoult = await _unitOfWork.TagRepository.GetByNameAsync(tag);
                    BlogPost.Tags!.Remove(resoult);
                }
            }

            if (RemovedImages != null) 
            {
                var removedImagesList = RemovedImages.Split(',').ToList();

                // Usuwamy zdjêcia z bazy danych lub z dysku
                foreach (var imageName in removedImagesList)
                {
                    Image resoult = await _unitOfWork.ImageRepository.GetByDataAsync(imageName);
                    BlogPost.Images!.Remove(resoult);
                }
            }


            await _unitOfWork.BlogPostRepository.UpdateAsync(BlogPost);
            await _unitOfWork.BlogPostRepository.SaveAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _unitOfWork.BlogPostRepository.DeleteAsync(id);
            await _unitOfWork.BlogPostRepository.SaveAsync();
            return RedirectToAction("Index");
        }


    }
}
