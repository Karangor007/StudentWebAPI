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
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {
            var obj = StudentsData.getStudents();


            return Ok(obj);
        }

        [HttpGet]
        public IHttpActionResult GetAllStudents(int id)
        {
            var obj = StudentsData.getStudentById(id);


            return Ok(obj);
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
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();
            var obj = StudentsData.deleteStudent(id);
            return Ok(obj);
        }
        #endregion




    }
}
