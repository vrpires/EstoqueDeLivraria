using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EstoqueDeLivros.Web.Models
{
    public class LivroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o autor(a).")]
        public string Autor { get; set; }

        public int Quantidade { get; set; }

        public static List<LivroViewModel> ListaLivrosBanco()
        {
            var retorno = new List<LivroViewModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "SELECT * FROM Livros ORDER BY nome ASC";

                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        retorno.Add(new LivroViewModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Autor = (string)reader["autor"],
                            Quantidade = (int)reader["quantidade"]
                        });
                    }
                }
            }

            return retorno;
        }

        public static LivroViewModel Editar(int id)
        {
            LivroViewModel retorno = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("SELECT * FROM Livros WHERE (id = {0})", id);

                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        retorno = new LivroViewModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Autor = (string)reader["autor"],
                            Quantidade = (int)reader["quantidade"]
                        };
                    }
                }
            }

            return retorno;
        }

        public static bool Excluir(int id)
        {
            var retorno = false;

            if (Editar(id) != null)
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format("DELETE FROM Livros WHERE (id = {0})", id);

                        retorno = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return retorno;
        }

        public int Salvar()
        {
            var retorno = 0;

            var verificaId = Editar(this.Id);


            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    if (verificaId == null)
                    {
                        comando.CommandText = string.Format("INSERT INTO Livros(nome, autor, quantidade) VALUES ('{0}','{1}',{2}); SELECT convert(int, scope_identity())", this.Nome, this.Autor, this.Quantidade);

                        retorno = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = string.Format("UPDATE Livros SET nome='{1}', autor='{2}', quantidade={3} WHERE id={0}", this.Id, this.Nome, this.Autor, this.Quantidade);

                        if(comando.ExecuteNonQuery() > 0)
                        {
                            retorno = this.Id;
                        }
                    }

                }
            }

            return retorno;
        }
    }
}