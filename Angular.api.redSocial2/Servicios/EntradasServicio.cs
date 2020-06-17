using Angular.api.redSocial2.Configuracion;
using Angular.api.redSocial2.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.api.redSocial2.Servicios
{
    public class EntradasServicio
    {
        private MySqlConnection con;


        public EntradasServicio() { }

        private void conectarBd()
        {
            ConfigString conf = new ConfigString();
            con = new MySqlConnection(conf.obtConex());
            
        }

        //buscar todos
        public List<Entradas> RecuperarTodos()
        {
            conectarBd();

            List<Entradas> listaEntr = new List<Entradas>();

            MySqlCommand com = new MySqlCommand("select id_entrada, id_usu, titulo_entrada, texto_entrada, fecha_creacion_ent from entradas", con);
            con.Open();

            MySqlDataReader registros = com.ExecuteReader();

            while (registros.Read())
            {
                Entradas entr = new Entradas();

                entr.idEntrada = int.Parse(registros["id_entrada"].ToString());
                entr.idUsu = int.Parse(registros["id_usu"].ToString());
                entr.tituloEntrada = registros["titulo_entrada"].ToString();
                entr.textoEntrada = registros["texto_entrada"].ToString();
                entr.fechaCreacionEntrada = DateTime.Parse(registros["fecha_creacion_ent"].ToString());

                listaEntr.Add(entr);
            }

            con.Close();

            return listaEntr;
        }

        //Buscar por id
        public Entradas Recuperar(int id_entr)
        {
            conectarBd();

            Entradas entr = new Entradas();

            MySqlCommand com = new MySqlCommand("select id_entrada, id_usu, titulo_entrada, texto_entrada, fecha_creacion_ent from entradas where id_entrada = @id_entr", con);

            com.Parameters.Add("@id_entr", MySqlDbType.Int32);
            com.Parameters["@id_entr"].Value = id_entr;

            con.Open();
            MySqlDataReader registros = com.ExecuteReader();

            if (registros.Read())
            {
                entr.idEntrada = int.Parse(registros["id_entrada"].ToString());
                entr.idUsu = int.Parse(registros["id_usu"].ToString());
                entr.tituloEntrada = registros["titulo_entrada"].ToString();
                entr.textoEntrada = registros["texto_entrada"].ToString();
                entr.fechaCreacionEntrada = DateTime.Parse(registros["fecha_creacion_ent"].ToString());
            }

            con.Close();

            return entr;
        }

        //Insertar
        public string insertar(Entradas entr)
        {
            
            conectarBd();

            //Otra forma:
            //MySqlCommand com = new MySqlCommand("insert into entradas (id_entrada, id_usu, titulo_entrada, texto_entrada, fecha_creacion_ent) values (" + entr.idEntrada + ", " + entr.idUsu + ", '" + entr.tituloEntrada + "', '" + entr.textoEntrada +"', " + entr.fechaCreacionEntrada + ")", con);
            
            MySqlCommand com = new MySqlCommand("insert into entradas (id_entrada, id_usu, titulo_entrada, texto_entrada, fecha_creacion_ent) values (@id_entr, @id_usu, @titulo_entrada, @texto_entrada, @fecha_creacion_ent)", con);
            
            com.Parameters.Add("@id_entr", MySqlDbType.Int32);
            com.Parameters.Add("@id_usu", MySqlDbType.Int32);
            com.Parameters.Add("@titulo_entrada", MySqlDbType.VarChar);
            com.Parameters.Add("@texto_entrada", MySqlDbType.VarChar);
            com.Parameters.Add("@fecha_creacion_ent", MySqlDbType.DateTime);

            com.Parameters["@id_entr"].Value = entr.idEntrada;
            com.Parameters["@id_usu"].Value = entr.idUsu;
            com.Parameters["@titulo_entrada"].Value = entr.tituloEntrada;
            com.Parameters["@texto_entrada"].Value = entr.textoEntrada;
            com.Parameters["@fecha_creacion_ent"].Value = entr.fechaCreacionEntrada;

            string resultado = "";

            try {

                con.Open();

                try {

                    //Reviar if
                    int rows = com.ExecuteNonQuery();

                    if (rows >= 1)
                    {
                        resultado = "Inserccion realizada correctamente";
                    }
                    else
                    {
                        resultado = "Problemas en la inserccion: ";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("PROBLEMA: " + ex.ToString());
                }
                
                con.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("PROBLEMA: " + ex.ToString());
            }

            return resultado;

        }

        //Modificar
        public void modificar(Entradas entr)
        {
            conectarBd();

        }

        //Borrar
        public void borrar(Entradas entr)
        {
            conectarBd();

        }

        //Borrar por id
        public void borrarId(int id_entr)
        {
            conectarBd();

        }

    }
}
