using System;
using System.Collections.Generic;

namespace unistay.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? StudentCode { get; set; }

    public string? Name { get; set; }

    public string? NationalId { get; set; }

    public string? Faculty { get; set; }

    public string? Address { get; set; }

    public double? DistanceFromHome { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? StudyType { get; set; }

    public string? HousingType { get; set; }

    public string? Grade { get; set; }

    public double? GradePercentage { get; set; }

    public bool? DrugTestPassed { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Allocation? Allocation { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<EvictionNotice> EvictionNotices { get; set; } = new List<EvictionNotice>();

    public virtual ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();

    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();

    public string? PasswordHash { get; set; }

}
