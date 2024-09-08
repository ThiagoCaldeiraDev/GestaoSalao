using GestaoSalao.Models.Entity;

namespace GestaoSalao.Models.Intefaces
{
    public interface IServicoServices : IBaseServices<Servico>
    {
        string Inserir(Servico dados);
        string Atualizar(Servico dados);
        string Excluir(int id);
    }
}
