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
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddUserDTO userDto)
        {
            var res = new GeneralResponse();

            var result = await _userService.AddAsync(userDto);

            res.Success = true;
            res.Message = "Added Successfully";
            res.Data = result;
            res.Errors = null;

            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO userDto)
        {
            var res = new GeneralResponse();

            var result = await _userService.UpdateAsync(userDto);

            res.Success = true;
            res.Message = "Updated Successfully";
            res.Data = result;
            res.Errors = null;

            return Ok(res);     
        }

        [HttpDelete("{Id:int:min(1)}")] 
        public async Task<IActionResult> DeleteUser(int Id)
        {
            if(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value == Id.ToString())
            {
                throw new Exception("You Can't Delete Yourself");
            }

            var res = new GeneralResponse();

            var result = await _userService.DeleteAsync(Id);

            res.Success = true;
            res.Message = "Deleted Successfully";
            res.Data = null;
            res.Errors = null;

            return Ok(res);
        }
    }
}
