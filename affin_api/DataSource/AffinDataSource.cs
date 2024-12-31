using System.Data.SqlClient;

namespace affin_api.DataSource
{
    public class AffinDataSource
    {
        private IConfiguration _configuration { get; }
        public SqlConnection Connection { get; set; }

        public AffinDataSource(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("Test_DB"));
        }
    }
}
