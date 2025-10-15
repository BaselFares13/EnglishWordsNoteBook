using EnglishWordsNoteBook.DTO;
using System.Security.Claims;
using EnglishWordsNoteBook.DTO.Word;
using EnglishWordsNoteBook.Services;
using EnglishWordsNoteBook.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EnglishWordsNoteBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;
        private readonly ISectionService _sectionService;

        public WordController(IWordService wordService, ISectionService sectionService)
        {
            _wordService = wordService;
            _sectionService = sectionService;
        }

        [HttpGet("{Id:int:min(1)}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var res = new GeneralResponse();

            var word = await _wordService.GetByIdAsync(Id);

            res.Success = true;
            res.Message = "Word have been received successfully";
            res.Data = word;
            res.Errors = null;

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWordDTO wordDTO)
        {
            var res = new GeneralResponse();

            var word = await _wordService.AddAsync(wordDTO);

            res.Success = true;
            res.Message = "Word have been created successfully";
            res.Data = word;
            res.Errors = null;

            return CreatedAtAction(
                 nameof(GetById),
                 new { Id = word.Id },
                 res
            );
        }

        [HttpPut("{Id:int:min(1)}")]
        public async Task<IActionResult> Update(int Id, UpdateWordDTO wordDTO)
        {
            var res = new GeneralResponse();

            var word = await _wordService.UpdateAsync(Id, wordDTO);

            res.Success = true;
            res.Message = "Word have been updated successfully";
            res.Data = word;
            res.Errors = null;

            return Ok(res);
        }

        [HttpDelete("{Id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id )
        {
            var res = new GeneralResponse();

            var word = await _wordService.DeleteAsync(id);

            if(!word)
            {
                throw new Exception("There is a problem happend while deleting a word");
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = new GeneralResponse();

            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var words = await _wordService.GetByUserIdAsync(Convert.ToInt32(userId));

            res.Success = true;
            res.Message = "Words have been recieved successfully";
            res.Data = words;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> CountAll()
        {
            var res = new GeneralResponse();

            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var words = await _wordService.CountByUserIdAsync(Convert.ToInt32(userId));

            res.Success = true;
            res.Message = "Words have been counted successfully";
            res.Data = words;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("Page")]
        public async Task<IActionResult> GetPage([FromQuery] GetPageDTO pageDTO)
        {
            var res = new GeneralResponse();

            var Id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var words = await _wordService.GetPageAsync(((pageDTO.PageNum - 1) * pageDTO.ItemsPerPage), pageDTO.ItemsPerPage, Convert.ToInt32(Id));

            res.Success = true;
            res.Message = "Words have been received successfully";
            res.Data = words;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("Section/{Id:int:min(1)}")]
        public async Task<IActionResult> GetWordsBySectionId(int Id)
        {
            await _sectionService.GetByIdAsync(Id);

            var res = new GeneralResponse();
            
            var words = await _wordService.GetBySectionIdAsync(Id);

            res.Success = true;
            res.Message = "Words have been received successfully";
            res.Data = words;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("Section/{Id:int:min(1)}/Count")]
        public async Task<IActionResult> CountWordsBySectionId(int Id)
        {
            await _sectionService.GetByIdAsync(Id);

            var res = new GeneralResponse();

            var wordsCount = await _wordService.CountBySectionIdAsync(Id);

            res.Success = true;
            res.Message = "Words have been counted successfully";
            res.Data = wordsCount;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchForWords([FromQuery] string WordPart)
        {
            if (WordPart == null)
                throw new NullReferenceException("WordPart Parameter Can't Be Null");

            var res = new GeneralResponse();

            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var words = await _wordService.SearchByUserAndSubstringAsync(Convert.ToInt32(userId), WordPart);

            res.Success = true;
            res.Message = "There Are Words Found";
            res.Data = words;
            res.Errors = null;

            return Ok(res);
        }

        [HttpGet("NoPronunciation")]
        public async Task<IActionResult> GetWordsWithoutPronunciationByUserId()
        {
            var res = new GeneralResponse();

            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var words = await _wordService.GetWordsWithoutPronunciationByUserId(Convert.ToInt32(userId));

            res.Success = true;
            res.Message = "There Are Words Found";
            res.Data = words;
            res.Errors = null;

            return Ok(res);
        }

        [HttpPost("{Id:int:min(1)}/Upload-Pronunciation")]
        public async Task<IActionResult> UploadPronunciationAudioFile(
            int Id,
            [FromForm]  UploadPronounciationDTO pronounciationDTO)
        {
            var res = new GeneralResponse();

            var url = await _wordService.UpdatePronounciationAudioFileByWordId(Id, 
                pronounciationDTO);

            if (url == null)
            {
                throw new Exception("Audio was not saved successfully");
            }

            res.Success = true;
            res.Message = "Audio saved successfully";
            res.Data = url;
            res.Errors = null;

            return Ok(res);
        }

    }
}
