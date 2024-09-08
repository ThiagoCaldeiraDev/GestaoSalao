using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces.Data;

namespace GestaoSalao.Services.Services
{
    public class BaseServices<T> where T : BaseEntity
    {
        #region Propriedades e Construtor

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<T> _repository;

        public BaseServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
        }

        #endregion

        #region Consultas

        public IEnumerable<T> ConsultaTodos()
            => _repository.ListarTodos();

        public T ConsultaPorId(int id)
        {
            return _repository.ListarPorId(id);
        }

        public T ConsultaPorId(string guid)
        {
            var guidBuscar = Guid.Parse(guid);
            return _repository.ListarPrimeiroPor(x => x.Guid == guidBuscar);
        }

        #endregion
    }
}
