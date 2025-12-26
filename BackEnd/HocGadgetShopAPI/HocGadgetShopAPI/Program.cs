
//thêm vào
var MyAllowSecificOrigins = "_MyAllowSecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//đổi địa chỉ sang 4200 cho trùng với angular visual studio code
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost", "http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ủy quyền/ gọi chức năng
app.UseCors(MyAllowSecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
