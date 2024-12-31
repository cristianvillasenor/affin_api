using affin_api.Models.DataAccess;
using affin_objects;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using utils_library.AESAlgorithm;
using utils_library.Exceptions;
using utils_library.Responses;

namespace affin_api.Models.BusinessLogic
{
    public class BLProspect
    {
        private readonly IConfiguration _configuration;
        private DAProspect _daProspect;
        private string _secretKey;

        public BLProspect(IConfiguration configuration)
        {
            _configuration = configuration;
            _daProspect = new DAProspect(_configuration);
            _secretKey = _configuration["AESAlgorith:SecretKey"];
        }

        public List<Prospect> GetProspectsList(int status)
        {
            List<Prospect>? list = null;

            try
            {
                list = _daProspect.GetProspectsList(status);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return list;
        }

        public int RegisterProspect(Prospect data)
        {
            int prospectId = 0;

            try
            {
                prospectId = _daProspect.RegisterProspect(data);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return prospectId;
        }

        public bool EditProspect(Prospect data)
        {
            bool response = false;

            try
            {
                response = _daProspect.EditProspect(data);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }

        public int ChangeProspectStatus(ProspectStatusRequest data)
        {
            int userId = 0;
            bool result = false;
            string secretKey = _secretKey.Trim();

            try
            {
                result = _daProspect.ChangeProspectStatus(data);
                if (data.StatusId == 3)
                {
                    _daProspect = new DAProspect(_configuration);

                    if (secretKey.Length != 50)
                        throw new ArgumentException("La clave secreta debe tener exactamente 50 caracteres.");

                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] keyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
                        string derivedKey = Convert.ToBase64String(keyBytes);
                        data.Password = AESAlgorithm.Encrypt(data.Password, derivedKey);
                    }

                    userId = _daProspect.CreateUser(data);
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
