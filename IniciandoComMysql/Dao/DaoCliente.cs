using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniciandoComMysql.Entities;
using MySql.Data.MySqlClient;

namespace IniciandoComMysql.Dao
{
    public class DaoCliente
    {
        
        MySqlConnection Conexao;

        public void Salvar(Cliente cliente)
        {
            try
            {
                Conexao = new MySqlConnection(DaoConexao.Conexao);
                Conexao.Open();
                
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = Conexao;
                comando.Prepare();
                comando.CommandText = "INSERT INTO contato (nome, email, telefone, endereco) VALUES(@nome, @email, @telefone, @endereco)";
                
                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@email", cliente.Email);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@endereco", cliente.Endereco);

                comando.ExecuteNonQuery();
            } catch (MySqlException erro)
            {
                throw new Exception(erro.Message);
            } finally
            {
                Conexao.Close();
            }
        }

        public List<Cliente> Pesquisa()
        {
            try
            {
                Conexao = new MySqlConnection(DaoConexao.Conexao);
                Conexao.Open();

                MySqlCommand comando = new MySqlCommand();
                comando.Connection = Conexao;
                comando.Prepare();
                comando.CommandText = "SELECT * FROM contato";

                MySqlDataReader reader = comando.ExecuteReader();

                List<Cliente> clientes = new List<Cliente>();

                while (reader.Read())
                {
                    clientes.Add(new Cliente(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                        )
                    );
                }
                return clientes;
            } catch (MySqlException erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }

        public List<Cliente> Pesquisa(string text)
        {
            try
            {
                Conexao = new MySqlConnection(DaoConexao.Conexao);
                Conexao.Open();

                MySqlCommand comando = new MySqlCommand();
                comando.Connection = Conexao;
                comando.Prepare();
                comando.CommandText = "SELECT * FROM contato WHERE nome LIKE @nome OR email LIKE @nome";

                comando.Parameters.AddWithValue("@nome", $"%{text}%");
                MySqlDataReader reader = comando.ExecuteReader();

                List<Cliente> clientes = new List<Cliente>();
                while (reader.Read())
                {
                    clientes.Add(new Cliente(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    ));
                }
                return clientes;

            } catch (MySqlException erro)
            {
                throw new Exception(erro.Message);
            } catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                Conexao.Close();
            }

        }
    }

}
