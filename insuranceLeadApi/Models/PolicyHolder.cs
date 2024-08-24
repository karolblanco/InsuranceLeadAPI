using System;
using System.Collections.Generic;

namespace insuranceLeadApi.Models;

public partial class PolicyHolder
{
    public long IdentificationNumber { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string SecondLastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public decimal EstimatedInsuranceValue { get; set; }

    public string? Remark { get; set; }
}
