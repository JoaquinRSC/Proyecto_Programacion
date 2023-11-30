using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Auth.Models;

namespace API_Auth.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/user")]
        public IHttpActionResult Get()
        {
            return Ok(CapaLogica.UserController.ObtenerTodos());
        }

        [Route("api/user")]
        public void Post([FromBody]string value)
        {
            CapaLogica.UserController.Crear("joaquin_r", "12345");
        }

        [Route("api/login")]
        public IHttpActionResult Auth([FromBody]UserModel user)
        {
            CapaLogica.UserController.Crear("joaquin_r", "12345");
            Dictionary<string,string> autenticacion = CapaLogica.UserController.Login(user.Username,user.Password);
            if (autenticacion["resultado"] == "OK")
                return Ok(autenticacion);
            return Unauthorized();
        }




    }
}
