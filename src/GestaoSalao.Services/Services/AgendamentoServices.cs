using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using GestaoSalao.Models.Intefaces.Data;

namespace GestaoSalao.Services.Services
{
    public class AgendamentoServices : BaseServices<Agendamento>, IAgendamentoServices
    {
        #region Propriedades e Construtor


        public AgendamentoServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region CRUD

        public string Inserir(Agendamento dados)
        {
            var validacao = Validar(dados);
            if (!string.IsNullOrEmpty(validacao))
            {
                return validacao;
            }

            try
            {
                // Insere o cenário primeiro
                _unitOfWork.BeginTransaction();

                _repository.Inserir(dados);

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                return $"Erro ao inserir, segue a exceção: {ex.Message}";
            }

            return string.Empty;
        }

        private string Validar(Agendamento dados, bool insert = true)
        {
            return string.Empty;
        }

        #endregion
    }
}
