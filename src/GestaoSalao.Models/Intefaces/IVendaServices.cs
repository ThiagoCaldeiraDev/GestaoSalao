using GestaoSalao.Models.Entity;

namespace GestaoSalao.Models.Intefaces
{
    public interface IVendaServices : IBaseServices<Venda>
    {
        string Inserir(Venda dados);
        string Atualizar(Venda dados);
        string Excluir(int id);
    }
}
