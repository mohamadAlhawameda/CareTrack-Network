//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Project_Final.Data;
//using Project_Final.Models;

//namespace Project_Final.Controllers
//{
//    public class MessagesController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public MessagesController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//         GET: Messages
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.Message.ToListAsync());
//        }

//         GET: Messages/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var message = await _context.Message
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (message == null)
//            {
//                return NotFound();
//            }

//            return View(message);
//        }

//         GET: Messages/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//         POST: Messages/Create
//         To protect from overposting attacks, enable the specific properties you want to bind to.
//         For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Content,SentAt")] Message message)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(message);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(message);
//        }

//         GET: Messages/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var message = await _context.Message.FindAsync(id);
//            if (message == null)
//            {
//                return NotFound();
//            }
//            return View(message);
//        }

//         POST: Messages/Edit/5
//         To protect from overposting attacks, enable the specific properties you want to bind to.
//         For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,SentAt")] Message message)
//        {
//            if (id != message.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(message);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!MessageExists(message.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(message);
//        }

//         GET: Messages/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var message = await _context.Message
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (message == null)
//            {
//                return NotFound();
//            }

//            return View(message);
//        }

//         POST: Messages/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var message = await _context.Message.FindAsync(id);
//            if (message != null)
//            {
//                _context.Message.Remove(message);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool MessageExists(int id)
//        {
//            return _context.Message.Any(e => e.Id == id);
//        }
//    }
//}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Final.Data;
using Project_Final.Models;
using Microsoft.AspNetCore.Authorization;

namespace Project_Final.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, string searchString)
        {
            // Load messages and include their associated replies
            var query = _context.Message.Include(m => m.Replies).AsQueryable();

            // Filter by start date if provided
            if (startDate.HasValue)
            {
                query = query.Where(m => m.SentAt >= startDate);
            }

            // Filter by end date if provided
            if (endDate.HasValue)
            {
                query = query.Where(m => m.SentAt <= endDate);
            }

            // Filter by message content if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Content.Contains(searchString));
            }

            var messagesWithReplies = await query.ToListAsync();

            return View(messagesWithReplies);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .FirstOrDefaultAsync(m => m.Id == id);

            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                // Set the SentAt property to the current date and time
                message.SentAt = DateTime.Now;

                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }
        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.FindAsync(id);
            if (message != null)
            {
                _context.Message.Remove(message);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,SentAt")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}

