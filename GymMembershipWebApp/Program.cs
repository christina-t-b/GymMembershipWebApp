using GymMembershipWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ReportService>();


builder.Services.AddSingleton<MemberManager>();
builder.Services.AddSingleton<AttendanceService>();
builder.Services.AddSingleton<ReportService>();


var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Member}/{action=Index}/{id?}"
);

app.Run();