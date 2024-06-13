using Repositories.Implement;
using Repositories.Interface;
using Services.Implement;
using Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

// Add repositories to the container.
builder.Services.AddScoped<INewsArticleRepo, NewsArticleRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ISystemAccountRepo, SystemAccountRepo>();
builder.Services.AddScoped<ITagRepo, TagRepo>();

// Add services to the container.
builder.Services.AddScoped<INewsArticleSvc, NewsArticleSvc>();
builder.Services.AddScoped<ICategorySvc, CategorySvc>();
builder.Services.AddScoped<ISystemAccountSvc, SystemAccountSvc>();
builder.Services.AddScoped<ITagSvc, TagSvc>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
