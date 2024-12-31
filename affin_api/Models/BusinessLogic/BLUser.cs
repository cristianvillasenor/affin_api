using affin_api.Models.DataAccess;
using affin_objects;
using utils_library.Exceptions;

namespace affin_api.Models.BusinessLogic
{
    public class BLUser
    {
        private readonly IConfiguration _configuration;
        private readonly DAUser _daUser;

        public BLUser(IConfiguration configuration)
        {
            _configuration = configuration;
            _daUser = new DAUser(_configuration);
        }

        public List<UserTypes> GetUserTypes()
        {
            List<UserTypes>? list = null;

            try
            {
                list = _daUser.GetUserTypes();
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return list;
        }
    }
}
