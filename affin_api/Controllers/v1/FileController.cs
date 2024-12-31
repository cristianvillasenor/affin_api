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
    public class FileController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly BLFile _blFile;

        public FileController(IConfiguration configuration)
        {
            _configuration = configuration;
            _blFile = new BLFile(_configuration);
        }

        [HttpGet("GetFileTypes")]
        public DataResponse<List<FileType>> GetFileTypes()
        {
            DataResponse<List<FileType>>? response = null;
            List<FileType>? list = null;

            try
            {
                list = _blFile.GetFileTypes();
                if (list.Count > 0 || list != null)
                    response = new DataResponse<List<FileType>>(list, OperationResult.SUCCESS, CustomConstants.SUCCESS_MESSAGE);
                else
                    response = new DataResponse<List<FileType>>(list, OperationResult.SUCCESS, CustomConstants.EMPTY_LIST);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }

        [HttpPost("SaveFile")]
        public DataResponse<bool> SaveFile(ProspectFiles data)
        {
            DataResponse<bool>? response = null;
            bool result = false;

            try
            {
                result = _blFile.SaveFile(data);
                response = new DataResponse<bool>(result, OperationResult.SUCCESS, CustomConstants.SUCCESS_MESSAGE);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }
    }
}
