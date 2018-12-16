using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecordNoteCore.Data;
using RecordNoteCore.Data.Entities;
using RecordNoteCore.Models;

namespace RecordNoteCore.Controllers
{
    public class TestsController : Controller
    {
        private readonly IRepository repository;


        public TestsController(IRepository context)
        {
            this.repository = context;
        }

        // GET: Tests
        public IActionResult Index()
        {
            return View(this.repository.GetTests());
        }

        // GET: Tests/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = this.repository.GetTestId(id.Value);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Percentage,IsAvailable")] Test test)
        {
            if (ModelState.IsValid)
            {
                //Verify all Test
                IEnumerable<Test> tests = this.repository.GetTests();
                decimal Percentage = test.Percentage;
                foreach(var item in tests)
                {
                    Percentage += item.Percentage;
                }

                if(Percentage >100)
                {
                    ViewBag.Message = "percentage not allowed";
                    return View();
                }

                this.repository.AddTest(test);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        // GET: Tests/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = this.repository.GetTestId(id.Value);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Percentage,IsAvailable")] Test test)
        {
            if (id != test.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdateTest(test);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.Id))
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
            return View(test);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var test = this.repository.GetTestId(id.Value);
                if (test == null)
                {
                    return NotFound();
                }
               
                this.repository.RemoveTest(test);
                await this.repository.SaveAllAsync();
                
            }
            catch(Exception ex )
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId ="Problem"});
        }

        private bool TestExists(int id)
        {
            return this.repository.TestExists(id);
        }
    }
}
