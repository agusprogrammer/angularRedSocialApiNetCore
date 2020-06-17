using Angular.api.redSocial2.Configuracion;
using Angular.api.redSocial2.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.api.redSocial2.Servicios
{
    public class UsuarioServicio
    {
        private MySqlConnection con;

        public UsuarioServicio() { }

        private void conectarBd()
        {
            ConfigString conf = new ConfigString();
            con = new MySqlConnection(conf.obtConex());

        }

        //Obtener usuario por login
        public Usuario usuarioLogin(string emailEntrada, string contrasenya)
        {
            conectarBd();

            Usuario usu = new Usuario();
            
            MySqlCommand com = new MySqlCommand("select * from usuario where contrasenya = @contrasenya and email_entrada = @email_ent", con);

            com.Parameters.Add("@email_ent", MySqlDbType.VarChar);
            com.Parameters["@email_ent"].Value = emailEntrada;

            com.Parameters.Add("@contrasenya", MySqlDbType.VarChar);
            com.Parameters["@contrasenya"].Value = contrasenya;

            con.Open();

            MySqlDataReader registros = com.ExecuteReader();

            if (registros.Read())
            {
                usu.idUsu = int.Parse(registros["id_usu"].ToString());
                usu.nombreUsu = registros["nombre_usu"].ToString();
                usu.fechaAlta = DateTime.Parse(registros["fecha_alta"].ToString());
                usu.pais = registros["pais"].ToString();
                usu.ciudad = registros["ciudad"].ToString();
                usu.region = registros["region"].ToString();
                usu.email = registros["email"].ToString();      //puede ser o no de entrada
                usu.nombre = registros["nombre"].ToString();
                usu.apellidos = registros["apellidos"].ToString();
                usu.fechaNacimiento = DateTime.Parse(registros["fecha_nacimiento"].ToString());
                usu.telefono = registros["telefono"].ToString();
                usu.emailEntrada = registros["email_entrada"].ToString();
                usu.contrasenya = registros["contrasenya"].ToString(); usu.perfilPrivado = bool.Parse(registros["perfil_privado"].ToString());
                usu.aceptadaPolPriva = bool.Parse(registros["aceptada_pol_priva"].ToString());
                usu.esAdministrador = bool.Parse(registros["es_administrador"].ToString());
                usu.usuarioActivo = bool.Parse(registros["usuario_activo"].ToString());
                usu.usuarioBaneado = bool.Parse(registros["usuario_baneado"].ToString());

                string regBanStr = registros["fecha_fin_baneo"].ToString();

                if (regBanStr != "")
                {
                    usu.fechaFinBaneo = DateTime.Parse(registros["fecha_fin_baneo"].ToString());
                }


                usu.estado = registros["estado"].ToString();
                usu.numVisitas = int.Parse(registros["num_visitas"].ToString());
                usu.fechaUltimoLogin = DateTime.Parse(registros["fecha_ultimo_login"].ToString());
                usu.fotoPerfil = registros["foto_perfil"].ToString();
                usu.fotoPortada = registros["foto_portada"].ToString();

            }

            con.Close();

            return usu;
        }
        

        //Obtener usuario por id
        public Usuario obtenerUsuPorId(int idUsu)
        {
            conectarBd();

            Usuario usu = new Usuario();

            MySqlCommand com = new MySqlCommand("select * from usuario where id_usu = @id_usu", con);

            com.Parameters.Add("@id_usu", MySqlDbType.Int32);
            com.Parameters["@id_usu"].Value = idUsu;

            con.Open();

            MySqlDataReader registros = com.ExecuteReader();

            if (registros.Read())
            {
                usu.idUsu = int.Parse(registros["id_usu"].ToString());
                usu.nombreUsu = registros["nombre_usu"].ToString();
                usu.fechaAlta = DateTime.Parse(registros["fecha_alta"].ToString());
                usu.pais = registros["pais"].ToString();
                usu.ciudad = registros["ciudad"].ToString();
                usu.region = registros["region"].ToString();
                usu.email = registros["email"].ToString();      //puede ser o no de entrada
                usu.nombre = registros["nombre"].ToString();
                usu.apellidos = registros["apellidos"].ToString();
                usu.fechaNacimiento = DateTime.Parse(registros["fecha_nacimiento"].ToString());
                usu.telefono = registros["telefono"].ToString();
                usu.emailEntrada = registros["email_entrada"].ToString();
                usu.contrasenya = registros["contrasenya"].ToString();                usu.perfilPrivado = bool.Parse(registros["perfil_privado"].ToString());
                usu.aceptadaPolPriva = bool.Parse(registros["aceptada_pol_priva"].ToString());
                usu.esAdministrador = bool.Parse(registros["es_administrador"].ToString());
                usu.usuarioActivo = bool.Parse(registros["usuario_activo"].ToString());
                usu.usuarioBaneado = bool.Parse(registros["usuario_baneado"].ToString());

                string regBanStr = registros["fecha_fin_baneo"].ToString();

                //If para evitar error del datetime
                if (regBanStr != "")
                {
                    usu.fechaFinBaneo = DateTime.Parse(registros["fecha_fin_baneo"].ToString());
                }
                
                usu.estado = registros["estado"].ToString();
                usu.numVisitas = int.Parse(registros["num_visitas"].ToString());
                usu.fechaUltimoLogin = DateTime.Parse(registros["fecha_ultimo_login"].ToString());
                usu.fotoPerfil = registros["foto_perfil"].ToString();
                usu.fotoPortada = registros["foto_portada"].ToString();

            }

            con.Close();

            return usu;
        }
        
        //Registrar usuario
        public string insertarUsuario(Usuario usu)
        {
            conectarBd();

            string resultado = "";
            

            string query = "";
            if (usu.idUsu <= 0 | usu.idUsu == null)
            {
                //Quitar id de usuario
                //MySqlCommand com = new MySqlCommand("insert into usuario() values()", con);
                query = "insert into usuario(nombre_usu,fecha_alta,pais,ciudad,region,email,nombre,apellidos,fecha_nacimiento,telefono,email_entrada,contrasenya,perfil_privado,aceptada_pol_priva,es_administrador,usuario_activo,usuario_baneado,fecha_fin_baneo,estado,num_visitas,fecha_ultimo_login,foto_perfil,foto_portada)" +
                    " values('" + usu.nombreUsu + "', @fecha_alta ,'" + usu.pais + "','" + usu.ciudad + "','" + usu.region + "','" + usu.email + "','" + usu.nombre + "','" + usu.apellidos + "', @fecha_nacimiento ,'" + usu.telefono + "','" + usu.emailEntrada + "','" + usu.contrasenya + "'," + usu.ConvertirBoolInt(usu.perfilPrivado) + "," + usu.ConvertirBoolInt(usu.aceptadaPolPriva) + "," + usu.ConvertirBoolInt(usu.esAdministrador) + "," + usu.ConvertirBoolInt(usu.usuarioActivo) + "," + usu.ConvertirBoolInt(usu.usuarioBaneado) + ", @fecha_fin_baneo ,'" + usu.estado + "'," + usu.numVisitas + ", @fecha_ultimo_login ,'" + usu.fotoPerfil + "','" + usu.fotoPortada + "')";
            }
            else
            {
                //MySqlCommand com = new MySqlCommand("insert into usuario(id_usu) values(" + usu.idUsu + ")", con);
                query = "insert into usuario(id_usu,nombre_usu,fecha_alta,pais,ciudad,region,email,nombre,apellidos,fecha_nacimiento,telefono,email_entrada,contrasenya,perfil_privado,aceptada_pol_priva,es_administrador,usuario_activo,usuario_baneado,fecha_fin_baneo,estado,num_visitas,fecha_ultimo_login,foto_perfil,foto_portada)" +
                    " values(" + usu.idUsu + ",'" + usu.nombreUsu + "', @fecha_alta ,'" + usu.pais + "','" + usu.ciudad + "','" + usu.region + "','" + usu.email + "','" + usu.nombre + "','" + usu.apellidos + "', @fecha_nacimiento ,'" + usu.telefono + "','" + usu.emailEntrada + "','" + usu.contrasenya + "'," + usu.ConvertirBoolInt(usu.perfilPrivado) + "," + usu.ConvertirBoolInt(usu.aceptadaPolPriva) + "," + usu.ConvertirBoolInt(usu.esAdministrador) + "," + usu.ConvertirBoolInt(usu.usuarioActivo) + "," + usu.ConvertirBoolInt(usu.usuarioBaneado) + ", @fecha_fin_baneo ,'" + usu.estado + "'," + usu.numVisitas + ", @fecha_ultimo_login ,'" + usu.fotoPerfil + "','" + usu.fotoPortada + "')";
                //Nota: revisar valores booleanos y fechas si la sintax es correcta
            }

            //Nota: convertir valores bool a numero, poner un metodo dentro del objeto usuario

            MySqlCommand com = new MySqlCommand(query, con);

            //Introducir fechas
            com.Parameters.Add("@fecha_alta", MySqlDbType.DateTime);
            com.Parameters["@fecha_alta"].Value = usu.fechaAlta;

            com.Parameters.Add("@fecha_nacimiento", MySqlDbType.Date);
            com.Parameters["@fecha_nacimiento"].Value = usu.fechaNacimiento;

            com.Parameters.Add("@fecha_fin_baneo", MySqlDbType.DateTime);
            com.Parameters["@fecha_fin_baneo"].Value = usu.fechaFinBaneo;

            com.Parameters.Add("@fecha_ultimo_login", MySqlDbType.DateTime);
            com.Parameters["@fecha_ultimo_login"].Value = usu.fechaUltimoLogin;
            
            //Ejecutar comando sql
            con.Open();

            int rows = 0;

            try {
                rows = com.ExecuteNonQuery();

            } catch(Exception ex) {
                Console.Write(ex.ToString());
                rows = 0;
            }
            
            if (rows >= 1)
            {
                resultado = "Usuario insertado";
            }
            else
            {
                resultado = "Problemas, usuario no insertado";
            }

            con.Close();

            return resultado;
        }

        //Modificar
        public string modificarUsuario(Usuario usu)
        {
            conectarBd();

            string resultado = "";

            //Obtener usuario para comprobar que los campos unicos son iguales
            //Es para evitar errorres con update
            Usuario usuComp = new Usuario();
            usuComp = obtenerUsuPorId(usu.idUsu);

            string query = "";

            if (usu.nombreUsu.Equals(usuComp.nombreUsu) & usu.emailEntrada.Equals(usuComp.emailEntrada))
            {
                //No introducir los 2 campos unique
                query = "update usuario set fecha_alta = @fecha_alta , pais = '" + usu.pais + "', ciudad = '" + usu.ciudad + "', region = '" + usu.region + "', " +
                " email = '" + usu.email + "', nombre = '" + usu.nombre + "', apellidos = '" + usu.apellidos + "', fecha_nacimiento = @fecha_nacimiento, telefono = '" + usu.telefono + "', " +
                " contrasenya = '" + usu.contrasenya + "', perfil_privado = " + usu.ConvertirBoolInt(usu.perfilPrivado) + ", aceptada_pol_priva = " + usu.ConvertirBoolInt(usu.aceptadaPolPriva) + ", " +
                " es_administrador = " + usu.ConvertirBoolInt(usu.esAdministrador) + ", usuario_activo = " + usu.ConvertirBoolInt(usu.usuarioActivo) + ", " +
                " usuario_baneado = " + usu.ConvertirBoolInt(usu.usuarioBaneado) + ", fecha_fin_baneo = @fecha_fin_baneo, estado = '" + usu.estado + "', num_visitas = " + usu.numVisitas + ", " +
                " fecha_ultimo_login = @fecha_ultimo_login, foto_perfil = '" + usu.fotoPerfil + "', foto_portada = '" + usu.fotoPortada + "' " +
                " where id_usu = " + usu.idUsu + " ";
            }
            else if (usu.nombreUsu.Equals(usuComp.nombreUsu))
            {
                //Introducir email
                query = "update usuario set fecha_alta = @fecha_alta , pais = '" + usu.pais + "', ciudad = '" + usu.ciudad + "', region = '" + usu.region + "', " +
                " email = '" + usu.email + "', nombre = '" + usu.nombre + "', apellidos = '" + usu.apellidos + "', fecha_nacimiento = @fecha_nacimiento, telefono = '" + usu.telefono + "', " +
                " email_entrada = '" + usu.emailEntrada + "', contrasenya = '" + usu.contrasenya + "', perfil_privado = " + usu.ConvertirBoolInt(usu.perfilPrivado) + ", aceptada_pol_priva = " + usu.ConvertirBoolInt(usu.aceptadaPolPriva) + ", " +
                " es_administrador = " + usu.ConvertirBoolInt(usu.esAdministrador) + ", usuario_activo = " + usu.ConvertirBoolInt(usu.usuarioActivo) + ", " +
                " usuario_baneado = " + usu.ConvertirBoolInt(usu.usuarioBaneado) + ", fecha_fin_baneo = @fecha_fin_baneo, estado = '" + usu.estado + "', num_visitas = " + usu.numVisitas + ", " +
                " fecha_ultimo_login = @fecha_ultimo_login, foto_perfil = '" + usu.fotoPerfil + "', foto_portada = '" + usu.fotoPortada + "' " +
                " where id_usu = " + usu.idUsu + " ";
            }
            else if (usu.emailEntrada.Equals(usuComp.emailEntrada))
            {
                //Introducir nombre usu
                query = "update usuario set nombre_usu = '" + usu.nombreUsu + "', fecha_alta = @fecha_alta , pais = '" + usu.pais + "', ciudad = '" + usu.ciudad + "', region = '" + usu.region + "', " +
                " email = '" + usu.email + "', nombre = '" + usu.nombre + "', apellidos = '" + usu.apellidos + "', fecha_nacimiento = @fecha_nacimiento, telefono = '" + usu.telefono + "', " +
                " contrasenya = '" + usu.contrasenya + "', perfil_privado = " + usu.ConvertirBoolInt(usu.perfilPrivado) + ", aceptada_pol_priva = " + usu.ConvertirBoolInt(usu.aceptadaPolPriva) + ", " +
                " es_administrador = " + usu.ConvertirBoolInt(usu.esAdministrador) + ", usuario_activo = " + usu.ConvertirBoolInt(usu.usuarioActivo) + ", " +
                " usuario_baneado = " + usu.ConvertirBoolInt(usu.usuarioBaneado) + ", fecha_fin_baneo = @fecha_fin_baneo, estado = '" + usu.estado + "', num_visitas = " + usu.numVisitas + ", " +
                " fecha_ultimo_login = @fecha_ultimo_login, foto_perfil = '" + usu.fotoPerfil + "', foto_portada = '" + usu.fotoPortada + "' " +
                " where id_usu = " + usu.idUsu + " ";
            }
            else
            {
                //Introducir los 2 campos
                query = "update usuario set nombre_usu = '" + usu.nombreUsu + "', fecha_alta = @fecha_alta , pais = '" + usu.pais + "', ciudad = '" + usu.ciudad + "', region = '" + usu.region + "', " +
                " email = '" + usu.email + "', nombre = '" + usu.nombre + "', apellidos = '" + usu.apellidos + "', fecha_nacimiento = @fecha_nacimiento, telefono = '" + usu.telefono + "', " +
                " email_entrada = '" + usu.emailEntrada + "', contrasenya = '" + usu.contrasenya + "', perfil_privado = " + usu.ConvertirBoolInt(usu.perfilPrivado) + ", aceptada_pol_priva = " + usu.ConvertirBoolInt(usu.aceptadaPolPriva) + ", " +
                " es_administrador = " + usu.ConvertirBoolInt(usu.esAdministrador) + ", usuario_activo = " + usu.ConvertirBoolInt(usu.usuarioActivo) + ", " +
                " usuario_baneado = " + usu.ConvertirBoolInt(usu.usuarioBaneado) + ", fecha_fin_baneo = @fecha_fin_baneo, estado = '" + usu.estado + "', num_visitas = " + usu.numVisitas + ", " +
                " fecha_ultimo_login = @fecha_ultimo_login, foto_perfil = '" + usu.fotoPerfil + "', foto_portada = '" + usu.fotoPortada + "' " +
                " where id_usu = " + usu.idUsu + " ";
            }

            MySqlCommand com = new MySqlCommand(query, con);

            //Introducir fechas
            com.Parameters.Add("@fecha_alta", MySqlDbType.DateTime);
            com.Parameters["@fecha_alta"].Value = usu.fechaAlta;

            com.Parameters.Add("@fecha_nacimiento", MySqlDbType.Date);
            com.Parameters["@fecha_nacimiento"].Value = usu.fechaNacimiento;

            com.Parameters.Add("@fecha_fin_baneo", MySqlDbType.DateTime);
            com.Parameters["@fecha_fin_baneo"].Value = usu.fechaFinBaneo;

            com.Parameters.Add("@fecha_ultimo_login", MySqlDbType.DateTime);
            com.Parameters["@fecha_ultimo_login"].Value = usu.fechaUltimoLogin;

            //Ejecutar comando sql
            con.Open();

            
            int rows = com.ExecuteNonQuery();
            

            if (rows >= 1)
            {
                resultado = "Usuario actualizado";
            }
            else
            {
                resultado = "Problemas, usuario no actualizado";
            }

            con.Close();

            return resultado;
        }

        //Si se borra un usuario se puede implementar 
        //el borrado en cascada (por codigo o bd) para eliminar el usuario y su contenido
        //Borrar usuario
        public string borrarUsuario(Usuario usu)
        {
            conectarBd();

            string resultado = "";

            MySqlCommand com = new MySqlCommand("delete from usuario where usuario.id_usu = " + usu.idUsu, con);

            //Ejecutar comando sql
            con.Open();

            int rows = com.ExecuteNonQuery();

            if (rows >= 1)
            {
                resultado = "Usuario borrado";
            }
            else
            {
                resultado = "Problemas, usuario no borrado";
            }

            con.Close();
            
            return resultado;
        }


    }
}
