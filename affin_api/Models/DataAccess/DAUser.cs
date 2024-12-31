using affin_api.DataSource;
using affin_objects;
using System.Data;
using System.Data.SqlClient;
using utils_library.DataBaseTools;
using utils_library.Exceptions;

namespace affin_api.Models.DataAccess
{
    public class DAUser
    {
        private readonly IConfiguration _configuration;
        private readonly AffinDataSource _dataSource;

        public DAUser(IConfiguration configuration)
        {
            _configuration = configuration;
            _dataSource = new AffinDataSource(_configuration);
        }

        public List<UserTypes> GetUserTypes()
        {
            List<UserTypes>? list = null;
            string query = "SELECT * FROM TblUserTypes WHERE UserTypeId != 1;";

            try
            {
                using (_dataSource.Connection)
                {
                    SqlCommand command = DataBaseTools.CreateSqlCommand(CommandType.Text, _dataSource.Connection, query);
                    using (DataSet result = DataBaseTools.ExecuteDataSet(command))
                    {
                        if (result.Tables.Count > 0)
                        {
                            list = new List<UserTypes>();
                            foreach (DataRow row in result.Tables[0].Rows)
                            {
                                UserTypes type = new()
                                {
                                    UserTypeId = (Int32)row[0],
                                    Description = DataBaseTools.GetStringFromDataRow(row, 1),
                                };
                                list.Add(type);
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
    }
}
