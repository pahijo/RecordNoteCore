using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecordNoteCore.Data;
using RecordNoteCore.Data.Entities;

namespace RecordNoteCore.Controllers
{
    public class ReportNotesController : Controller
    {
        private readonly IRepository repository;

        public ReportNotesController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: ReportNotes
        public IActionResult Index()
        {
            ViewBag.Tests = new SelectList(this.repository.GetTests(), "Id", "Name");
            ViewBag.Students = new SelectList(this.repository.GetStudents(), "Id", "Name");
            return View(this.repository.GetAllSumary());
        }

        // GET: ReportNotes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportNote = this.repository.GetReportNoteId(id.Value);
            if (reportNote == null)
            {
                return NotFound();
            }

            return View(reportNote);
        }



        public IActionResult DetailsSumary(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentNote = this.repository.GetIdSumary(id);

            if (studentNote == null)
            {
                return NotFound();
            }
            return View(studentNote);
        }
        // GET: ReportNotes/Create
        public IActionResult Create()
        {
            ReportNoteView model = new ReportNoteView();
            model.Tests = new SelectList(this.repository.GetTests(), "Id", "Name");
            model.Students = new SelectList(this.repository.GetStudents(), "Id", "Name");

            return View(model);
        }

        // POST: ReportNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportNoteView reportNoteView) //[Bind("Id,Students,Tests,Note")] 
        {
            var res = reportNoteView;

            ReportNote reportNote = new ReportNote
            {
                Student = this.repository.GetStudentId(reportNoteView.Student),
                Test = this.repository.GetTestId(reportNoteView.Test),
                Note = reportNoteView.Note
            };

            if (ModelState.IsValid)
            {
                //Se valida que no repita el Test
                IEnumerable<ReportNote> report = this.repository.GetIdSumary(reportNote.Student.Id);

                var newTest = report.Where(r => r.Test.Id == reportNote.Test.Id).FirstOrDefault();
                if (newTest == null)
                {
                    this.repository.AddReportNote(reportNote);
                    await repository.SaveAllAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Message = "Test registered!";
            reportNoteView.Tests = new SelectList(this.repository.GetTests(), "Id", "Name");
            reportNoteView.Students = new SelectList(this.repository.GetStudents(), "Id", "Name");

            return View(reportNoteView);
        }

        // GET: ReportNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportNote = this.repository.GetReportNoteId(id.Value);
            if (reportNote == null)
            {
                return NotFound();
            }
            return View(reportNote);
        }

        // POST: ReportNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Note")] ReportNote reportNote)
        {
            if (id != reportNote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdateReportNotes(reportNote);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportNoteExists(reportNote.Id))
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
            return View(reportNote);
        }

        // GET: ReportNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportNote = this.repository.GetReportNoteId(id.Value);
            if (reportNote == null)
            {
                return NotFound();
            }

            this.repository.RemoveReportNote(reportNote);
            await this.repository.SaveAllAsync();
            return RedirectToAction("Index");
        }


        private bool ReportNoteExists(int id)
        {
            return this.repository.ReportNoteExists(id);
        }


    }
}
