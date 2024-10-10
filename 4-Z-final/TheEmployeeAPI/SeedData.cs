

using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void MigrateAndSeed(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        if (!context.Employees.Any())
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    SocialSecurityNumber = "123-45-6789",
                    Address1 = "123 Main St",
                    City = "Anytown",
                    State = "NY",
                    ZipCode = "12345",
                    PhoneNumber = "555-123-4567",
                    Email = "john.doe@example.com"
                },
                new Employee
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    SocialSecurityNumber = "987-65-4321",
                    Address1 = "456 Elm St",
                    Address2 = "Apt 2B",
                    City = "Othertown",
                    State = "CA",
                    ZipCode = "98765",
                    PhoneNumber = "555-987-6543",
                    Email = "jane.smith@example.com"
                }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        if (!context.Benefits.Any())
        {
            var benefits = new List<Benefit>
            {
                new Benefit { Name = "Health", Description = "Medical, dental, and vision coverage", BaseCost = 100.00m },
                new Benefit { Name = "Dental", Description = "Dental coverage", BaseCost = 50.00m },
                new Benefit { Name = "Vision", Description = "Vision coverage", BaseCost = 30.00m }
            };

            context.Benefits.AddRange(benefits);
            context.SaveChanges();

            //add employee benefits too

            var healthBenefit = context.Benefits.Single(b => b.Name == "Health");
            var dentalBenefit = context.Benefits.Single(b => b.Name == "Dental");
            var visionBenefit = context.Benefits.Single(b => b.Name == "Vision");

            var john = context.Employees.Single(e => e.FirstName == "John");
            context.AddRange(new List<EmployeeBenefit>
            {
                new EmployeeBenefit { EmployeeId = john.Id, BenefitId = healthBenefit.Id, CostToEmployee = 100m},
                new EmployeeBenefit { EmployeeId = john.Id, BenefitId = dentalBenefit.Id }
            });

            var jane = context.Employees.Single(e => e.FirstName == "Jane");
            context.AddRange(new List<EmployeeBenefit>
            {
                new EmployeeBenefit { EmployeeId = jane.Id, BenefitId = healthBenefit.Id, CostToEmployee = 120m},
                new EmployeeBenefit { EmployeeId = jane.Id, BenefitId = visionBenefit.Id }
            });

            context.SaveChanges();
        }
    }
}