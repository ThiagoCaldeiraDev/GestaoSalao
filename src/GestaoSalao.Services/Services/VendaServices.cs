using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using GestaoSalao.Models.Intefaces.Data;

namespace GestaoSalao.Services.Services
{
    public class VendaServices : BaseServices<Venda>, IVendaServices
    {
        #region Propriedades e Construtor


        public VendaServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region CRUD

        public string Inserir(Venda dados)
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

        public string Atualizar(Venda dados)
        {
            var validacao = Validar(dados, false);
            if (!string.IsNullOrEmpty(validacao))
            {
                return validacao;
            }

            try
            {
               
                // Atualiza
                _unitOfWork.BeginTransaction();

                _repository.Atualizar(dados);

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar, segue a exceção: {ex.Message}";
            }

            return string.Empty;
        }

        public string Excluir(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                _repository.Remove(id);

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                return $"Erro ao excluir, segue a exceção: {ex.Message}";
            }

            return string.Empty;
        }

        private string Validar(Venda dados, bool insert = true)
        {
            return string.Empty;
        }

        #endregion
    }
}
