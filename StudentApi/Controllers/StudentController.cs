using Microsoft.AspNetCore.Mvc;
using StudentApi.Entities;
using StudentApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            var result = await _studentRepository.GetStudents();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Student>> GetById(int Id)
        {
            var result = await _studentRepository.GetStudentByIdAsync(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task Post([FromBody] Student student)
        {
            if (ModelState.IsValid)
                await _studentRepository.CreateStudent(student);
        }

        [HttpPut("{Id:int}")]
        public async Task Put(int Id, [FromBody] Student student)
        {
            student.Id = Id;
            if (ModelState.IsValid)
                await _studentRepository.UpdateStudent(student);

        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int Id)
        {
            var result = await _studentRepository.DeleteStudent(Id);
        }

    }
}
