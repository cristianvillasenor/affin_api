using affin_api.DataSource;
using affin_objects;
using System.Data;
using System.Data.SqlClient;
using utils_library.DataBaseTools;
using utils_library.Exceptions;

namespace affin_api.Models.DataAccess
{
    public class DAFile
    {
        private readonly IConfiguration _configuration;
        private readonly AffinDataSource _dataSource;

        public DAFile(IConfiguration configuration)
        {
            _configuration = configuration;
            _dataSource = new AffinDataSource(_configuration);
        }

        public List<FileType> GetFileTypes()
        {
            List<FileType>? list = null;
            string query = "SELECT * FROM TblFileType;";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.Text, _dataSource.Connection, query);
                    {
                        using (DataSet result = DataBaseTools.ExecuteDataSet(command))
                        {
                            if (result.Tables.Count > 0)
                            {
                                list = new List<FileType>();
                                foreach (DataRow row in result.Tables[0].Rows)
                                {
                                    FileType type = new()
                                    {
                                        FileTypeId = (Int32)row[0],
                                        Description = DataBaseTools.GetStringFromDataRow(row, 1),
                                    };
                                    list.Add(type);
                                }
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

        public bool SaveFile(ProspectFiles data)
        {
            bool response = false;
            string procedure = "SpSaveFile";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.StoredProcedure, _dataSource.Connection, procedure);
                    command.Parameters.AddWithValue("@ProspectId", data.ProspectId);
                    command.Parameters.AddWithValue("@FileType", data.FileType);
                    command.Parameters.AddWithValue("@FilePath", data.FilePath);
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
    }
}
