namespace Simples_Data.Models.ViewModel;
public class ProjetoViewModel {
    public IEnumerable<ProjetoModel> projetoModels { get; set; }
    public long count { get; set; }
    public long skip { get; set; }
    public long take { get; set; }
}
