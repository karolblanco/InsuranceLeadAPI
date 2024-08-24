using insuranceLeadApi.Models;
using insuranceLeadApi.Models.Dto;
using insuranceLeadApi.Models.ViewModels;
using insuranceLeadApi.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace insuranceLeadApi.Service
{
    public class PolicyHolderService
    {
        private PolicyHolderRepository policyHolderRepository;

        public PolicyHolderService(PolicyHolderRepository Repository) {
            this.policyHolderRepository = Repository;
        
        }

        public PolicyHolder SavePolicyHolder(PolicyHolderDTO request)
        {
            PolicyHolderDTO existingPolicyHolder = this.policyHolderRepository.FindById(request.IdentificationNumber);
            if (existingPolicyHolder != null) 
            {
             throw new InvalidOperationException("A PolicyHolder with the same IdentificationNumber already exists.");

            }

            PolicyHolder domainModelPolicyHolder = new()
            {
                IdentificationNumber = request.IdentificationNumber,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                SecondLastName = request.SecondLastName,
                Phone = request.Phone,
                BirthDate = request.BirthDate,
                Email = request.Email,
                EstimatedInsuranceValue = request.EstimatedInsuranceValue,
                Remark = request.Remark

            };

            var policyHolder = policyHolderRepository.Save(domainModelPolicyHolder);
            

            return policyHolder;
        }

        public PolicyHolderDTO FindById(long id) {

         return  policyHolderRepository.FindById(id);

        }

        public PaginatedList<PolicyHolderDTO> FindAll(int Size, int Page, String Search)
        {

            return policyHolderRepository.FindAll(Size, Page, Search); ;

        }

        public void Update(long currentId, PolicyHolderDTO newPolicyHolderDTO) {
            PolicyHolderDTO currentPolicyHolderID = policyHolderRepository.FindById(currentId);
            if (currentPolicyHolderID == null)
            {
                throw new InvalidOperationException("The record to be updated does not exist");
            }

            PolicyHolderDTO newPolicyHolderID = policyHolderRepository.FindById(newPolicyHolderDTO.IdentificationNumber);
            if (newPolicyHolderID != null)
            {
                throw new InvalidOperationException("Cannot update: the new ID already exists");

            }

            policyHolderRepository.Update(currentId, newPolicyHolderDTO);
        }


        public void DeleteById(long id)
        {

          policyHolderRepository.Delete(id);

        }
    }
}
