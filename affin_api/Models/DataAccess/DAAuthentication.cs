using affin_api.DataSource;
using affin_objects;
using System.Data;
using System.Data.SqlClient;
using utils_library.DataBaseTools;
using utils_library.Exceptions;

namespace affin_api.Models.DataAccess
{
    public class DAAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly AffinDataSource _dataSource;

        public DAAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
            _dataSource = new AffinDataSource(_configuration);
        }

        public User Login(Credentials data)
        {
            User? user = null;
            string procedure = "SpLogin";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.StoredProcedure, _dataSource.Connection, procedure);
                    command.Parameters.AddWithValue("@Username", data.Username);
                    command.Parameters.AddWithValue("@Password", data.Password);
                    using (DataSet result = DataBaseTools.ExecuteDataSet(command))
                    {
                        if (result.Tables.Count > 0)
                        {
                            foreach (DataRow row in result.Tables[0].Rows)
                            {
                                user = new()
                                {
                                    UserId = (Int32)row[0],
                                    Username = data.Username,
                                    ProspectId = DataBaseTools.GetIntFromDataRow(row, 2),
                                    TipoProspecto = DataBaseTools.GetIntFromDataRow(row, 3),
                                    RazonSocial = DataBaseTools.GetStringFromDataRow(row, 4),
                                    RFC = DataBaseTools.GetStringFromDataRow(row, 5),
                                    DireccionFiscal = DataBaseTools.GetStringFromDataRow(row, 6),
                                    TelefonoCelular = DataBaseTools.GetStringFromDataRow(row, 7),
                                    CorreoElectronico = DataBaseTools.GetStringFromDataRow(row, 8),
                                    RegisterDate = DataBaseTools.GetStringFromDataRow(row, 9),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return user;
        }
    }
}
