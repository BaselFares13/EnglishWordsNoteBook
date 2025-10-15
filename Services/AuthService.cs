using System.Linq;
using Azure;
using EnglishWordsNoteBook.DTO.User;
using EnglishWordsNoteBook.HelpingClasses;
using EnglishWordsNoteBook.Models;
using EnglishWordsNoteBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EnglishWordsNoteBook.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        public IConfiguration _config { get; }

        public AuthService(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService, IConfiguration config) { 
            _userManager = userManager;
            _jwtService = jwtService;
            _config = config;
        }
        public async Task<bool> Regsiter(AddUserDTO userDTO)
        {
            var user = new ApplicationUser();
            user.Email = userDTO.Email;
            user.UserName = userDTO.Username;


            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded) throw new Exception(String.Join(", ", result.Errors.Select(e => e.Description).ToList()));
            
            await _userManager.AddToRoleAsync(user, userDTO.RoleName);

            return true;
        }
        public async Task<ReturnAuthDTO> Login(LoginUserDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user == null) return new ReturnAuthDTO()
            {
                Success = false,
                Message = "There Is No User With This Email",
                Tokens = null
            }; 

            var isMatched = await _userManager.CheckPasswordAsync(user, userDTO.Password);
            if (!isMatched) return new ReturnAuthDTO()
            {
                Success = false,
                Message = "Password Is Incorrect",
                Tokens = null
            };

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationTime = DateTime.UtcNow.AddDays(double.Parse(_config["Jwt:ExpiryInDays"] ?? "7"));

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return new ReturnAuthDTO()
            {
                Success = false,
                Message = "Problem Happend While Saving To Database",
                Tokens = null
            };

            return new ReturnAuthDTO()
            {
                Success = true,
                Message = "User Login Successfully",
                Tokens = new TokensDTO()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpirationTime = user.RefreshTokenExpirationTime ?? DateTime.Now
                }
            } ;
        }

        [Authorize]
        public async Task<bool> Logout(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpirationTime = null;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;

            return true;
        }
        public async Task<ReturnAuthDTO> Refresh(string refreshToken)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpirationTime <= DateTime.Now) 
                return new ReturnAuthDTO() { 
                    Success = false,
                    Message = "Invalid Or Expired Refresh Token",
                    Tokens = null
                };

            var roles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _jwtService.GenerateAccessToken(user, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpirationTime = DateTime.UtcNow.AddDays(double.Parse(_config["Jwt:ExpiryInDays"] ?? "7"));
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return new ReturnAuthDTO()
            {
                Success = false,
                Message = "Problem Happend While Saving To Database",
                Tokens = null
            };


            return new ReturnAuthDTO() {
                Success = true,
                Message = "Tokens Refreshed Successfully",
                Tokens = new TokensDTO()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    RefreshTokenExpirationTime = user.RefreshTokenExpirationTime ?? DateTime.Now
                }
            };
        }
    }
}
