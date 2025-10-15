using EnglishWordsNoteBook.DTO.Section;
using EnglishWordsNoteBook.Models;
using EnglishWordsNoteBook.Repositories.Interfaces;
using EnglishWordsNoteBook.Services.Interfaces;

namespace EnglishWordsNoteBook.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<ReturnSectionDTO?> AddAsync(int UserId, AddSectionDTO sectionDTO)
        {
            var section = new Section()
            {
                Title = sectionDTO.Title,
                Notes = sectionDTO.Notes,
                Date = DateTime.Now,
                UserId = UserId
            };

            var result = await _sectionRepository.AddAsync(section);
            result = result && await _sectionRepository.SaveAsync();

            if (result)
            {
                var returnSection = new ReturnSectionDTO()
                {
                    Id = section.Id,
                    Title = section.Title,
                    Notes = section.Notes,
                    Date = section.Date
                };

                return returnSection;
            }

            return null;
        }
        public async Task<ReturnSectionDTO?> UpdateAsync(int UserId, UpdateSectionDTO sectionDTO)
        {
            var section = await _sectionRepository.GetByIdAsync(UserId);

            if (section == null) return null;

            section.Notes = sectionDTO.Notes;
            section.Title = sectionDTO.Title;

            var result =  await _sectionRepository.UpdateAsync(section);
            result = result && await _sectionRepository.SaveAsync();

            if (result) {
                var returnSection = new ReturnSectionDTO()
                {
                    Id = section.Id,
                    Title = section.Title,
                    Notes = section.Notes,
                    Date = section.Date
                };

                return returnSection;
            }

            return null;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _sectionRepository.DeleteAsync(id) && await _sectionRepository.SaveAsync();
        }
        public async Task<List<ReturnSectionDTO>> GetAllAsync()
        {
            var sections = await _sectionRepository.GetAllAsync();
            var returnSectionDTOs = new List<ReturnSectionDTO>();

            foreach (var section in sections) {
                returnSectionDTOs.Add(new ReturnSectionDTO() { 
                    Id = section.Id,
                    Title = section.Title,
                    Notes = section.Notes,
                    Date = section.Date
                });
            }

            return returnSectionDTOs;
        }
        public async Task<ReturnSectionDTO?> GetByIdAsync(int id)
        {
            var section = await _sectionRepository.GetByIdAsync(id);
            if (section == null)
            {
                throw new Exception("There is no section with this Id");
            };

            var returnSectionDTO = new ReturnSectionDTO() {
                Id = section.Id,    
                Title = section.Title,
                Notes = section.Notes,
                Date = section.Date
            };

            return returnSectionDTO;
        }
        public async Task<int> CountByUserIdAsync(int id)
        {
            return await _sectionRepository.CountByUserIdAsync(id);
        }
        public async Task<List<ReturnSectionDTO>> GetPageAsync(int skip, int take, int userId)
        {
            var sections = await _sectionRepository.GetPageAsync(skip, take, userId);
            var returnSectionDTOs = new List<ReturnSectionDTO>();

            foreach (var section in sections)
            {
                returnSectionDTOs.Add(new ReturnSectionDTO()
                {
                    Id = section.Id,
                    Title = section.Title,
                    Notes = section.Notes,
                    Date = section.Date
                });
            }

            return returnSectionDTOs;
        }
        public async Task<List<ReturnSectionDTO>> GetByUserIdAsync(int id)
        {
            var sections = await _sectionRepository.GetByUserIdAsync(id);
            var returnSectionDTOs = new List<ReturnSectionDTO>();

            foreach (var section in sections)
            {
                returnSectionDTOs.Add(new ReturnSectionDTO()
                {
                    Id = section.Id,
                    Title = section.Title,
                    Notes = section.Notes,
                    Date = section.Date
                });
            }

            return returnSectionDTOs;
        }

    }
}
