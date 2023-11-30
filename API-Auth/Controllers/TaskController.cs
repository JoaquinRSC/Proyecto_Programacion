using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Auth.Models;

namespace API_Auth.Controllers
{
    public class TaskController : ApiController
    {
        [Route("api/task")]
        public void Post([FromBody] string title, string authorName, string body, string creationDate, string expirationDate)
        {
           CapaLogica.TaskController.Crear(title, authorName, body, creationDate, expirationDate);
        }

        [Route("api/task")]
        public void Patch([FromBody] int id, string title, string authorName, string body, string creationDate, string expirationDate)
        {
            CapaLogica.TaskController.Editar(id, title, authorName, body, creationDate, expirationDate);
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
