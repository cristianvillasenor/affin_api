using affin_api.Models.BusinessLogic;
using affin_objects;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using utils_library.CustomConstants;
using utils_library.Exceptions;
using utils_library.Responses;

namespace affin_api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly BLUser _blUser;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _blUser = new BLUser(_configuration);
        }

        [HttpGet("GetUserTypes")]
        public DataResponse<List<UserTypes>> GetUserTypes()
        {
            DataResponse<List<UserTypes>>? response = null;
            List<UserTypes>? list = null;

            try
            {
                list = _blUser.GetUserTypes();

                if (list!.Count > 0 || list != null)
                    response = new DataResponse<List<UserTypes>>(list, OperationResult.SUCCESS, CustomConstants.SUCCESS_MESSAGE);
                else
                    response = new DataResponse<List<UserTypes>>(list, OperationResult.SUCCESS, CustomConstants.EMPTY_LIST);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }
    }
}
