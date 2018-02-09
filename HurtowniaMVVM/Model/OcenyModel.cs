using System.Data.SqlClient;

namespace HurtowniaMVVM.Model
{
    class OcenyModel
    {
        public int Id { get; set; }
        public int? SrednieOpoznienieDostawcy { get; set; }
        public int? SredniCzasTrwaniaDostawy { get; set; }
        public int? SredniaJakosc { get; set; }

        public static OcenyModel UzyskajOcenyDostawcy(long idDostawcy)
        {
            using (SqlConnection connection = new SqlConnection(DostawcaModel.connectionString))
            {
                connection.Open();

                string queryString = "SELECT * FROM dbo.SystemOcenyDostawcow WHERE IdDostawcy = " + idDostawcy;
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var id = (long)reader.GetValue(0);
                    return new OcenyModel
                    {
                        Id = (int)id,
                        SrednieOpoznienieDostawcy = reader.GetInt32(1),
                        SredniCzasTrwaniaDostawy = reader.GetInt32(2),
                        SredniaJakosc = reader.GetInt32(3)
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
