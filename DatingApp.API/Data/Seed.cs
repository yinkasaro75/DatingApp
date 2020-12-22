using System.Collections.Generic;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
  public class Seed
  {
    private readonly DataContext _context;
    public Seed(DataContext context)
    {
      _context = context;

    }

    public void SeedUsers()
    {
        var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
        var users = JsonConvert.DeserializeObject<List<User>>(userData);
        foreach (var user in users  )
        {
            byte [] passwordHash , passwordSalt;
            CreatePassWord("password",out passwordHash, out passwordSalt);
            user.PassWordHash = passwordHash;
            user.PassWordSalt = passwordSalt;
            user.UserName = user.UserName.ToLower();
     _context.Users.Add(user);       

        }

        _context.SaveChanges();
    }

     private void CreatePassWord(string password, out byte[] passWordHash, out byte[] passWordSalt)
    {
        using(var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passWordSalt = hmac.Key;
            passWordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));



        }
      
    }
  }
}