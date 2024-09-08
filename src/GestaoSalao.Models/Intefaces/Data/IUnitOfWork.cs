namespace GestaoSalao.Models.Intefaces.Data
{
    public interface IUnitOfWork
    {
        #region Repositories
        IRepository<T> GetRepository<T>() where T : class;

        #endregion

        void SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        void FinalizeTransaction();
    }
}
