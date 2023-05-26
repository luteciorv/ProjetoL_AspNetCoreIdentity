using AspNetCoreIdentity.Application.DTOs.Student;
using AspNetCoreIdentity.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentity.WebApi.Controllers
{
    [Route("api/v1/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadStudentDto>> Get()
        {
            var students = _studentService.GetAll();
            if (students is null)
                return NotFound();

            return Ok(students);
        }
            

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadStudentDto>> GetById(Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student is null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateStudentDto studentDto)
        {
            var readStudentDto = await _studentService.CreateAsync(studentDto);
            return CreatedAtAction(nameof(GetById), new {id =  readStudentDto.Id}, readStudentDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(
            Guid id, 
            [FromBody] EditStudentDto studentDto
        )
        {
            await _studentService.UpdateAsync(id, studentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _studentService.DeleteById(id);
            return NoContent();
        }
    }
}
