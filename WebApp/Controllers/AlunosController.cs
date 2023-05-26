using AspNetCoreIdentity.Application.DTOs.Student;
using AspNetCoreIdentity.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("alunos")]
    [Authorize]
    public class AlunosController : Controller
    {
        [Route(""), HttpGet]
        [AllowAnonymous]
        public ActionResult Index([FromServices] IStudentService _studentService) =>
            View(_studentService.GetAll());


        [Route("detalhes/{id}"), HttpGet]
        [Authorize(Policy = "RequireUserManagerAdminRole")]
        public async Task<ActionResult> Details(Guid id, [FromServices] IStudentService studentService) =>
            View(await studentService.GetDetailsByIdAsync(id));


        [Route("cadastrar"), HttpGet]
        [Authorize(Policy = "RequireUserManagerAdminRole")]
        public ActionResult Create() => View();

        [Route("cadastrar"), HttpPost, ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireUserManagerAdminRole")]
        public async Task<ActionResult> Create(CreateStudentDto studentDto, [FromServices] IStudentService studentService)
        {
            if (!ModelState.IsValid)
                return View(studentDto);

            await studentService.CreateAsync(studentDto);
            return RedirectToAction(nameof(Index));
        }


        [Route("editar/{id}"), HttpGet]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult> Edit(Guid id, [FromServices] IStudentService studentService)
        {
            var student = await studentService.GetEditByIdAsync(id);
            if (student is null)
                return NotFound();

            return View(student);
        }

        [Route("editar/{id}"), HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult> Edit(EditStudentDto studentDto, [FromServices] IStudentService studentService)
        {
            if (!ModelState.IsValid)
                return View(studentDto);

            await studentService.UpdateAsync(studentDto);
            return RedirectToAction(nameof(Index));
        }


        [Route("apagar/{id}"), HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id, [FromServices] IStudentService studentService)
        {
            var student = await studentService.GetDeleteByIdAsync(id);        
            if (student is null)
                return NotFound();

            return View(student);
        }

        [Route("apagar/{id}"), HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(Guid id, [FromServices] IStudentService studentService)
        {
            await studentService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}