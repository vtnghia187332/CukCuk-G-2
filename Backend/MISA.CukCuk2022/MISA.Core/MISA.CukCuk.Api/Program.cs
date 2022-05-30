using MISA.Core.Interfaces;
using MISA.Core.Interfaces.IRepo;
using MISA.Core.Lib;
using MISA.Core.Services;
using MISA.Infrastructure.Repositories;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình DI (Dependency Injection)
builder.Services.AddScoped<IUnitRepository,UnitRepository >();
builder.Services.AddScoped<IUnitService, UnitService>();

builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddScoped<IConvertionRepository, ConvertionRepository>();
builder.Services.AddScoped<IConvertionService, ConvertionService>();


builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IEPPLusAppService, EPPLusAppService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();


builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


// Thêm Policy của CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

});
// Thêm Newtonsoft JSON để sửa lỗi trả về của API
builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
