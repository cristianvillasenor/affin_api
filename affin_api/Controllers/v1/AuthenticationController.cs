using affin_api.Models.BusinessLogic;
using affin_objects;
using affin_objects.Responses;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using utils_library.Exceptions;
using utils_library.Responses;

namespace affin_api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly BLAuthentication _blAuthentication;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _blAuthentication = new BLAuthentication(_configuration);
        }

        [HttpPost("Login")]
        public DataResponse<LoginResponse> Login(Credentials data)
        {
            DataResponse<LoginResponse>? response = null;
            LoginResponse? login = null;

            try
            {
                login = _blAuthentication.Login(data);
                string message = "";

                if (login != null)
                    message = "Operación ejecutada correctamente.";
                else
                    message = $"El usuario {data.Username} no existe";

                response = new DataResponse<LoginResponse>(login, OperationResult.SUCCESS, message);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }
    }
}
