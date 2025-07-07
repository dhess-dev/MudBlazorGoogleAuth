using Microsoft.EntityFrameworkCore;
using MudBlazorGoogleAuth.Model;

namespace MudBlazorGoogleAuth.Data;

public class MudBlazorGoogleAuthContext(DbContextOptions<MudBlazorGoogleAuthContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<AccountLink> AccountLinks { get; set; }
}