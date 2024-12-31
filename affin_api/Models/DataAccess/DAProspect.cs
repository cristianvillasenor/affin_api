using affin_api.DataSource;
using affin_objects;
using System.Data;
using System.Data.SqlClient;
using utils_library.DataBaseTools;
using utils_library.Exceptions;
using utils_library.Responses;

namespace affin_api.Models.DataAccess
{
    public class DAProspect
    {
        private readonly IConfiguration _configuration;
        private readonly AffinDataSource _dataSource;

        public DAProspect(IConfiguration configuration)
        {
            _configuration = configuration;
            _dataSource = new AffinDataSource(_configuration);
        }

        public List<Prospect> GetProspectsList(int statusId)
        {
            List<Prospect>? list = null;
            string query = $"SELECT * FROM TblProspects WHERE TipoProspecto != 1 AND ProspectStatudId = {statusId};";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.Text, _dataSource.Connection, query);
                    using (DataSet result = DataBaseTools.ExecuteDataSet(command))
                    {
                        if (result.Tables.Count > 0)
                        {
                            list = new List<Prospect>();
                            foreach (DataRow row in result.Tables[0].Rows)
                            {
                                Prospect prospect = new()
                                {
                                    ProspectoId = (Int32)row[0],
                                    TipoProspecto = DataBaseTools.GetIntFromDataRow(row, 1),
                                    RazonSocial = DataBaseTools.GetStringFromDataRow(row, 2),
                                    RFC = DataBaseTools.GetStringFromDataRow(row, 3),
                                    DireccionFiscal = DataBaseTools.GetStringFromDataRow(row, 4),
                                    TelefonoCelular = DataBaseTools.GetStringFromDataRow(row, 5),
                                    CorreoElectronico = DataBaseTools.GetStringFromDataRow(row, 6),
                                    NumeroCuentaBancaria = DataBaseTools.GetStringFromDataRow(row, 7),
                                    AreasOperacion = DataBaseTools.GetStringFromDataRow(row, 8),
                                    CultivoPrincipal = DataBaseTools.GetStringFromDataRow(row, 9),
                                    UbicacionPlantios = DataBaseTools.GetStringFromDataRow(row, 10),
                                    TieneSeguro = DataBaseTools.GetIntFromDataRow(row, 11),
                                    StatusProspecto = DataBaseTools.GetIntFromDataRow(row, 12),
                                    UltimaActualizacion = DataBaseTools.GetStringFromDataRow(row, 13),
                                };
                                list.Add(prospect);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return list;
        }

        public int RegisterProspect(Prospect prospect)
        {
            int prospectId = 0;
            string stored = "SpInsertProspect";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.StoredProcedure, _dataSource.Connection, stored);
                    command.Parameters.AddWithValue("@TipoProspecto", prospect.TipoProspecto);
                    command.Parameters.AddWithValue("@RazonSocial", prospect.RazonSocial);
                    command.Parameters.AddWithValue("@RFC", prospect.RFC);
                    command.Parameters.AddWithValue("@DireccionFiscal", prospect.DireccionFiscal);
                    command.Parameters.AddWithValue("@TelefonoCelular", prospect.TelefonoCelular);
                    command.Parameters.AddWithValue("@CorreoElectronico", prospect.CorreoElectronico);
                    command.Parameters.AddWithValue("@NumeroCuentaBancaria", (object)prospect.NumeroCuentaBancaria ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AreasOperacion", (object)prospect.AreasOperacion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CultivoPrincipal", (object)prospect.CultivoPrincipal ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UbicacionPlantios", (object)prospect.UbicacionPlantios ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TieneSeguro", prospect.TieneSeguro);
                    using (DataSet result = DataBaseTools.ExecuteDataSet(command))
                    {
                        if (result.Tables.Count > 0)
                        {
                            foreach (DataRow row in result.Tables[0].Rows)
                            {
                                prospectId = Int32.Parse(row[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return prospectId;
        }

        public bool EditProspect(Prospect prospect)
        {
            bool response = false;
            string stored = "SpUpdateProspect";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.StoredProcedure, _dataSource.Connection, stored);
                    command.Parameters.AddWithValue("@ProspectoId", prospect.ProspectoId);
                    command.Parameters.AddWithValue("@TipoProspecto", prospect.TipoProspecto);
                    command.Parameters.AddWithValue("@RazonSocial", prospect.RazonSocial);
                    command.Parameters.AddWithValue("@RFC", prospect.RFC);
                    command.Parameters.AddWithValue("@DireccionFiscal", prospect.DireccionFiscal);
                    command.Parameters.AddWithValue("@TelefonoCelular", prospect.TelefonoCelular);
                    command.Parameters.AddWithValue("@CorreoElectronico", prospect.CorreoElectronico);
                    command.Parameters.AddWithValue("@NumeroCuentaBancaria", (object)prospect.NumeroCuentaBancaria ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AreasOperacion", (object)prospect.AreasOperacion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CultivoPrincipal", (object)prospect.CultivoPrincipal ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UbicacionPlantios", (object)prospect.UbicacionPlantios ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TieneSeguro", prospect.TieneSeguro);
                    using (DataSet result = DataBaseTools.ExecuteDataSet(command))
                    {
                        if (result.Tables.Count > 0)
                        {
                            foreach (DataRow row in result.Tables[0].Rows)
                            {
                                if ((Int32)row[0] == 0)
                                    response = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }

        public bool ChangeProspectStatus(ProspectStatusRequest data)
        {
            bool response = false;
            string query = $"UPDATE TblProspects SET ProspectStatudId = {data.StatusId}, UltimaActualizacion = GETDATE() WHERE ProspectoId = {data.ProspectId}";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.Text, _dataSource.Connection, query);
                    using (DataSet result = DataBaseTools.ExecuteDataSet(command))
                    {
                        response = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }

        public int CreateUser(ProspectStatusRequest data)
        {
            int userId = 0;
            string stored = "SpCreateUser";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.StoredProcedure, _dataSource.Connection, stored);
                    command.Parameters.AddWithValue("@Username", data.Username);
                    command.Parameters.AddWithValue("@Password", data.Password);
                    command.Parameters.AddWithValue("@ProspectId", data.ProspectId);
                    using (DataSet result = DataBaseTools.ExecuteDataSet(command))
                    {
                        if (result.Tables.Count > 0)
                        {
                            foreach (DataRow row in result.Tables[0].Rows)
                            {
                                userId = Int32.Parse(row[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return userId;
        }
    }
}
