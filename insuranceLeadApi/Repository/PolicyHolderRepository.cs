using insuranceLeadApi.Models;
using insuranceLeadApi.Models.Dto;
using insuranceLeadApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace insuranceLeadApi.Repository
{
    public class PolicyHolderRepository
    {
        private readonly InsuranceDBContext dBContext;

        public PolicyHolderRepository(InsuranceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public PaginatedList<PolicyHolderDTO> FindAll(int Size, int Page, string Search)
        {
            PaginatedList<PolicyHolderDTO> result = new PaginatedList<PolicyHolderDTO>();

            var query = dBContext.PolicyHolders.Select(x => new PolicyHolderDTO
            {
                IdentificationNumber = x.IdentificationNumber,
                FirstName = x.FirstName + " " + (x.MiddleName != null ? (" " + x.MiddleName) : "" ),
                LastName = x.LastName,
                SecondLastName = x.SecondLastName,
                Phone = x.Phone,
                BirthDate = x.BirthDate,
                Email = x.Email,
                EstimatedInsuranceValue = x.EstimatedInsuranceValue,
                Remark = x.Remark
            });

            if (!string.IsNullOrEmpty(Search)) {

                query = query.Where(x => x.IdentificationNumber.ToString().Contains(Search));
            }
            result.Total = query.Count();
            result.Element = query
                .OrderBy(x => x.IdentificationNumber)
                .Skip(Page * Size)
                .Take(Size)
                .ToList();

            return result;
        }


        public PolicyHolderDTO FindById(long id)
        {

           var  result = dBContext.PolicyHolders.Where(x => x.IdentificationNumber == id).Select(x => new PolicyHolderDTO
            {
                IdentificationNumber = x.IdentificationNumber,
                FirstName = x.FirstName + " " + (x.MiddleName != null ? (" " + x.MiddleName) : ""),
                LastName = x.LastName,
                SecondLastName = x.SecondLastName,
                Phone = x.Phone,
                BirthDate = x.BirthDate,
                Email = x.Email,
                EstimatedInsuranceValue = x.EstimatedInsuranceValue,
                Remark = x.Remark
            }).FirstOrDefault();

            return result;
        }

        public PolicyHolder FindEntityById(long id)
        {

            var result = dBContext.PolicyHolders.Where(x => x.IdentificationNumber == id).Select(x => new PolicyHolder
            {
                IdentificationNumber = x.IdentificationNumber,
                FirstName = x.FirstName + " " + (x.MiddleName != null ? (" " + x.MiddleName) : ""),
                LastName = x.LastName,
                SecondLastName = x.SecondLastName,
                Phone = x.Phone,
                BirthDate = x.BirthDate,
                Email = x.Email,
                EstimatedInsuranceValue = x.EstimatedInsuranceValue,
                Remark = x.Remark
            }).FirstOrDefault();

            return result;
        }

        public PolicyHolder Save(PolicyHolder PolicyHolder)
        {

            dBContext.PolicyHolders.Add(PolicyHolder);
            dBContext.SaveChanges();

            return PolicyHolder;
        }

        public  void Update(long currentId , PolicyHolderDTO PolicyHolder)
        {
            PolicyHolder existingPolicyHolder = FindEntityById(currentId);

            if (existingPolicyHolder != null) {
                dBContext.PolicyHolders.Remove(existingPolicyHolder);
            }
            var newPolicyHolder = new PolicyHolder
            {
                IdentificationNumber = PolicyHolder.IdentificationNumber,
                FirstName = PolicyHolder.FirstName,
                MiddleName = PolicyHolder.MiddleName,
                LastName = PolicyHolder.LastName,
                SecondLastName = PolicyHolder.SecondLastName,
                Phone = PolicyHolder.Phone,
                BirthDate = PolicyHolder.BirthDate.ToUniversalTime(),
                Email = PolicyHolder.Email,
                Remark = PolicyHolder.Remark,
                EstimatedInsuranceValue = PolicyHolder.EstimatedInsuranceValue

            };

            dBContext.PolicyHolders.Add(newPolicyHolder);
            dBContext.SaveChanges();


        }

        public void Delete (long id)
        {
            var policyHolder = dBContext.PolicyHolders.Find(id);
            if (policyHolder is not null)
            {
                dBContext.PolicyHolders.Remove(policyHolder);
                dBContext.SaveChanges();
            }
        }
    }
}
