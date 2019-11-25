using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.Models;

namespace Tracker.Controllers
{
    public class WorkItemController : Controller
    {
        private readonly AppDbContext _context;
        public WorkItemController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var workItems = await _context.WorkItem.ToListAsync();
            return View(workItems);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkItem workItem)
        {
            if(ModelState.IsValid)
            {
                workItem.CreatedAt = DateTime.Now;
                _context.Add(workItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(workItem);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var workItem = await _context.WorkItem.FindAsync(id);
            Console.Write(workItem.CreatedAt);
            if(workItem == null)
            {
                return NotFound(0);
            }
            return View(workItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkItem workItem)
        {
            Console.WriteLine(workItem.CreatedAt);
            if(ModelState.IsValid)
            {
                _context.Update(workItem);
                var modLog = new ModLog { ItemId = workItem.Id };
                _context.Add(modLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workItem);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workDetails = new WorkDetailsDto
            {
                Works = await _context.WorkItem.FindAsync(id),
                ModLogs = await _context.ModLog.Where(m => m.ItemId == id).ToListAsync()
            };
            if (workDetails == null)
            {
                return NotFound();
            }
            return View(workDetails);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workDetails = new WorkDetailsDto
            {
                Works = await _context.WorkItem.FindAsync(id),
                ModLogs = await _context.ModLog.Where(m => m.ItemId == id).ToListAsync()
            };
            if (workDetails == null)
            {
                return NotFound();
            }
            return View(workDetails);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var workItem = await _context.WorkItem.FindAsync(id);
            if (workItem == null)
            {
                return View();
            }
            _context.WorkItem.Remove(workItem);

            var modLogs = _context.ModLog.Where(m => m.ItemId == id);
            if(modLogs == null)
            {
                return View();
            }
            foreach(var item in modLogs)
            {
                _context.ModLog.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}