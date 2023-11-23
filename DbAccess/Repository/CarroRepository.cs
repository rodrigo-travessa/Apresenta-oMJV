using DbAccess.Models;
using DbAccess.Repository.IRepository;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DbAccess.Repository
{
    public class CarroRepository : ICarroRepository
    {
        private readonly string _connectionString;
        public CarroRepository(string connectionString)
        {
            _connectionString = connectionString;

        }
        public void Add(Carro entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Carros (Modelo, Marca, PrecoDiaria, PrecoKM, Estado) VALUES (@Modelo, @Marca, @PrecoDiaria, @PrecoKM, 0)";
                    cmd.Parameters.AddWithValue("@Modelo", entity.Modelo);
                    cmd.Parameters.AddWithValue("@Marca", entity.Marca);
                    cmd.Parameters.AddWithValue("@PrecoDiaria", entity.PrecoDiaria);
                    cmd.Parameters.AddWithValue("@PrecoKM", entity.PrecoKM);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Erro ao executar ADD CARRO");
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
                    cmd.CommandText = "DELETE FROM Carros WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar DELETE CARRO");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public IEnumerable<Carro> Find(Expression<Func<Carro, bool>> predicate)
        {
            IEnumerable<Carro> carros = new List<Carro>();
            try
            {
                carros = GetAll().AsQueryable().Where(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar FIND CARRO");
                Console.WriteLine(ex.Message);
            }
            return carros;
        }

        public Carro Get(int id)
        {
            Carro carro = new Carro();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Carros WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = con;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        carro.Id = Convert.ToInt32(rdr["Id"]);
                        carro.Modelo = rdr["Modelo"].ToString();
                        carro.Marca = rdr["Marca"].ToString();
                        carro.Estado = Convert.ToInt32(rdr["Estado"]);
                        carro.PrecoDiaria = Convert.ToDecimal(rdr["PrecoDiaria"]);
                        carro.PrecoKM = Convert.ToDecimal(rdr["PrecoKM"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar GET CARRO");
                    Console.WriteLine(ex.Message);
                }

            }
            return carro;
        }

        public IEnumerable<Carro> GetAll()
        {
            List<Carro> carros = new List<Carro>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                var cmd = new SqlCommand();
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Carros";
                cmd.Connection = con;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Carro novoCarro = new Carro();
                    novoCarro.Id = Convert.ToInt32(rdr["id"]);
                    novoCarro.Modelo = rdr["Modelo"].ToString();
                    novoCarro.Marca = rdr["Marca"].ToString();
                    novoCarro.Estado = Convert.ToInt32(rdr["Estado"]);
                    novoCarro.PrecoDiaria = Convert.ToDecimal(rdr["PrecoDiaria"]);
                    novoCarro.PrecoKM = Convert.ToDecimal(rdr["PrecoKM"]);

                    carros.Add(novoCarro);

                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar GETALL CARRO");
                    Console.WriteLine(ex.Message);
                }
                return carros;
            }
        }

        public void Update(Carro entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    var cmd = new SqlCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE Carros SET Modelo=@Modelo, Marca=@Marca, PrecoDiaria=@PrecoDiaria, PrecoKM=@PrecoKM, Estado=@Estado WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", entity.Id);
                    cmd.Parameters.AddWithValue("@Modelo", entity.Modelo);
                    cmd.Parameters.AddWithValue("@Marca", entity.Marca);
                    cmd.Parameters.AddWithValue("@PrecoDiaria", entity.PrecoDiaria);
                    cmd.Parameters.AddWithValue("@PrecoKM", entity.PrecoKM);
                    cmd.Parameters.AddWithValue("@Estado", entity.Estado);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar Update CARRO");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
