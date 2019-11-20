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
            if(workItem == null)
            {
                return NotFound(0);
            }
            return View(workItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkItem workItem)
        {
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
    }
}