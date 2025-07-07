using Microsoft.EntityFrameworkCore;
using MudBlazorGoogleAuth.Data;
using MudBlazorGoogleAuth.Model;

namespace MudBlazorGoogleAuth.Services;

public interface IUserService
{
    Task CreateAsync(User user, AccountLink accountLink);
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<AccountLink?> GetAccountLinkByIdAsync(string id);
    Task<User?> GetUserByProviderIdAsync(string id);
}
public class UserService(MudBlazorGoogleAuthContext context) : IUserService
{
    public MudBlazorGoogleAuthContext Context { get; } = context;

    public async Task CreateAsync(User user, AccountLink accountLink)
    {
        await Context.Users.AddAsync(user);
        await Context.AccountLinks.AddAsync(accountLink);
        await Context.SaveChangesAsync();
    }
    
    public async Task<List<User>> GetUsersAsync()
    {
        List<User> result =  await Context.Users.ToListAsync();
        await Context.SaveChangesAsync();
        return result;
    }
    
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        User? result = await Context.Users.FindAsync(id);
        if (result is null)
        {
            
            return null;
        }
        await Context.SaveChangesAsync();
        return result;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? result = await Context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return result;
    }
    public async Task<User?> GetUserByProviderIdAsync(string id)
    {
        AccountLink? accountLink = await GetAccountLinkByIdAsync(id);
        if (accountLink is null)
        {
            return null;
        }
        User? result = await Context.Users.FirstOrDefaultAsync(x => x.Id == accountLink.UserId);
        return result;
    }

    public async Task<AccountLink?> GetAccountLinkByIdAsync(string id)
    {
        AccountLink? accountLink = await Context.AccountLinks.FirstOrDefaultAsync(x => x.Id == id);
        return accountLink;
    }
    
    public async Task UpdateAsync(User user)
    {
        Context.Users.Update(user);
        await Context.SaveChangesAsync();
    }
    public async Task DeleteAsync(User user)
    {
        Context.Users.Remove(user);
        await Context.SaveChangesAsync();   
    }
}