using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using ModelLayer.Validator;
using FluentValidation;
using BusinessLayer.AutoMapper;
using AutoMapper;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using BusinessLayer.Interface;
using BusinessLayer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<AddressBookContext>(options => options.UseSqlServer(connectionString));

// Register AutoMapper

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<Mapping>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IAddressBookRL, AddressBookRL>();
builder.Services.AddScoped<IAddressBookBL, AddressBookBL>();

// Register FluentValidation

builder.Services.AddValidatorsFromAssemblyContaining<AddressBookValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();