using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular.api.redSocial2.Modelos;
using Angular.api.redSocial2.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Angular.api.redSocial2.Controllers
{
    [Produces("application/json")]
    [Route("api/EntradasApi")]
    public class EntradasApiController : Controller
    {

        /*
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        // http://localhost:50139/swagger/index.html
        // http://localhost:50139/api/EntradasApi

        // GET: api/EntradasApi
        [HttpGet]
        public ActionResult Get()
        {
            EntradasServicio entrServ = new EntradasServicio();
            
            return Ok(entrServ.RecuperarTodos());
        }


        // GET: api/EntradasApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/EntradasApi
        // public void Post([FromBody]string value)

        // http://localhost:50139/api/EntradasApi/PostAddEntrada

        [HttpPost("PostAddEntrada")] //se ha puesto el nombre del metodo
        public ActionResult PostAddEntrada([FromBody]Entradas entr)
        {
            EntradasServicio entrServ = new EntradasServicio();

            entr.fechaCreacionEntrada = DateTime.Now;
            //Nota: para recoger datos de angular 
            //tienen que coincidir el nombre las 
            //variables en C# con las de angular
            
            // Prueba para insertar fecha
            // DateTime fechEntr = new DateTime(2020, 05, 10, 17, 26, 00);
            // entr.fechaCreacionEntrada = fechEntr;

            string resultado = entrServ.insertar(entr);

            return Ok(resultado);

        }
        
        // PUT: api/EntradasApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
