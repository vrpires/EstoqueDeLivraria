using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EstoqueDeLivros.Web.Models
{
    public class UsuarioViewModel
    {
        public static bool ValidaUsuario(string login, string senha)
        {
            var retorno = false;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("SELECT count(*) FROM Usuario WHERE login='{0}' AND senha='{1}'", login, senha);
                    retorno = ((int)comando.ExecuteScalar() > 0);
                }
            }

            return retorno;
        }
    }
}