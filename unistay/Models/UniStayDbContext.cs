using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace unistay.Models;

public partial class UniStayDbContext : DbContext
{
    public UniStayDbContext()
    {
    }

    public UniStayDbContext(DbContextOptions<UniStayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Allocation> Allocations { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Dormitory> Dormitories { get; set; }

    public virtual DbSet<EvictionNotice> EvictionNotices { get; set; }

    public virtual DbSet<Guardian> Guardians { get; set; }

    public virtual DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<Violation> Violations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MOH7;Database=DormDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E84B7E74F6");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.Username, "UQ__Admin__536C85E4B906DC3E").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DormitoryId).HasColumnName("DormitoryID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Dormitory).WithMany(p => p.Admins)
                .HasForeignKey(d => d.DormitoryId)
                .HasConstraintName("FK__Admin__Dormitory__6FE99F9F");
        });

        modelBuilder.Entity<Allocation>(entity =>
        {
            entity.HasKey(e => e.AllocationId).HasName("PK__Allocati__B3C6D6AB54062F17");

            entity.ToTable("Allocation");

            entity.HasIndex(e => e.RoomId, "IDX_Allocation_RoomID");

            entity.HasIndex(e => e.StudentId, "UQ_ActiveAllocation")
                .IsUnique()
                .HasFilter("([IsActive]=(1) AND [IsDeleted]=(0))");

            entity.Property(e => e.AllocationId).HasColumnName("AllocationID");
            entity.Property(e => e.AcademicYear).HasMaxLength(20);
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MealPlanAllocated).HasMaxLength(50);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Semester).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.AllocatedByNavigation).WithMany(p => p.Allocations)
                .HasForeignKey(d => d.AllocatedBy)
                .HasConstraintName("FK__Allocatio__Alloc__01142BA1");

            entity.HasOne(d => d.Application).WithMany(p => p.Allocations)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Allocatio__Appli__7E37BEF6");

            entity.HasOne(d => d.Room).WithMany(p => p.Allocations)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Allocatio__RoomI__00200768");

            entity.HasOne(d => d.Student).WithOne(p => p.Allocation)
                .HasForeignKey<Allocation>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Allocatio__Stude__7F2BE32F");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__C93A4F7970A8FEB8");

            entity.ToTable("Application");

            entity.HasIndex(e => e.Status, "IDX_Application_Status");

            entity.HasIndex(e => new { e.StudentId, e.AcademicYear, e.Semester }, "UQ_App_Student_Year")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.AcademicYear).HasMaxLength(20);
            entity.Property(e => e.ApplicationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MealPlanType).HasMaxLength(50);
            entity.Property(e => e.PreferredRoomId).HasColumnName("PreferredRoomID");
            entity.Property(e => e.Semester).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.PreferredRoom).WithMany(p => p.Applications)
                .HasForeignKey(d => d.PreferredRoomId)
                .HasConstraintName("FK__Applicati__Prefe__787EE5A0");

            entity.HasOne(d => d.ReviewedByNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ReviewedBy)
                .HasConstraintName("FK__Applicati__Revie__778AC167");

            entity.HasOne(d => d.Student).WithMany(p => p.Applications)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__Stude__76969D2E");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.BuildingId).HasName("PK__Building__5463CDE4F52AA5CA");

            entity.ToTable("Building");

            entity.Property(e => e.BuildingId).HasColumnName("BuildingID");
            entity.Property(e => e.BuildingName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DormitoryId).HasColumnName("DormitoryID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Dormitory).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.DormitoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Building__Dormit__571DF1D5");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6FC18E9654");

            entity.ToTable("Document");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsVerified).HasDefaultValue(false);

            entity.HasOne(d => d.Application).WithMany(p => p.Documents)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK__Document__Applic__2EDAF651");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.VerifiedBy)
                .HasConstraintName("FK__Document__Verifi__2FCF1A8A");
        });

        modelBuilder.Entity<Dormitory>(entity =>
        {
            entity.HasKey(e => e.DormitoryId).HasName("PK__Dormitor__14188ACEB80343AA");

            entity.ToTable("Dormitory");

            entity.Property(e => e.DormitoryId).HasColumnName("DormitoryID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DormitoryName).HasMaxLength(200);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.UniversityId).HasColumnName("UniversityID");

            entity.HasOne(d => d.University).WithMany(p => p.Dormitories)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dormitory__Unive__5165187F");
        });

        modelBuilder.Entity<EvictionNotice>(entity =>
        {
            entity.HasKey(e => e.EvictionId).HasName("PK__Eviction__A889DE2EEA5D1D3F");

            entity.ToTable("EvictionNotice");

            entity.Property(e => e.EvictionId).HasColumnName("EvictionID");
            entity.Property(e => e.AllocationId).HasColumnName("AllocationID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Allocation).WithMany(p => p.EvictionNotices)
                .HasForeignKey(d => d.AllocationId)
                .HasConstraintName("FK__EvictionN__Alloc__114A936A");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EvictionNotices)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__EvictionN__Creat__123EB7A3");

            entity.HasOne(d => d.Student).WithMany(p => p.EvictionNotices)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__EvictionN__Stude__10566F31");
        });

        modelBuilder.Entity<Guardian>(entity =>
        {
            entity.HasKey(e => e.GuardianId).HasName("PK__Guardian__0A5E1B7BD40A7843");

            entity.ToTable("Guardian");

            entity.Property(e => e.GuardianId).HasColumnName("GuardianID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Job).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.NationalId)
                .HasMaxLength(20)
                .HasColumnName("NationalID");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Relation).HasMaxLength(50);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Student).WithMany(p => p.Guardians)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Guardian__Studen__68487DD7");
        });

        modelBuilder.Entity<MaintenanceRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Maintena__33A8519A37A2E4D5");

            entity.ToTable("MaintenanceRequest");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IssueType).HasMaxLength(100);
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__Maintenan__Assig__29221CFB");

            entity.HasOne(d => d.Room).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Maintenan__RoomI__2739D489");

            entity.HasOne(d => d.Student).WithMany(p => p.MaintenanceRequests)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Maintenan__Stude__282DF8C2");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.MealId).HasName("PK__Meal__ACF6A65D5FF5791E");

            entity.ToTable("Meal");

            entity.HasIndex(e => e.MealDate, "IDX_Meal_Date");

            entity.HasIndex(e => new { e.StudentId, e.MealDate, e.MealType }, "UQ__Meal__3449321FDD4DEA4F").IsUnique();

            entity.Property(e => e.MealId).HasColumnName("MealID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MealType).HasMaxLength(20);
            entity.Property(e => e.MissedPenalty).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Student).WithMany(p => p.Meals)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Meal__StudentID__19DFD96B");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A580397A68F");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.StudentId, "IDX_Payment_StudentID");

            entity.HasIndex(e => e.ReceiptNumber, "UQ__Payment__C08AFDABA0BC64E5").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.AcademicYear).HasMaxLength(20);
            entity.Property(e => e.AllocationId).HasColumnName("AllocationID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsOverdue).HasDefaultValue(false);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentType).HasMaxLength(50);
            entity.Property(e => e.ReceiptNumber).HasMaxLength(100);
            entity.Property(e => e.Semester).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Allocation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.AllocationId)
                .HasConstraintName("FK__Payment__Allocat__09A971A2");

            entity.HasOne(d => d.ReceivedByNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ReceivedBy)
                .HasConstraintName("FK__Payment__Receive__0A9D95DB");

            entity.HasOne(d => d.Student).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Payment__Student__08B54D69");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__32863919BFCAA185");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.BuildingId).HasColumnName("BuildingID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.RoomNumber).HasMaxLength(20);

            entity.HasOne(d => d.Building).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__BuildingID__5DCAEF64");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A796B4E1D2D");

            entity.ToTable("Student");

            entity.HasIndex(e => e.Email, "IDX_Student_Email");

            entity.HasIndex(e => e.NationalId, "IDX_Student_NationalID");

            entity.HasIndex(e => e.NationalId, "UQ__Student__E9AA321ACE66F803").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Faculty).HasMaxLength(200);
            entity.Property(e => e.Grade).HasMaxLength(20);
            entity.Property(e => e.HousingType).HasMaxLength(50);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.NationalId)
                .HasMaxLength(20)
                .HasColumnName("NationalID");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.StudentCode).HasMaxLength(50);
            entity.Property(e => e.StudyType).HasMaxLength(50);
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniversityId).HasName("PK__Universi__9F19E19C8CF24B10");

            entity.ToTable("University");

            entity.Property(e => e.UniversityId).HasColumnName("UniversityID");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Governorate).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UniversityName).HasMaxLength(200);
        });

        modelBuilder.Entity<Violation>(entity =>
        {
            entity.HasKey(e => e.ViolationId).HasName("PK__Violatio__18B6DC28D2313A70");

            entity.ToTable("Violation");

            entity.HasIndex(e => e.StudentId, "IDX_Violation_StudentID");

            entity.Property(e => e.ViolationId).HasColumnName("ViolationID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsPaid).HasDefaultValue(false);
            entity.Property(e => e.Penalty).HasMaxLength(50);
            entity.Property(e => e.PenaltyAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.ViolationType).HasMaxLength(100);

            entity.HasOne(d => d.RecordedByNavigation).WithMany(p => p.Violations)
                .HasForeignKey(d => d.RecordedBy)
                .HasConstraintName("FK__Violation__Recor__208CD6FA");

            entity.HasOne(d => d.Student).WithMany(p => p.Violations)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Violation__Stude__1F98B2C1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
