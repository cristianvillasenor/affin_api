using affin_api.Models.BusinessLogic;
using affin_objects;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using utils_library.CustomConstants;
using utils_library.Exceptions;
using utils_library.Responses;

namespace affin_api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class ProspectController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly BLProspect _blProspect;

        public ProspectController(IConfiguration configuration)
        {
            _configuration = configuration;
            _blProspect = new BLProspect(_configuration);
        }

        [HttpGet("GetProspectsList/{statusId}")]
        //[Authorize]
        public DataResponse<List<Prospect>> GetProspects(int statusId)
        {
            DataResponse<List<Prospect>>? response = null;
            List<Prospect>? prospects = null;
            string message = "";

            try
            {
                prospects = _blProspect.GetProspectsList(statusId);
                if (prospects != null || prospects.Count > 0)
                    message = CustomConstants.SUCCESS_MESSAGE;
                else
                    message = CustomConstants.EMPTY_LIST;

                response = new DataResponse<List<Prospect>>(prospects, OperationResult.SUCCESS, message);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }

        [HttpPost("RegisterProspect")]
        public DataResponse<int> RegisterProspect(Prospect data)
        {
            DataResponse<int>? response = null;
            int prospectId = 0;

            try
            {
                prospectId = _blProspect.RegisterProspect(data);
                response = new DataResponse<int>(prospectId, OperationResult.SUCCESS, CustomConstants.SUCCESS_MESSAGE);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }

        [HttpPut("EditProspect")]
        //[Authorize]
        public DataResponse<bool> EditProspect(Prospect data)
        {
            DataResponse<bool>? response = null;
            bool result = false;

            try
            {
                result = _blProspect.EditProspect(data);
                response = new DataResponse<bool>(result, OperationResult.SUCCESS, CustomConstants.SUCCESS_MESSAGE);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }

        [HttpPut("ChangeProspectStatus")]
        //[Authorize]
        public DataResponse<int> ChangeProspectStatus(ProspectStatusRequest data)
        {
            DataResponse<int>? response = null;
            int userId = 0;

            try
            {
                userId = _blProspect.ChangeProspectStatus(data);
                response = new DataResponse<int>(userId, OperationResult.SUCCESS, CustomConstants.SUCCESS_MESSAGE);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            return response;
        }
    }
}
