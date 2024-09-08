using GestaoSalao.Models.Entity;

namespace GestaoSalao.Models.Intefaces
{
    public interface IProdutoServices : IBaseServices<Produto>
    {
        string Inserir(Produto dados);
        string Atualizar(Produto dados);
        string Excluir(int id);
    }
}
