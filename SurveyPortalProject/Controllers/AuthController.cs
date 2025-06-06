﻿//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using SurveyPortalAPI.Models; // AppDbContext burada

//namespace SurveyPortalAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly IConfiguration _configuration;

//        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _configuration = configuration;
//        }

//        // POST: api/auth/register
//        [HttpPost("register")]
//        [AllowAnonymous]
//        public async Task<IActionResult> Register([FromBody] RegisterModel model)
//        {
//            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
//            var result = await _userManager.CreateAsync(user, model.Password);

//            if (result.Succeeded)
//            {
//                // Kullanıcıya "User" rolü ekleyebilirsin istersen
//                // await _userManager.AddToRoleAsync(user, "User");

//                return Ok(new { message = "Kayıt başarılı!" });
//            }

//            return BadRequest(result.Errors);
//        }

//        // POST: api/auth/login
//        [HttpPost("login")]
//        [AllowAnonymous]
//        public async Task<IActionResult> Login([FromBody] LoginModel model)
//        {
//            var user = await _userManager.FindByEmailAsync(model.Email);
//            if (user == null) return Unauthorized("Geçersiz kullanıcı");

//            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
//            if (!result.Succeeded) return Unauthorized("Geçersiz şifre");

//            var token = GenerateJwtToken(user);
//            return Ok(new { token });
//        }

//        private string GenerateJwtToken(IdentityUser user)
//        {
//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim(ClaimTypes.NameIdentifier, user.Id)
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddHours(2),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
