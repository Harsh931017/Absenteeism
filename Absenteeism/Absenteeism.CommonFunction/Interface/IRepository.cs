using System.Linq.Expressions;

namespace Absenteeism.CommonFunction.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //TEntity Get(int id);
        //IEnumerable<TEntity> GetAll();
        //IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        //int GetCount();
        //int GetCount(Expression<Func<TEntity, bool>> predicate);

        //void MarkModified(TEntity entity);
        //void Add(TEntity entity);
        //void AddRange(IEnumerable<TEntity> entities);

        //void Delete(int id);
        //void Delete(TEntity entity);
        //void DeleteRange(IEnumerable<TEntity> entities);

        //void Edit(TEntity entity);
        //void EditRange(IEnumerable<TEntity> entities);

        //void Save();

        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<int> GetCount();
        Task<int> GetCount(Expression<Func<TEntity, bool>> predicate);

        Task MarkModified(TEntity entity);
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);

        Task Delete(int id);
        Task Delete(TEntity entity);
        Task DeleteRange(IEnumerable<TEntity> entities);

        Task Edit(TEntity entity);
        Task EditRange(IEnumerable<TEntity> entities);

        Task Save();



        // List<TEntity> GetWithRawSql(string query, params object[] parameters);
    }
}
