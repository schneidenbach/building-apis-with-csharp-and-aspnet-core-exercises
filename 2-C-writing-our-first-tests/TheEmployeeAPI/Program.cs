var employees = new List<Employee>
{
    new Employee { Id = 1, FirstName = "John", LastName = "Doe" },
    new Employee { Id = 2, FirstName = "Jane", LastName = "Doe" }
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var employeeRoute = app.MapGroup("/employees");

employeeRoute.MapGet(string.Empty, () => {
    return employees;
});

employeeRoute.MapGet("{id:int}", (int id) => {
    var employee = employees.SingleOrDefault(e => e.Id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(employee);
});

employeeRoute.MapPost(string.Empty, (Employee employee) => {
    employee.Id = employees.Max(e => e.Id) + 1; // We're not using a database, so we need to manually assign an ID
    employees.Add(employee);
    return Results.Created($"/employees/{employee.Id}", employee);
});

app.UseHttpsRedirection();

app.Run();

