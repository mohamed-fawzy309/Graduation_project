using System;
using System.Collections.Generic;

namespace unistay.Models;

public partial class Meal
{
    public int MealId { get; set; }

    public int? StudentId { get; set; }

    public DateOnly? MealDate { get; set; }

    public string? MealType { get; set; }

    public string? Status { get; set; }

    public DateTime? TakenAt { get; set; }

    public decimal? MissedPenalty { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Student? Student { get; set; }
}
