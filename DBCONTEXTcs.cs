using Microsoft.EntityFrameworkCore;

namespace DeptPick
{
    public class DBCONTEXTcs :DbContext
    {
        public DBCONTEXTcs(DbContextOptions options) : base(options)
        {
        }
        public DbSet<College> Colleges => Set<College>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<OtpCode> OtpCodes => Set<OtpCode>();
        public DbSet<RegistrationPeriod> RegistrationPeriods => Set<RegistrationPeriod>();
        public DbSet<PreferenceSubmission> PreferenceSubmissions => Set<PreferenceSubmission>();
        public DbSet<StudentPreference> StudentPreferences => Set<StudentPreference>();
        public DbSet<AllocationRound> AllocationRounds => Set<AllocationRound>();
        public DbSet<AllocationResult> AllocationResults => Set<AllocationResult>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<DeviceToken> DeviceTokens => Set<DeviceToken>();
        public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToTable("INFO_PRODUCTS");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<College>(entity =>
            {
                entity.ToTable("Collages");
                entity.HasKey(e => e.Id);
                                             //does not allow null
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Code).IsUnique();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Departments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.Code).IsUnique();
                entity.HasOne(e => e.College).WithMany(c => c.Departments)
                                                          //يمنع مسح كليه موجود وعنده علاقات في جدوال تانيه 
                .HasForeignKey(e => e.CollegeId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.UniversityId).IsRequired().HasMaxLength(20);
                entity.Property(e => e.NationalId).IsRequired().HasMaxLength(14);
                entity.HasIndex(e => e.UniversityEmail).IsUnique();
                entity.HasIndex(e => e.UniversityId).IsUnique();
                entity.HasIndex(e => e.NationalId).IsUnique();
                entity.Property(e => e.UniversityEmail).IsRequired().HasMaxLength(150);
                entity.Property(e => e.CumulativeGpa).HasColumnType("decimal(4,2)");
                entity.Property(e => e.LastSemesterGpa).HasColumnType("decimal(4,2)");
                entity.HasOne(e => e.College).WithMany(c => c.Students)
                .HasForeignKey(e => e.CollegeId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OtpCode>(entity =>
            {
                entity.ToTable("OtpCodes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UniversityEmail).IsRequired().HasMaxLength(150);
                entity.Property(e => e.CodeHash).IsRequired().HasMaxLength(256);
                entity.HasIndex(e => new { e.UniversityEmail, e.IsUsed, e.ExpiresAt }); 
            });

            modelBuilder.Entity<RegistrationPeriod>(entity =>
            {
                entity.ToTable("RegistrationPeriods");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.HasOne(r => r.CreatedByAdmin)
                      .WithMany()
                      .HasForeignKey(r => r.CreatedByAdminId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<PreferenceSubmission>(entity =>
            {
                entity.ToTable("PreferenceSubmissions");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.StudentId, e.RegistrationPeriodId }).IsUnique();

                entity.HasOne(p => p.Student)
                      .WithMany(s => s.PreferenceSubmissions)
                      .HasForeignKey(p => p.StudentId)//if delete student deleta also any submission for him
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(p => p.RegistrationPeriod)
             .WithMany(r => r.PreferenceSubmissions)
             .HasForeignKey(p => p.RegistrationPeriodId)
             .OnDelete(DeleteBehavior.Restrict);//prevent delete registrationperiod with submissions
            });

            modelBuilder.Entity<StudentPreference>(entity =>
            {
                entity.ToTable("StudentPreferences");
                entity.HasKey(e => e.Id);

                // Unique: مفيش قسم متكرر ولا ترتيب متكرر في نفس الـ Submission
                entity.HasIndex(e => new { e.SubmissionId, e.Priority }).IsUnique();
                entity.HasIndex(e => new { e.SubmissionId, e.DepartmentId }).IsUnique();

                entity.HasOne(sp => sp.Submission)
                      .WithMany(ps => ps.StudentPreferences)
                      .HasForeignKey(sp => sp.SubmissionId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(sp => sp.Department)
            .WithMany(d => d.StudentPreferences)
            .HasForeignKey(sp => sp.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AllocationRound>(entity =>
            {
                entity.ToTable("AllocationRounds");
                entity.HasKey(e => e.Id);

                entity.HasOne(a => a.RegistrationPeriod)
                      .WithMany(r => r.AllocationRounds)
                      .HasForeignKey(a => a.RegistrationPeriodId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.CreatedByAdmin)
                      .WithMany()
                      .HasForeignKey(a => a.CreatedByAdminId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AllocationResult>(entity =>
            {
                entity.ToTable("AllocationResults");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.GpaSnapshot).HasColumnType("decimal(4,2)");

                // Unique: طالب واحد ليه نتيجة واحدة في الـ Round دي
                entity.HasIndex(e => new { e.AllocationRoundId, e.StudentId }).IsUnique();

                entity.HasOne(ar => ar.AllocationRound)
                      .WithMany(a => a.AllocationResults)
                      .HasForeignKey(ar => ar.AllocationRoundId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ar => ar.Student)
             .WithMany(s => s.AllocationResults)
             .HasForeignKey(ar => ar.StudentId)
             .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ar => ar.Department)
                      .WithMany(d => d.AllocationResults)
                      .HasForeignKey(ar => ar.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notifications");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Body).IsRequired().HasMaxLength(1000);

                entity.HasOne(n => n.Student)
                      .WithMany(s => s.Notifications)
                      .HasForeignKey(n => n.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DeviceToken>(entity =>
            {
                entity.ToTable("DeviceTokens");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired().HasMaxLength(500);
                entity.HasIndex(e => e.Token).IsUnique();

                entity.HasOne(dt => dt.Student)
                      .WithMany(s => s.DeviceTokens)
                      .HasForeignKey(dt => dt.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.ToTable("AdminUsers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50);

                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLogs");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EntityType).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Action).IsRequired().HasMaxLength(50);

                entity.HasOne(a => a.PerformedByAdmin)
                      .WithMany(au => au.AuditLogs)
                      .HasForeignKey(a => a.PerformedByAdminId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(a => a.PerformedByStudent)
                      .WithMany()
                      .HasForeignKey(a => a.PerformedByStudentId)
                       .OnDelete(DeleteBehavior.SetNull);
            });

        }
    }
}
