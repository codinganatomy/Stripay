using Microsoft.EntityFrameworkCore;
using Stripay.API.Models;

namespace Stripay.API.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Payment> Payments { get; set; }
}
