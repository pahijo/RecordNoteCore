using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecordNoteCore.Data.Entities;

namespace RecordNoteCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public  DbSet<Test> Tests { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<ReportNote> ReportNotes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RecordNoteCore.Data.Entities.ReportNote> ReportNote { get; set; }
    }
}
