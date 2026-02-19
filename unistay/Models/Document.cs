using System;
using System.Collections.Generic;

namespace unistay.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public int? ApplicationId { get; set; }

    public string? DocumentType { get; set; }

    public string? FilePath { get; set; }

    public DateTime? UploadedAt { get; set; }

    public int? VerifiedBy { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public bool? IsVerified { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Application? Application { get; set; }

    public virtual Admin? VerifiedByNavigation { get; set; }
}
