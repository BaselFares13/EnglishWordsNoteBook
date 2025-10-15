using System.Security.Claims;
using EnglishWordsNoteBook.DTO;
using EnglishWordsNoteBook.DTO.Section;
using EnglishWordsNoteBook.Services;
using EnglishWordsNoteBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishWordsNoteBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSection(AddSectionDTO sectionDTO)
        {
            var res = new GeneralResponse();

            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var section = await _sectionService.AddAsync(Convert.ToInt32(userId), sectionDTO);

            res.Success = true;
            res.Message = "Section have been created successfully";
            res.Data = section;
            res.Errors = null;

            return CreatedAtAction(
                 nameof(GetById),
                 new { Id = section.Id }, 
                 section
             );
        }

        [HttpPut("{Id:int:min(1)}")] 
        public async Task<IActionResult> UpdateSection(int Id, UpdateSectionDTO sectionDTO)
        {
            var res = new GeneralResponse();

            var section = await _sectionService.UpdateAsync(Id, sectionDTO);

            res.Success = true;
            res.Message = "Section have been Updated successfully";
            res.Data = section;
            res.Errors = null;

            return Ok(res);
        }

        [HttpDelete("{Id:int:min(1)}")]
        public async Task<IActionResult> DeleteSection(int Id)
        {
            var res = new GeneralResponse();

            var result = await _sectionService.DeleteAsync(Id);

            res.Success = result;
            res.Message = "Section have been Deleted successfully";
            res.Data = null;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("{Id:int:min(1)}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var res = new GeneralResponse();

            var section = await _sectionService.GetByIdAsync(Id);

            res.Success = true;
            res.Message = "Section have been received successfully";
            res.Data = section;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> CountByUserId()
        {
            var res = new GeneralResponse();

            var Id =  User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var Count = await _sectionService.CountByUserIdAsync(Convert.ToInt32(Id));

            res.Success = true;
            res.Message = "Request Done Successfully";
            res.Data = Count;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("Page")]
        public async Task<IActionResult> GetPage([FromQuery] GetPageDTO pageDTO)
        {
            var res = new GeneralResponse();

            var Id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var sections = await _sectionService.GetPageAsync(((pageDTO.PageNum - 1)* pageDTO.ItemsPerPage), pageDTO.ItemsPerPage, Convert.ToInt32(Id));

            res.Success = true;
            res.Message = "Sections have been received successfully";
            res.Data = sections;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId()
        {
            var res = new GeneralResponse();

            var Id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var sections = await _sectionService.GetByUserIdAsync(Convert.ToInt32(Id));

            res.Success = true;
            res.Message = "Sections have been received successfully";
            res.Data = sections;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var res = new GeneralResponse();


            var sections = await _sectionService.GetAllAsync();

            res.Success = true;
            res.Message = "Sections have been received successfully";
            res.Data = sections;
            res.Errors = null;

            return Ok(res);
        }


    }
}
