using System.Diagnostics.CodeAnalysis;
using EnglishWordsNoteBook.DTO.Section;
using EnglishWordsNoteBook.DTO.Word;
using EnglishWordsNoteBook.Models;
using EnglishWordsNoteBook.Repositories.Interfaces;
using EnglishWordsNoteBook.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnglishWordsNoteBook.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public WordService(IWordRepository wordRepository,
            UserManager<ApplicationUser> userManager)
        {
            _wordRepository = wordRepository;
            _userManager = userManager;
        }

        public async Task<ReturnWordDTO?> AddAsync(AddWordDTO wordDTO)
        {
            if (wordDTO.SectionId < 1)
                throw new Exception("SectionId Can't be less than 1");

            var word = new Word()
            {
                WordValue = wordDTO.WordValue,
                Translation = wordDTO.Translation,
                PronunciationAudioFileUrl = wordDTO.PronunciationAudioFileUrl,
                SectionId = wordDTO.SectionId ?? 1,
                Date = DateTime.Now,
            };

            var result = await _wordRepository.AddAsync(word);
            result = result && await _wordRepository.SaveAsync();

            if (!result)
            {
                throw new Exception("Problem Happen When Saving Word Row In Database");
            }

            var returnWord = new ReturnWordDTO()
            {
                Id = word.Id,
                WordValue = word.WordValue,
                Translation = word.Translation,
                Date = word.Date,
                PronunciationAudioFileUrl = word.PronunciationAudioFileUrl,
                SectionId = word.SectionId
            };

            return returnWord;
        }

        private async Task<string> SavePronounciationAudioFile(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new Exception();
            }

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Audio", "WordPronounciations");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"/Audio/WordPronounciations/{fileName}";

            return fileUrl;
        }
        public async Task<string?> UpdatePronounciationAudioFileByWordId(int Id,
            UploadPronounciationDTO PronounciationDTO)
        {
            string? result = null;

            try
            {
                var fileUrl = await SavePronounciationAudioFile(PronounciationDTO.PronounciationAudioFile);
                await _wordRepository.UpdatePronounciationAudioUrlByWordId(Id, fileUrl);
                await _wordRepository.SaveAsync();

                result = fileUrl;
            }
            catch (Exception ex) {
                result = null;
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<ReturnWordDTO?> UpdateAsync(int userId, UpdateWordDTO wordDTO)
        {
            var word = await _wordRepository.GetByIdAsync(userId);

            if (word == null) return null;

            word.WordValue = wordDTO.WordValue;
            word.Translation = wordDTO.Translation;
            word.SectionId = wordDTO.SectionId ?? 1;


            var result = await _wordRepository.UpdateAsync(word);
            result = result && await _wordRepository.SaveAsync();

            if (result)
            {
                var returnWord = new ReturnWordDTO()
                {
                    Id = word.Id,
                    WordValue = word.WordValue,
                    Translation = word.Translation,
                    Date = word.Date,
                    PronunciationAudioFileUrl = word.PronunciationAudioFileUrl
                };

                return returnWord;
            }

            return null;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _wordRepository.DeleteAsync(id) && await _wordRepository.SaveAsync();
        }
        public async Task<List<ReturnWordDTO>> GetAllAsync()
        {
            var words = await _wordRepository.GetAllAsync();
            var returnWordDTOs = new List<ReturnWordDTO>();

            foreach (var word in words)
            {
                returnWordDTOs.Add(new ReturnWordDTO()
                {
                    Id = word.Id,
                    WordValue = word.WordValue,
                    Translation = word.Translation,
                    Date = word.Date,
                    PronunciationAudioFileUrl = word.PronunciationAudioFileUrl
                });
            }

            return returnWordDTOs;
        }
        public async Task<ReturnWordDTO?> GetByIdAsync(int id)
        {
            var word = await _wordRepository.GetByIdAsync(id);
            if (word == null) 
                throw new Exception("There is no word with this id");

            var returnWordDTO = new ReturnWordDTO()
            {
                Id = word.Id,
                WordValue = word.WordValue,
                Translation = word.Translation,
                Date = word.Date,
                PronunciationAudioFileUrl = word.PronunciationAudioFileUrl,
                SectionId = word.SectionId
            };

            return returnWordDTO;
        }
        public async Task<int> CountByUserIdAsync(int id)
        {
            return await _wordRepository.CountByUserIdAsync(id);
        }
        public async Task<List<ReturnWordDTO>> GetPageAsync(int skip, int take, int userId)
        {
            var words = await _wordRepository.GetPageAsync(skip, take, userId);
            var returnWordDTOs = new List<ReturnWordDTO>();

            foreach (var word in words)
            {
                returnWordDTOs.Add(new ReturnWordDTO()
                {
                    Id = word.Id,
                    WordValue = word.WordValue,
                    Translation = word.Translation,
                    Date = word.Date,
                    PronunciationAudioFileUrl = word.PronunciationAudioFileUrl
                });
            }

            return returnWordDTOs;
        }
        public async Task<List<ReturnWordDTO>> GetByUserIdAsync(int id)
        {
            var words = await _wordRepository.GetByUserIdAsync(id);
            var returnWordDTOs = new List<ReturnWordDTO>();

            foreach (var word in words)
            {
                returnWordDTOs.Add(new ReturnWordDTO()
                {
                    Id = word.Id,
                    WordValue = word.WordValue,
                    Translation = word.Translation,
                    Date = word.Date,
                    PronunciationAudioFileUrl = word.PronunciationAudioFileUrl
                });
            }

            return returnWordDTOs;
        }

        public async Task<List<ReturnWordDTO>> GetBySectionIdAsync(int id)
        {
            var words = await _wordRepository.GetBySectionIdAsync(id);
            var returnWordDTOs = new List<ReturnWordDTO>();

            foreach (var word in words)
            {
                returnWordDTOs.Add(new ReturnWordDTO()
                {
                    Id = word.Id,
                    WordValue = word.WordValue,
                    Translation = word.Translation,
                    Date = word.Date,
                    PronunciationAudioFileUrl = word.PronunciationAudioFileUrl
                });
            }

            return returnWordDTOs;
        }
        public async Task<int> CountBySectionIdAsync(int id)
        {
            return await _wordRepository.CountBySectionIdAsync(id);
        }
        public async Task<List<ReturnWordDTO>> SearchByUserAndSubstringAsync(int id, string substr)
        {
            if(await _userManager.FindByIdAsync(id.ToString()) == null)
            {
                throw new Exception("User Was Not Found");
            }
            var words = await _wordRepository.SearchByUserAndSubstringAsync(id, substr);
            var returnWordDTOs = new List<ReturnWordDTO>();

            foreach (var word in words)
            {
                returnWordDTOs.Add(new ReturnWordDTO()
                {
                    Id = word.Id,
                    WordValue = word.WordValue,
                    Translation = word.Translation,
                    Date = word.Date,
                    PronunciationAudioFileUrl = word.PronunciationAudioFileUrl
                });
            }

            return returnWordDTOs;
        }
        public async Task<List<ReturnWordDTO>> GetWordsWithoutPronunciationByUserId(int id)
        {
            var words = await _wordRepository.GetWordsWithoutPronunciationByUserId(id);
            var returnWordDTOs = new List<ReturnWordDTO>();

            foreach (var word in words)
            {
                returnWordDTOs.Add(new ReturnWordDTO()
                {
                    Id = word.Id,
                    WordValue = word.WordValue,
                    Translation = word.Translation,
                    Date = word.Date,
                    PronunciationAudioFileUrl = word.PronunciationAudioFileUrl
                });
            }

            return returnWordDTOs;
        }

    }
}
