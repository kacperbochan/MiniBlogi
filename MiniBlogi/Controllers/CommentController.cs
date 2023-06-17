using Microsoft.AspNetCore.Mvc;
using MiniBlogi.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // GET: /Comment/
        public async Task<IActionResult> Index()
        {
            return View(await _commentRepository.GetAllAsync());
        }

        // GET: /Comment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentRepository.GetByIdAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: /Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,BlogPostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                await _commentRepository.AddAsync(comment);
                await _commentRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // POST: /Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,BlogPostId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _commentRepository.UpdateAsync(comment);
                await _commentRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _commentRepository.DeleteAsync(id);
            await _commentRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }


}
