using GestaoSalao.Models.Entity;

namespace GestaoSalao.Models.Intefaces
{
    public interface IBaseServices<T> where T : class
    {
        IEnumerable<T> ConsultaTodos();
        T ConsultaPorId(int id);
        T ConsultaPorId(string guid);
    }
}
