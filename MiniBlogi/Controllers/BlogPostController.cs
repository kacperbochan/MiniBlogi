using Microsoft.AspNetCore.Mvc;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;
using MiniBlogi.Repo;
using MiniBlogi.Repo.Interfaces;
using System;

namespace MiniBlogi.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        // GET: /BlogPost/
        public async Task<IActionResult> Index()
        {
            return View(await _blogPostRepository.GetAllAsync());
        }

        // GET: /BlogPost/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _blogPostRepository.GetByIdAsync(id.Value);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: /BlogPost/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                await _blogPostRepository.AddAsync(blogPost);
                await _blogPostRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // POST: /BlogPost/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _blogPostRepository.UpdateAsync(blogPost);
                await _blogPostRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // POST: /BlogPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogPostRepository.DeleteAsync(id);
            await _blogPostRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }



}
