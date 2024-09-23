using Absenteeism.CommonFunction.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Absenteeism.CommonFunction.Implementations
{
    public abstract class RepositoryBase<TEntity, TContext> :
        IRepository<TEntity> where TEntity : class
                                where TContext : DbContext, new()
    {

        private TContext _context;// = new TContext();
        public TContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        // not needed, just keeping it, will delete later
        protected DbSet<TEntity> _DbSet { get; set; }

        public RepositoryBase()
        {
        }

        public RepositoryBase(TContext context)
        {
            _context = context;
            _DbSet = _context.Set<TEntity>();
        }

        #region delete later
        //public List<TEntity> GetAll()
        //{
        //    if (_DbSet.Count() > 0)
        //        return _DbSet.ToList();

        //    return new List<TEntity>();
        //} 
        #endregion



        #region Save

        //public IEnumerable<TEntity> GetAll()
        //{
        //    IEnumerable<TEntity> query = _context.Set<TEntity>();
        //    return query;
        //}

        //public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        //{
        //    IEnumerable<TEntity> query = _context.Set<TEntity>().Where(predicate);
        //    return query;
        //}

        //public TEntity Get(int id)
        //{
        //    return _DbSet.Find(id);
        //}

        ////3. Mark entity as modified
        //public void MarkModified(TEntity entity)
        //{
        //    _context.Entry<TEntity>(entity).State = EntityState.Modified;
        //}

        //public void Add(TEntity entity)
        //{
        //    _context.Set<TEntity>().Add(entity);
        //    //_DbSet.Add(entity);
        //}

        //// Update/Save Entity in database
        //public void Edit(TEntity entity)
        //{
        //    _context.Entry<TEntity>(entity).State = EntityState.Modified;
        //}

        //public void Delete(int id)
        //{
        //    var entity = _context.Set<TEntity>().Find(id);
        //    if (entity != null)
        //        _context.Set<TEntity>().Remove(entity);

        //    //_DbSet.Remove(entity);
        //}

        //public void Delete(TEntity entity)
        //{
        //    _context.Set<TEntity>().Remove(entity);
        //    //_DbSet.Remove(entity);
        //}

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        //public void AddRange(IEnumerable<TEntity> entities)
        //{
        //    _context.Set<TEntity>().AddRange(entities);
        //}

        //public void DeleteRange(IEnumerable<TEntity> entities)
        //{
        //    _context.Set<TEntity>().RemoveRange(entities);
        //}

        //public void EditRange(IEnumerable<TEntity> entities)
        //{
        //    foreach (TEntity entity in entities)
        //        _context.Entry<TEntity>(entity).State = EntityState.Modified;
        //}

        //public int GetCount()
        //{
        //    return _DbSet.Count();
        //}

        //public int GetCount(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return _DbSet.Count(predicate);
        //} 
        #endregion
        #region delete later

        //public virtual List<TEntity> GetWithRawSql(string query, params object[] parameters)
        //{
        //    return _DbSet.SqlQuery(query, parameters).ToList();
        //} 
        #endregion



        public async Task<IEnumerable<TEntity>> GetAll()
        {
            IEnumerable<TEntity> query = _context.Set<TEntity>();
            return query;
        }

        public async Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> query = _context.Set<TEntity>().Where(predicate);
            return query;
        }

        public async Task<TEntity> Get(int id)
        {
            return await _DbSet.FindAsync(id);
        }

        //3. Mark entity as modified
        public async Task MarkModified(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public async Task Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            //_DbSet.Add(entity);
        }

        // Update/Save Entity in database
        public async Task Edit(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
                _context.Set<TEntity>().Remove(entity);

            //_DbSet.Remove(entity);
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            //_DbSet.Remove(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task EditRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public async Task<int> GetCount()
        {
            return await _DbSet.CountAsync();
        }

        public async Task<int> GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return await _DbSet.CountAsync(predicate);
        }
    }
}
