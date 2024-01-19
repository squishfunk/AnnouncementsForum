using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AnnouncementsForum;
using AnnouncementsForum.Models;
using AnnouncementsForum.Services;
using AnnouncementsForum.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//custom services
builder.Services.AddScoped<IAnnoucmentsService, AnnoucmentsService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IForbiddenWordsService, ForbbidenWordsService>();
builder.Services.AddScoped<IReportedAnnoucment, ReportedAnnoucmentService>();
builder.Services.AddScoped<IHtmlTagsService, HtmlTagsService>();
builder.Services.AddScoped<IAdminAnnoucmentsService, AdminAnnoucmentsService>();
//Database and Identity config
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 2;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// seed role on new database
using(var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin" };

    foreach(var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    var htmlTags = scope.ServiceProvider.GetRequiredService<IHtmlTagsService>();
    var context = scope.ServiceProvider.GetRequiredService<DBContext>();
    var allTags = new[]{"a","abbr","acronym","address","applet","area","article","aside","audio","b","base","basefont","bdi","bdo","bgsound","big","blink","blockquote","body","br","button","canvas","caption","center","cite","code","col","colgroup","content","data","datalist","dd","decorator","del","details","dfn","dir","div","dl","dt","element","em","embed","fieldset","figcaption","figure","font","footer","form","frame","frameset","h1","h2","h3","h4","h5","h6","head","header","hgroup","hr","html","i","iframe","img","input","ins","isindex","kbd","keygen","label","legend","li","link","listing","main","map","mark","marquee","menu","menuitem","meta","meter","nav","nobr","noframes","noscript","object","ol","optgroup","option","output","p","param","plaintext","pre","progress","q","rp","rt","ruby","s","samp","script","section","select","shadow","small","source","spacer","span","strike","strong","style","sub","summary","sup","table","tbody","td","template","textarea","tfoot","th","thead","time","title","tr","track","tt","u","ul","var","video","wbr","xmp"};
    if(htmlTags.GetAll().Count() != allTags.Count())
    {
        foreach(var tag in allTags)
        {
            var tagToAdd = new HtmlTags();
            tagToAdd.IsAllowed = false;
            tagToAdd.Name = tag;
            context.HtmlTags.Add(tagToAdd);
        }
        await context.SaveChangesAsync();
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();
    string userName = "admin";
    string email = "admin@admin.com";
    string password = "admin";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new UserModel();
        user.Email = email;
        user.UserName = userName;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
