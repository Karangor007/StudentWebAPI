using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentWebAPI.Models;

namespace StudentWebAPI.Controllers
{
    public class StudentController : ApiController
    {
        public StudentController()
        {

        }

        #region GET Methods
        [Route("api/student")]
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {
            var obj = StudentsData.getStudents();


            return Ok(obj);
        }

        [Route("api/student/GetStudentsById")]
        [HttpGet]
        public IHttpActionResult GetStudentsById(Student param)
        {
            Student obj = new Student();
            obj.id = param.id;
            if (obj.id <= 0)
                return BadRequest();
            var temp = StudentsData.getStudentById(obj);
            
            return Ok(temp);
        }
        #endregion

        #region POST Methods
        [HttpPost]
        public IHttpActionResult PostStudents(Student param)
        {
            var obj = StudentsData.postStudent(param);
            return Ok(obj);
        }
        #endregion

        #region PUT Methods
        [HttpPut]
        public IHttpActionResult PutStudents(Student param)
        {
            if (param.id <= 0)
                return BadRequest();
            var obj = StudentsData.PutStudent(param);
            return Ok(obj);
        }
        #endregion

        #region DELETE Methods
        [HttpDelete]
        public IHttpActionResult Delete(Student param)
        {
            Student obj = new Student();
            obj.id = param.id;
            if (obj.id <= 0)
                return BadRequest();
            var temp = StudentsData.deleteStudent(obj);
            return Ok(temp);
        }
        #endregion




    }
}
