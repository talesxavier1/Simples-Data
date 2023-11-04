using Simples_Data.APIs.Pipelines;
using Simples_Data.APIs.TraceListeners;
using SingularChatAPIs.BD;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
MongoDBConnection.start();

/*
private readonly IMongoCollection<AcademicBackgroundModel> collection;

public AcademicBackgroundRepository() {
    IMongoDatabase database = MongoDBConnection.getMongoDatabase();
    collection = database.GetCollection<AcademicBackgroundModel>("AcademicBackground");
}
*/

Trace.Listeners.Add(new LogTraceListener());

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();


var app = builder.Build();


app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

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
