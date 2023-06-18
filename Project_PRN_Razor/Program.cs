using Domain.IRepositories;
using Domain.Mapping;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped(typeof(ISemesterRepository), typeof(SemesterRepository));
builder.Services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
builder.Services.AddScoped(typeof(IClassRepository), typeof(ClassRepository));
builder.Services.AddScoped(typeof(IEnrollmentRepository), typeof(EnrollmentRepository));
builder.Services.AddScoped(typeof(IMajorRepository), typeof(MajorRepository));
builder.Services.AddScoped(typeof(IMarkRepository), typeof(MarkRepository));
builder.Services.AddScoped(typeof(IMarkReportRepository), typeof(MarkReportRepository));
builder.Services.AddScoped(typeof(IStudentDetailRepository), typeof(StudentDetailRepository));
builder.Services.AddScoped(typeof(ISubjectRepository), typeof(SubjectRepository));
builder.Services.AddScoped(typeof(ISubjectMarkRepository), typeof(SubjectMarkRepository));
builder.Services.AddScoped(typeof(ITeacherDetailRepository), typeof(TeacherDetailRepository));

builder.Services.AddDbContext<ProjectPrn221Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Project_PRN")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();