using AskMate.Model;
using AskMate.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace AskMate.Controllers;

[ApiController]
[Route("/api/User")]
public class UserController : ControllerBase
{
    private readonly string _connectionString = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build()
        .GetConnectionString("Default");

    [HttpPost()]
    public IActionResult Create(User user)
    {
        var repository = new UserRepository(new NpgsqlConnection(_connectionString));
        return Ok(repository.Create(user));
    }

    [HttpPost("api/User/Login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var repository = new UserRepository(new NpgsqlConnection(_connectionString));
        // var isAuthenticated = repository.AuthenticateUser(user.Username, user.Password);
        var user = repository.AuthenticateUser(username, password);
        if (user is not null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
                // new Claim(ClaimTypes.Name, username),
                // new Claim(ClaimTypes.Email, password),
                // add or remove claims as necessary    
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
            };
            var principal = new ClaimsPrincipal(claimsIdentity);
            // principal.Identity.IsAuthenticated = true;
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                principal,
                authProperties);
        }
        

        return Ok();
    }
    
    [HttpPost("api/User/LogOut")]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}