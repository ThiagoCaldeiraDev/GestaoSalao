using GestaoSalao.Models.Entity;

namespace GestaoSalao.Models.Intefaces
{
    public interface IAgendamentoServices : IBaseServices<Agendamento>
    {
        string Inserir(Agendamento dados);
    }
}
