using insuranceLeadApi.Controllers.Model;
using insuranceLeadApi.Models;
using insuranceLeadApi.Models.Dto;
using insuranceLeadApi.Models.ViewModels;
using insuranceLeadApi.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace insuranceLeadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHolderController : ControllerBase
    {
        private readonly InsuranceDBContext dBContext;
        private  PolicyHolderService policyHolderService;

        public PolicyHolderController(InsuranceDBContext dBContext, PolicyHolderService policyHolderService)
        {
            this.dBContext = dBContext;
            this.policyHolderService = policyHolderService; 
        }

        // GET: api/<PolicyHolderController>
        [HttpGet]
        public IActionResult GetAllPolicyHolders(int Size = 10, int Page = 0, String Search = null)
        {
           var response = new PolicyHolderRestModel<PaginatedList<PolicyHolderDTO>>();

            try
            {
                response.data = policyHolderService.FindAll(Size, Page, Search);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex) { 
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.data = null;
                response.Messages.Add(ex.Message);
                response.Messages.Add(ex.ToString());

            }
            return StatusCode((int)response.StatusCode, response);
        }

        // GET api/<PolicyHolderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var response = new PolicyHolderRestModel<PolicyHolderDTO>();

            try
            {
                response.data = policyHolderService.FindById(id);
            
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.data = null;
                response.Messages.Add(ex.Message);
                response.Messages.Add(ex.ToString());

            }
            if (response.data == null && response.Messages.Count == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages.Add("Policy Holder not found"); 
            }
            return StatusCode((int)response.StatusCode, response);
        }

        // POST api/<PolicyHolderController>
        [HttpPost]
        public IActionResult Post(PolicyHolderDTO policyHolderRequest)
        {
            var response = new PolicyHolderRestModel<PolicyHolder>();

            try
            {
                response.data = policyHolderService.SavePolicyHolder(policyHolderRequest);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.data = null;
                response.Messages.Add(ex.Message);
                response.Messages.Add(ex.ToString());

            }
            return StatusCode((int)response.StatusCode, response);
        }

        // PUT api/<PolicyHolderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, PolicyHolderDTO holderDTO)
        {
            var response = new PolicyHolderRestModel<bool>();

            try
            {
                policyHolderService.Update(id, holderDTO);
                response.data = true;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.data = false;
                response.Messages.Add(ex.Message);
                response.Messages.Add(ex.ToString());

            }
            return StatusCode((int)response.StatusCode, response);
        }

        // DELETE api/<PolicyHolderController>/5
        [HttpDelete("{id}")]
        public  IActionResult Delete(long id)
        {
            var response = new PolicyHolderRestModel<bool>();

            try
            {
                policyHolderService.DeleteById(id);
                response.data = true;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.data = false;
                response.Messages.Add(ex.Message);
                response.Messages.Add(ex.ToString());

            }
            return StatusCode((int)response.StatusCode, response);

        }
    }
}
