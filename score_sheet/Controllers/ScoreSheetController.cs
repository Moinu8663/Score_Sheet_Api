using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using score_sheet.Exceptions;
using score_sheet.Model;
using score_sheet.Services;

namespace score_sheet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScoreSheetController : ControllerBase
    {
        private readonly Iservice iservice;
        private readonly ITokenGenerator tokenGenerator;
        public ScoreSheetController(Iservice iservice, ITokenGenerator tokenGenerator)
        {
            this.iservice=iservice;
            this.tokenGenerator=tokenGenerator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(iservice.GetAll());
        }

        [HttpGet("{RollNo}")]
        public IActionResult Get(int RollNo)
        {
            
            try
            {
                return Ok(iservice.GetScoreByRollNo(RollNo));
            }
            catch (ScoreNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddScore")]
        public IActionResult Post([FromBody] ScoreSheet scoreSheet)
        {
            
            try
            {
                iservice.AddScore(scoreSheet);
                return StatusCode(200, "New Score Added Successfully");
            }
            catch(ScoreAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("GetResult")]
        public IActionResult GetResult([FromBody] ScoreSheet scoreSheet)
        {
            var obj = iservice.GetResult(scoreSheet);
            if (obj != null)
            {
                return Ok(tokenGenerator.GenerateToken(scoreSheet));
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        [HttpPut("{RollNo}")]
        public IActionResult Put(ScoreSheet scoreSheet, int RollNo)
        {
            try
            {
                iservice.UpdateScore(RollNo, scoreSheet);
                return StatusCode(200, "Score Updated Successfully");
            }
            catch (ScoreNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{RollNo}")]
        public IActionResult Delete(int RollNo)
        {
            
            try
            {
                iservice.DeleteScore(RollNo);
                return StatusCode(200, "Score Deleted Successfully");
            }
            catch (ScoreNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
