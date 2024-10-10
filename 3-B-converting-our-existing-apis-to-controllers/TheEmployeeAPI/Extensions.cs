using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public static class Extensions
{
    public static ValidationProblemDetails ToValidationProblemDetails(this List<ValidationResult> validationResults)
    {
        var problemDetails = new ValidationProblemDetails();

        foreach (var validationResult in validationResults)
        {
            foreach (var memberName in validationResult.MemberNames)
            {
                if (problemDetails.Errors.ContainsKey(memberName))
                {
                    problemDetails.Errors[memberName] = problemDetails.Errors[memberName].Concat([validationResult.ErrorMessage]).ToArray()!;
                }
                else
                {
                    problemDetails.Errors[memberName] = new List<string> { validationResult.ErrorMessage! }.ToArray();
                }
            }
        }

        return problemDetails;
    }
}
