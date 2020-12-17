using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
  public class AuthRepository : IAuthRepository
  {
    private DataContext _context;
    public AuthRepository(DataContext context)
    {
      _context = context;

    }
    public async Task<User> Login(string username, string password)
    {
      var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
      if(user == null)
      return null;
      if(!VerifyPasswordHash(password,user.PassWordHash,user.PassWordSalt))
        return null;

        return user;
    }

    private bool VerifyPasswordHash(string password, byte[] passWordHash, byte[] passWordSalt)
    {
      using(var hmac = new System.Security.Cryptography.HMACSHA512(passWordSalt))
        {
            var compuHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for(int i=1; i < compuHash.Length ; i++)
            {
                if(compuHash[i] != passWordHash[i]) return false;
            }
        }
        return true;
    }

    public async Task<User> Register(User user, string password)
    {
      byte[] passWordHash,passWordSalt;
      CreatePassWord(password,out passWordHash,out passWordSalt);
      user.PassWordHash = passWordHash;
      user.PassWordSalt = passWordSalt;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();

      return user;

    }

    private void CreatePassWord(string password, out byte[] passWordHash, out byte[] passWordSalt)
    {
        using(var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passWordSalt = hmac.Key;
            passWordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));



        }
      
    }

    public async Task<bool> UserExist(string userName)
    {
      if(await _context.Users.AnyAsync(x => x.UserName == userName))
      return true;

      return false;
     
    }
  }
}