using EnglishWordsNoteBook.DTO.User;
using EnglishWordsNoteBook.Models;
using EnglishWordsNoteBook.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EnglishWordsNoteBook.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ReturnedUserDTO?> AddAsync(AddUserDTO userDto)
        {
            var user = new ApplicationUser()
            {
                UserName = userDto.Username,
                Email = userDto.Email
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded) {
                throw new Exception(String.Join(", ", result.Errors.Select(e => e.Description).ToList()));
            }
            await _userManager.AddToRoleAsync(user, userDto.RoleName);


            var returnedUser = new ReturnedUserDTO()
            {
                id = user.Id,
                Username = userDto.Username,
                Email = userDto.Email
            };

            return returnedUser;
        }
        public async Task<ReturnedUserDTO?> UpdateAsync(UpdateUserDTO userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            if (user == null)
            {
                throw new Exception("There is no user with this id");
            };

            user.UserName = userDto.Username;
            user.Email = userDto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(String.Join(", ", result.Errors.Select(e => e.Description).ToList()));
            };

            if (!String.IsNullOrWhiteSpace(userDto.Password) && !String.IsNullOrWhiteSpace(userDto.NewPassword)) {
                var result2 = await _userManager.ChangePasswordAsync(user, userDto.Password, userDto.NewPassword);
                if (!result2.Succeeded) {
                    throw new Exception(String.Join(", ", result2.Errors.Select(e => e.Description).ToList()));
                }
            }            

            return new ReturnedUserDTO()
            {
                id = user.Id,
                Username = userDto.Username,
                Email = userDto.Email
            };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new Exception("There is no user with this id");
            };

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(String.Join(", ", result.Errors.Select(e => e.Description).ToList()));
            };

            return true;
        }
    }
}