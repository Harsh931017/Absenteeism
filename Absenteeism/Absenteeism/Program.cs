using Absenteeism.CommonFunction;
using Absenteeism.Service.Interface;
using Absenteeism.Service;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Absenteeism.Repository.Interface;
using Absenteeism.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

#region AppSettings
builder.Services.Configure<AppSettings>
      (options => builder.Configuration.GetSection("AppSettings").Bind(options));

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Swagger Version

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                  }
              },
              Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});



#endregion

#region JWTAuthentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AppSettings:Jwt:Issuer"],
        ValidAudience = builder.Configuration["AppSettings:Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Jwt:Key"]))
    };
});
#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(Program));
#endregion

#region Repository
builder.Services.AddTransient<IMasterRepository, MasterRepository>();
builder.Services.AddTransient<IDashboardRepository, DashboardRepository>();

#endregion

#region Service
builder.Services.AddScoped<MasterService>();
builder.Services.AddTransient<IMasterService, MasterService>();
builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<IDashboardService, DashboardService>();


builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformationService>();
#endregion

var app = builder.Build();

app.UseAuthentication();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
{
    options.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
});

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();