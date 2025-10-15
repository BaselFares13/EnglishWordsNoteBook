using System.Security.Claims;
using EnglishWordsNoteBook.DTO;
using EnglishWordsNoteBook.DTO.User;
using EnglishWordsNoteBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace EnglishWordsNoteBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AddUserDTO userDTO)
        {
            
            var res = new GeneralResponse();
       
            await _authService.Regsiter(userDTO);

            res.Success = true;
            res.Message = "User Register Successfully";
            res.Data = null;
            res.Errors = null;

            return Ok(res);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            var res = new GeneralResponse();
     
            var result = await _authService.Login(userDTO);
            if (!result.Success)
                throw new Exception(result.Message);

            Response.Cookies.Append("refreshToken", result.Tokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.Tokens.RefreshTokenExpirationTime
            });


            res.Success = true;
            res.Message = "User Login Successfully";
            res.Data = new
            {
                accessToken = result.Tokens.AccessToken,
            };
            res.Errors = null;
      
            return Ok(res);
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var res = new GeneralResponse();
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
            var result = await _authService.Logout(Convert.ToInt32(userId));
            if (!result)
                throw new Exception("Something Went Wrong");

            Response.Cookies.Delete("refreshToken");

            return NoContent();
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            var res = new GeneralResponse();

            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                throw new Exception("No refresh token provided");

         
            var result = await _authService.Refresh(refreshToken);
                
            if (!result.Success)
                throw new Exception(result.Message);

            Response.Cookies.Append("refreshToken", result.Tokens.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.Tokens.RefreshTokenExpirationTime

            });

            res.Success = true;
            res.Message = "Tokens Refreshed Successfully";
            res.Data = new
            {
                accessToken = result.Tokens.AccessToken
            };
            res.Errors = null;
            
            return Ok(res);
        }

    }
}
