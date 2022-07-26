using KanBanApi.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

const string ANY_ORIGIN = "ANY_ORIGIN";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ANY_ORIGIN,
                      builder =>
                      {
                          builder.AllowAnyOrigin();
                      });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ImplementApi();

builder.WebHost.ConfigureAppConfiguration(config => config.SetConfiguration().Build());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(ANY_ORIGIN);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
