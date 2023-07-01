var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors(setup =>
{
    setup.AddPolicy("CORS", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddMvc().AddJsonOptions(p =>
{
    p.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddControllers();


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

app.UseHttpsRedirection();

app.UseCors("CORS");


app.UseAuthorization();


app.MapControllers();

app.Run();
