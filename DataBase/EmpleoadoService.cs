using Microsoft.Data.SqlClient;

namespace ama_zon.DataBase
{
    public class EmpleoadoService
    {
        private readonly DbConnection _dbConnection;

        public EmpleoadoService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void SaveEmployees(List<Empleado> employeeList)
        {
            using (var connection = _dbConnection.OpenConnection())
            {
                var transaction = connection.BeginTransaction();

                try
                {
                    foreach (var employee in employeeList)
                    {
                        string insertQuery = "INSERT INTO Employee (Identificacion, Nombre, Apellido, Direccion, FechaNacimiento, Correo, PaisId, AcuerdoId) " +
                                             "VALUES (@Identificacion, @Nombre, @Apellido, @Direccion, @FechaNacimiento, @Correo, @PaisId, @AcuerdoId)";

                        using (var command = new SqlCommand(insertQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Identificacion", employee.Identificacion);
                            command.Parameters.AddWithValue("@Nombre", employee.Nombre);
                            command.Parameters.AddWithValue("@Apellido", employee.Apellido);
                            command.Parameters.AddWithValue("@Direccion", employee.Direccion);
                            command.Parameters.AddWithValue("@FechaNacimiento", employee.FechaNacimiento);
                            command.Parameters.AddWithValue("@Correo", employee.Correo);
                            command.Parameters.AddWithValue("@PaisId", employee.PaisId);
                            command.Parameters.AddWithValue("@AcuerdoId", employee.AcuerdoId);

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
