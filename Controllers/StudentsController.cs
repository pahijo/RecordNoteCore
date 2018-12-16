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
    public class StudentsController : Controller
    {
        private readonly IRepository repository;

        public StudentsController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: Students
        public IActionResult Index()
        {
            return View(this.repository.GetStudents());
        }

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = this.repository.GetStudentId(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Student student)
        {
            if (ModelState.IsValid)
            {
                this.repository.UpdateStudents(student);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = this.repository.GetStudentId(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdateStudents(student);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var student = this.repository.GetStudentId(id.Value);

            try
            {
                if (student == null)
                {
                    return NotFound();
                }
                this.repository.RemoveStudent(student);
                await this.repository.SaveAllAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

            return View(student);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = "Problem" });
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = this.repository.GetStudentId(id);
            this.repository.RemoveStudent(student);
            await this.repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return this.repository.StudentExists(id);
        }

    }
}
