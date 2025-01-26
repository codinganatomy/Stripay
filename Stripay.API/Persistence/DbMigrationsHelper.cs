using Microsoft.EntityFrameworkCore;

namespace Stripay.API.Persistence;

public class DbMigrationsHelper(ILogger<DbMigrationsHelper> logger, AppDbContext context) {
    private readonly ILogger<DbMigrationsHelper> _logger = logger;
    private readonly AppDbContext _context = context;

    public async Task ApplyMigrationsAsync() {
        try {
            if(_context.Database.IsSqlServer() && (await _context.Database.GetPendingMigrationsAsync()).Any()) {
                _logger.LogInformation("Applying migrations...");
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Migrations applied successfully.");
            }
        } catch (Exception ex) {
            _logger.LogError(ex, "An error occurred while applying migrations.");
        }
    }
 }
