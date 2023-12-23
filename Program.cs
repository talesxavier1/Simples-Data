using Microsoft.AspNetCore.OData;
using Simples_Data.APIs.Pipelines;
using Simples_Data.APIs.TraceListeners;
using Simples_Data.APIs_SimplesData_Front.Odata;
using SingularChatAPIs.BD;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
MongoDBConnection.start();

Trace.Listeners.Add(new LogTraceListener());

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

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

app.UseWhen(context => {
    var value = context.Request.Path.Value;
    if (value != null && value.Contains("api/v1/data")) { return true; }
    return false;
}
,branch => {
    branch.UsePipelineValidacaoRequisicao();
});


app.Run();


//=====================================================
/*using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata",IODataConfig.GetEdmModel()).Filter().Select().Expand());


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataRouteDebug();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseAuthorization();

app.MapControllers();

app.Run();
*/