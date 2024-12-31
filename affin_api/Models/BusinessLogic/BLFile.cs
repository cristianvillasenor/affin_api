using affin_api.Models.DataAccess;
using affin_objects;
using utils_library.Exceptions;

namespace affin_api.Models.BusinessLogic
{
    public class BLFile
    {
        private readonly IConfiguration _configuration;
        private readonly DAFile _daFile;

        public BLFile(IConfiguration configuration)
        {
            _configuration = configuration;
            _daFile = new DAFile(_configuration);
        }

        public List<FileType> GetFileTypes()
        {
            List<FileType>? list = null;

            try
            {
                list = _daFile.GetFileTypes();
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

            try
            {
                response = _daFile.SaveFile(data);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }
    }
}
