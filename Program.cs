using Microsoft.AspNetCore.OData;
using MongoDB.Driver;
using Simples_Data.APIs_SimplesData_Front.Odata;
using Simples_Data.MongoDB;
using SingularChatAPIs.utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();


var mongoDbSettingsModel = AppSettings.appSetting.GetSection("MongoDBSettings").Get<MongoDbSettingsModel>();
if (mongoDbSettingsModel != null) {
    builder.Services.AddSingleton<IMongoClient>((provider) => new MongoClient(mongoDbSettingsModel.ConnectionString));
    builder.Services.AddSingleton<IMongoDatabase>((provider) => provider.GetRequiredService<IMongoClient>().GetDatabase(mongoDbSettingsModel.DatabaseName));
}

builder.Services.AddControllers().AddOData(options => {
    options.Filter();
    options.Select();
    options.Expand();
    options.Count();
    options.OrderBy();
    options.SetMaxTop(null);
    options.AddRouteComponents("odata/v1",OdataEDMConfig.GetEdmModel("v1"));
});

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseAuthorization();
app.UseODataRouteDebug();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllerRoute(name: "default",pattern: "{controller=main}/{action=Index}/{id?}");

app.Run();
