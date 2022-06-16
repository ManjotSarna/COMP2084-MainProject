using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using COMP2084_MAINPROJECT.Models;

namespace COMP2084_MAINPROJECT.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<COMP2084_MAINPROJECT.Models.Recipe>? Recipe { get; set; }
    public DbSet<COMP2084_MAINPROJECT.Models.Origin>? Origin { get; set; }
}
