using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using GestaoSalao.Models.Intefaces.Data;

namespace GestaoSalao.Services.Services
{
    public class ProdutoServices : BaseServices<Produto>, IProdutoServices
    {
        #region Propriedades e Construtor


        public ProdutoServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region CRUD

        public string Inserir(Produto dados)
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

        public string Atualizar(Produto dados)
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

        private string Validar(Produto dados, bool insert = true)
        {
            var dadosDataIgual = insert 
                ? _repository.ListarPrimeiroPor(x => x.Nome == dados.Nome) 
                : _repository.ListarPrimeiroPor(x => x.Nome == dados.Nome && x.Id != dados.Id);

            return dadosDataIgual != null 
                ? "Existe um produto com o mesmo nome cadastrado" : string.Empty;
        }

        #endregion
    }
}
