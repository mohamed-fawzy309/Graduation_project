using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using unistay.Models; // لو Models موجودة هنا

namespace unistay.Data
{
    public class UniStayDbContext : DbContext
    {
        public UniStayDbContext(DbContextOptions<UniStayDbContext> options)
            : base(options)
        {
        }

        
    }
}
