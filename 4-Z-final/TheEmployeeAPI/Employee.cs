public class Employee : AuditableEntity
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? SocialSecurityNumber { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public List<EmployeeBenefit> Benefits { get; set; } = new List<EmployeeBenefit>();
}

public class Benefit
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal BaseCost { get; set; }
}

public class EmployeeBenefit
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int BenefitId { get; set; }
    public Benefit Benefit { get; set; } = null!;
    public decimal? CostToEmployee { get; set; }
}


public abstract class AuditableEntity
{
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}