using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecordNoteCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecordNoteCore.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext context;


        #region Constructors
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        #endregion

        #region Common
        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
              
        #endregion

        #region Test
        public IEnumerable<Test> GetTests()
        {
            return this.context.Tests.OrderBy(t => t.Name);
        }

        public Test GetTestId(int id)
        {
            return this.context.Tests.Where(t => t.Id == id).FirstOrDefault(); ;
        }

        public void AddTest(Test test)
        {
            this.context.Tests.Add(test);
        }

        public void UpdateTest(Test test)
        {
            this.context.Update(test);
        }

        public void RemoveTest(Test test)
        {
            this.context.Remove(test);
        }


        public bool TestExists(int id)
        {
            return this.context.Tests.Any(e => e.Id == id);
        }

        #endregion

        #region Student

        public IEnumerable<Student> GetStudents()
        {
            return this.context.Students.OrderBy(t => t.Name);
        }

        public Student GetStudentId(int id)
        {
            return this.context.Students.Where(t => t.Id == id).FirstOrDefault(); ;
        }

        public void AddStudent(Student student)
        {
            this.context.Students.Add(student);
        }

        public void UpdateStudents(Student student)
        {
            this.context.Update(student);
        }

        public void RemoveStudent(Student student)
        {
            this.context.Remove(student);
        }


        public bool StudentExists(int id)
        {
            return this.context.Students.Any(e => e.Id == id);
        }

        #endregion

        #region ReportNote
        public IEnumerable<ReportNote> GetReportNotes()
        {
            return this.context.ReportNote
                .Include(s => s.Student)
                .Include(t => t.Test)
                .OrderBy(t => t.Student);
        }
       

        public ReportNote GetReportNoteId(int id)
        {
            return this.context.ReportNote.Where(t => t.Id == id)
                .Include(s => s.Student)
                .Include(t => t.Test)
                .FirstOrDefault(); ;
        }

        public void AddReportNote(ReportNote reportNote)
        {
            this.context.ReportNote.Add(reportNote);
        }

        public void UpdateReportNotes(ReportNote reportNote)
        {
            this.context.Update(reportNote);
        }

        public void RemoveReportNote(ReportNote reportNote)
        {
            this.context.Remove(reportNote);
        }

        public bool ReportNoteExists(int id)
        {
            return this.context.ReportNote.Any(e => e.Id == id);
        }

        public List<SummaryNote> GetAllSumary()
        {
            var listado = this.context.ReportNotes
                .Include(s => s.Student)
                .Include(t => t.Test)
                .GroupBy(x => x.Student);
            List<SummaryNote> summaryNotes = new List<SummaryNote>();

            foreach(var item in listado)
            {
                SummaryNote summary = new SummaryNote();
                summary.TestNoteViews = new List<TestNoteView>();
                summary.Student = item.Key;
                var sty = item.GetEnumerator();

                decimal final = 0;
                foreach (ReportNote test in item)
                {
                    TestNoteView camp = new TestNoteView();
                    camp.Id = test.Test.Id;
                    camp.IsAvailable = test.Test.IsAvailable;
                    camp.Name = test.Test.Name;
                    camp.Note = test.Note;
                    camp.Percentage = test.Test.Percentage;
                    final += camp.Note * (camp.Percentage / 100);

                    summary.TestNoteViews.Add(camp);
                }
                summary.Final = final;
                summaryNotes.Add(summary);
            }
           

            return summaryNotes;

        }

        //public SummaryNote GetIdSumary(int id)
        //{
        //    var listado = this.context.ReportNotes
        //        .Where(r=>r.Student.Id == id)
        //        .Include(s => s.Student)
        //        .Include(t => t.Test)
        //        .GroupBy(x => x.Student).ToList();
        //    SummaryNote summary = new SummaryNote();
        //    summary.TestNoteViews = new List<TestNoteView>();
        //    foreach (var item in listado)
        //    {
        //        TestNoteView testNote = new TestNoteView();
        //        summary.Student = item.Key;
        //        foreach(var detail in item)
        //        {
        //            testNote.Id = detail.Test.Id;
        //            testNote.IsAvailable = detail.Test.IsAvailable;
        //            testNote.Name = detail.Test.Name;
        //            testNote.Percentage = detail.Test.Percentage;
        //            testNote.Note = detail.Note;
        //            summary.TestNoteViews.Add(testNote);
        //        }
        //    }
        //    return summary;
        //}


        public IEnumerable<ReportNote> GetIdSumary(int id)
        {
            return this.context.ReportNote.Where(t => t.Student.Id == id)
                .Include(s => s.Student)
                .Include(t => t.Test);                
        }

        #endregion


    }
}
