using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentWebAPI.Models;

namespace StudentWebAPI.Controllers
{
    public class MarksController : ApiController
    {
        public MarksController()
        {

        }

        #region GET Methods
        [Route("api/marks")]
        [HttpGet]
        public IHttpActionResult GetAllMarks()
        {
            var obj = StudentMarks.getMarks();


            return Ok(obj);
        }

        [Route("api/marks/GetMarksByStudentId")]
        [HttpGet]
        public IHttpActionResult GetMarksByStudentId(Marks param)
        {
            Marks obj = new Marks();
            obj.studentId = param.studentId;
            if (obj.studentId <= 0)
                return BadRequest();
            var temp = StudentMarks.getMarksByStudentId(obj);


            return Ok(temp);
        }
        #endregion

        #region POST Methods
        [Route("api/marks/PostMarksByStudentId")]
        [HttpPost]
        public IHttpActionResult PostMarks(Marks param)
        {
            Marks obj = new Marks();
            obj.studentId = param.studentId;
            if (obj.studentId <= 0)
                return BadRequest();
            var temp = StudentMarks.postMarks(param);
            return Ok(temp);
        }
        #endregion

        #region PUT Methods
        [Route("api/marks/UpdateMarksByStudentId")]
        [HttpPut]
        public IHttpActionResult PutMarks(Marks param)
        {
            Marks obj = new Marks();
            obj = param;
            if (obj.studentId <= 0)
                return BadRequest();
            var temp = StudentMarks.PutMarks(obj);
            return Ok(temp);
        }
        #endregion

        #region DELETE Methods
        [Route("api/marks/DeleteMarksById")]
        [HttpDelete]
        public IHttpActionResult DeleteMarks(Marks param)
        {
            Marks obj = new Marks();
            obj = param;
            if (obj.id <= 0)
                return BadRequest();
            var temp = StudentMarks.deleteMarks(obj);
            return Ok(temp);
        }
        #endregion
    }



}
