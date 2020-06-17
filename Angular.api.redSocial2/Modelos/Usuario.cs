using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.api.redSocial2.Modelos
{
    public class Usuario
    {
        public Usuario() { }

        public int idUsu { get; set; }
        public string nombreUsu { get; set; }
        public DateTime fechaAlta { get; set; } //fecha automatica
        public string pais { get; set; }
        public string ciudad { get; set; }
        public string region { get; set; }
        public string email { get; set; }   //Por defecto se usa el de entrada
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public DateTime fechaNacimiento { get; set; } // ver si datetime es compatible con date de Mysql
        public string telefono { get; set; }
        public string emailEntrada { get; set; } //email para entrar en la plataforma
        public string contrasenya { get; set; }  //password
        public bool perfilPrivado { get; set; }   //Opciones boolean (se usa 0 o 1)
        public bool aceptadaPolPriva { get; set; } //usuario acepta la politica de privacidad
        public bool esAdministrador { get; set; } //usuario admin (permite administrar otras cuentas)
        public bool usuarioActivo { get; set; }
        public bool usuarioBaneado { get; set; }      //castigo a usuario
        public DateTime fechaFinBaneo { get; set; }  //ver si se puede seleccionar horas en angular
        public string estado { get; set; } //variables referentes a la interfaz de usuario
        public int numVisitas { get; set; }
        public DateTime fechaUltimoLogin { get; set; }
        public string fotoPerfil { get; set; }  //foto de perfil (ver como transmitir imagenes o subir en una carpeta en angular)
        public string fotoPortada { get; set; } //foto de portada


        //convierte de bool a int (para introducir en las query)
        public int ConvertirBoolInt(bool valorBool)
        {
            int numeroBool = 0;

            if (valorBool.Equals(true))
            {
                numeroBool = 1;
            }

            return numeroBool;
        }

    }

    //Nota: las fechas iran en la manera que se pueda de manera automatica
    // en el lado del servidor

    //Los tiny int de MySQL se identifican como bool

    //si se introduce un dato en una consulta (select, insert ...), no se puede usar bool, 
    //se debe de convertir
    

}
