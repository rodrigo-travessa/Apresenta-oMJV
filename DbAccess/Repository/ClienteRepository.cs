using DbAccess.Models;
using DbAccess.Repository.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace DbAccess.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;
        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Cliente entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Clientes (Nome, Sobrenome) VALUES (@Nome, @Sobrenome)";
                    cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", entity.Sobrenome);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Erro ao executar ADD Cliente");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM Clientes WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar DELETE Cliente");
                    Console.WriteLine(ex.Message);
                }
                
            }
        }

        public IEnumerable<Cliente> Find(Expression<Func<Cliente, bool>> predicate)
        {
            IEnumerable<Cliente> clientes = new List<Cliente>();
            try
            {
                clientes = GetAll().AsQueryable().Where(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar FIND Cliente");
                Console.WriteLine(ex.Message);
            }
            return clientes;
        }

        public Cliente Get(int id)
        {
            Cliente cliente = new Cliente();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Clientes WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = con;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        cliente.Id = Convert.ToInt32(rdr["Id"]);
                        cliente.Nome = rdr["Nome"].ToString();
                        cliente.Sobrenome = rdr["Sobrenome"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar GET Cliente");
                    Console.WriteLine(ex.Message);
                }

            }
            return cliente;
        }

        public IEnumerable<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Clientes";
                    cmd.Connection = con;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Cliente novoCliente = new Cliente();
                        novoCliente.Id = Convert.ToInt32(rdr["id"]);
                        novoCliente.Nome = rdr["Nome"].ToString();
                        novoCliente.Sobrenome = rdr["Sobrenome"].ToString();
                        clientes.Add(novoCliente);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar GETALL Clientes");
                    Console.WriteLine(ex.Message);
                }
                return clientes;
            }
        }

        public void Update(Cliente entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE Clientes SET Nome=@Nome, Sobrenome=@Sobrenome WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", entity.Id);
                    cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", entity.Sobrenome);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar Update Cliente");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
