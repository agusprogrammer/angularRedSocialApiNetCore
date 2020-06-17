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
    [Route("api/UsuarioApi")]
    public class UsuarioApiController : Controller
    {

        // http://localhost:50139/swagger/index.html
        // http://localhost:50139/api/UsuarioApi

        // GET: api/UsuarioApi
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        

        // GET: api/UsuarioApi/5
        [HttpGet("{id}", Name = "GetUsu")] //Nota: en los Get de las api, cambiar el name, sino da problemas
        public ActionResult Get(int id)
        {
            UsuarioServicio usuServ = new UsuarioServicio();
            Usuario usu = new Usuario();

            usu = usuServ.obtenerUsuPorId(id);

            return Ok(usu);
        }

        //Nota: en los Get de las api, cambiar el name, sino da problemas
        // error:  Attribute routes with the same name 'Get' must have the same template

        //mirar el tema de rutas, vienen mas datos
        // GET: api/UsuarioApi/email/contrasenya
        [HttpGet("{emailent}/{contrasenya}", Name = "GetUsuLogin")] //Nota: en los Get de las api, cambiar el name, sino da problemas
        public ActionResult Get(string emailent, string contrasenya)
        {
            UsuarioServicio usuServ = new UsuarioServicio();
            Usuario usu = new Usuario();

            usu = usuServ.usuarioLogin(emailent, contrasenya);

            return Ok(usu);
        }

        // POST: api/UsuarioApi
        // public ActionResult Post([FromBody]string value)
        [HttpPost]
        public ActionResult Post([FromBody]Usuario usu)
        {
            UsuarioServicio usuServ = new UsuarioServicio();

            string resultado = "";

            if(usu != null)
            {
                resultado = usuServ.insertarUsuario(usu);
            }
            else
            {
                resultado = "Usuario vacio";
            }
            
            return Ok(resultado);

        }

        
        // PUT: api/UsuarioApi/5
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //public ActionResult Put(int id, [FromBody]Usuario usu)
        public ActionResult Put(int id, [FromBody]Usuario usu)
        {
            UsuarioServicio usuServ = new UsuarioServicio();
            
            string resultado = usuServ.modificarUsuario(usu);

            return Ok(resultado);

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            UsuarioServicio usuServ = new UsuarioServicio();

            Usuario usu = new Usuario();

            usu = usuServ.obtenerUsuPorId(id);

            string resultado = "";

            if(usu != null)
            {
                resultado = usuServ.borrarUsuario(usu);
            }
            else
            {
                resultado = "Usuario vacio";
            }

            return Ok(resultado);
        }

        /*
            //usuario de prueba
            Usuario usu = new Usuario();
            //usu.idUsu = 2;
            usu.nombreUsu = "usuPrueba2";
            usu.fechaAlta = DateTime.Now;
            usu.pais = "España";
            usu.ciudad = "Madrid";
            usu.region = "CValenciana";
            usu.email = "usuPrueba2@gmail.com";
            usu.nombre = "Andres";
            usu.apellidos = "Clemente";
            usu.fechaNacimiento = DateTime.Now;
            usu.telefono = "+79 (503) 687 328";
            usu.emailEntrada = "usuPrueba2@gmail.com";
            usu.contrasenya = "123456789";
            usu.perfilPrivado = false;
            usu.aceptadaPolPriva = true;
            usu.esAdministrador = false;
            usu.usuarioActivo = true;
            usu.usuarioBaneado = false;
            // usu.fechaFinBaneo = DateTime.Now; //se pone la fecha inicial por defecto
            usu.estado = "(frase usuario)";
            usu.numVisitas = 0;
            usu.fechaUltimoLogin = DateTime.Now;
            usu.fotoPerfil = "";
            usu.fotoPortada = "";
            */

    }
}
