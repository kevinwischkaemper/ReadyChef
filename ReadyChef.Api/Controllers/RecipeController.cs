using ReadyChef.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadyChef.Api.Controllers
{
    public class RecipeController : ApiController
    {   
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            //asdf
            return Ok(true);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id == 1)
                return Ok("returned 1");
            else
                return Ok("not 1");
        }

        [HttpPost]
        public void Add([FromBody] Recipe recipe)
        {

        }
    }
}
