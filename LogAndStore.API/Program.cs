using LogAndStore.Application.DependencyInjection;
using LogAndStore.DAL.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath); 
});

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();