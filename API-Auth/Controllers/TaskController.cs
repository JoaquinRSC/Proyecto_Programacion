using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Auth.Models;
using Entities;

namespace API_Auth.Controllers
{
    public class TaskController : ApiController
    {
        [Route("api/task")]
        public void Post([FromBody] TaskEntity task)
        {
           CapaLogica.TaskController.Crear(task);
        }

        [Route("api/task")]
        public void Put([FromBody] TaskEntity task)
        {
            CapaLogica.TaskController.Editar(task);
        }

        [Route("api/task")]
        public IHttpActionResult Get()
        {
            return Ok(CapaLogica.TaskController.ObtenerTodos());
        }

        [Route("api/task/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(CapaLogica.TaskController.ObtenerUno(id));

        }

        [Route("api/task/{id:int}")]
        public void Delete(int id)
        {
            CapaLogica.TaskController.Borrar(id);
        }
    }
}
