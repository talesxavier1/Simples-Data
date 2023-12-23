using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Simples_Data.Models;

namespace Simples_Data.APIs_SimplesData_Front.Odata;

public static class OdataEDMConfig {
    public static IEdmModel GetEdmModel(string version) {

        switch (version) {
            case "v1":
                return GetEdmModelV1();
            default:
                return GetEdmModelV1();
        }

    }


    private static IEdmModel GetEdmModelV1() {
        var modelBuilder = new ODataConventionModelBuilder();

        modelBuilder.EntitySet<ProjetoModel>("ProjetoModels");

        return modelBuilder.GetEdmModel();
    }
}