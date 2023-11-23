using DbAccess.Models;
using DbAccess.Repository.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace DbAccess.Repository
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly string _connectionString;
        public LocacaoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Locacao entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Locacoes (ClienteId, CarroId, DataLocacao, DataDevolucao, ValorTotal) VALUES (@ClienteId, @CarroId, @DataLocacao, @DataDevolucao, @ValorTotal)";
                    cmd.Parameters.AddWithValue("@ClienteId", entity.ClienteId);
                    cmd.Parameters.AddWithValue("@CarroId", entity.CarroId);
                    cmd.Parameters.AddWithValue("@DataLocacao", entity.DataLocacao);
                    cmd.Parameters.AddWithValue("@DataDevolucao", entity.DataDevolucao);
                    cmd.Parameters.AddWithValue("@ValorTotal", entity.ValorTotal);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Erro ao executar ADD Locacao");
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
                    cmd.CommandText = "DELETE FROM Locacoes WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar DELETE Locacao");
                    Console.WriteLine(ex.Message);
                }

            }
        }

        public IEnumerable<Locacao> Find(Expression<Func<Locacao, bool>> predicate)
        {
            IEnumerable<Locacao> clientes = new List<Locacao>();
            try
            {
                clientes = GetAll().AsQueryable().Where(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar FIND Locacao");
                Console.WriteLine(ex.Message);
            }
            return clientes;
        }

        public Locacao Get(int id)
        {
            Locacao locacao = new Locacao();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Locacoes WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = con;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        locacao.Id = Convert.ToInt32(rdr["id"]);
                        locacao.ClienteId = Convert.ToInt32(rdr["ClienteId"]);
                        locacao.CarroId = Convert.ToInt32(rdr["CarroId"]);
                        locacao.DataLocacao = Convert.ToDateTime(rdr["DataLocacao"]);
                        locacao.DataDevolucao = Convert.ToDateTime(rdr["DataDevolucao"]);
                        locacao.ValorTotal = Convert.ToDecimal(rdr["ValorTotal"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar GET Locacao");
                    Console.WriteLine(ex.Message);
                }

            }
            return locacao;
        }

        public IEnumerable<Locacao> GetAll()
        {
            List<Locacao> locacoes = new List<Locacao>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Locacoes";
                    cmd.Connection = con;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Locacao novaLocacao = new Locacao();
                        novaLocacao.Id = Convert.ToInt32(rdr["id"]);
                        novaLocacao.ClienteId = Convert.ToInt32(rdr["ClienteId"]);
                        novaLocacao.CarroId = Convert.ToInt32(rdr["CarroId"]);
                        novaLocacao.DataLocacao = Convert.ToDateTime(rdr["DataLocacao"]);
                        novaLocacao.DataDevolucao = Convert.ToDateTime(rdr["DataDevolucao"]);
                        novaLocacao.ValorTotal = Convert.ToDecimal(rdr["ValorTotal"]);
                        locacoes.Add(novaLocacao);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar GETALL Clientes");
                    Console.WriteLine(ex.Message);
                }
                return locacoes;
            }
        }

        public void Update(Locacao entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE Locacoes SET ClienteId=@ClienteID, CarroId=@CarroId, DataLocacao=@DataLocacao, DataDevolucao=@DataDevolucao, ValorTotal=@ValorTotal WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", entity.Id);
                    cmd.Parameters.AddWithValue("@ClienteId", entity.ClienteId);
                    cmd.Parameters.AddWithValue("@CarroId", entity.CarroId);
                    cmd.Parameters.AddWithValue("@DataLocacao", entity.DataLocacao);
                    cmd.Parameters.AddWithValue("@DataDevolucao", entity.DataDevolucao);
                    cmd.Parameters.AddWithValue("@ValorTotal", entity.ValorTotal);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar Update Locacao");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}