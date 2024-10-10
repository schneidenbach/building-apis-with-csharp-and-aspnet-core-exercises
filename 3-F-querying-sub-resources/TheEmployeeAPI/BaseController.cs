using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : Controller
{
}
