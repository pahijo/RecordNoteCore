using System.Collections.Generic;
using System.Threading.Tasks;
using RecordNoteCore.Data.Entities;

namespace RecordNoteCore.Data
{
    public interface IRepository
    {
        void AddTest(Test test);

        Test GetTestId(int id);

        IEnumerable<Test> GetTests();

        void RemoveTest(Test test);

        Task<bool> SaveAllAsync();

        void UpdateTest(Test test);

        bool TestExists(int id);

        IEnumerable<Student> GetStudents();


        Student GetStudentId(int id);

        void AddStudent(Student student);


        void UpdateStudents(Student student);


        void RemoveStudent(Student student);


        bool StudentExists(int id);



        IEnumerable<ReportNote> GetReportNotes();

        List<SummaryNote> GetAllSumary();

        IEnumerable<ReportNote> GetIdSumary(int id);

        ReportNote GetReportNoteId(int id);

        void AddReportNote(ReportNote reportNote);


        void UpdateReportNotes(ReportNote reportNote);


        void RemoveReportNote(ReportNote reportNote);


        bool ReportNoteExists(int id);
    }
}