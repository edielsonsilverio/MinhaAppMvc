using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly DbContext Db;
        internal DbSet<TEntity> dbSet;

        protected Repository(DbContext db)
        {
            try
            {
                Db = db;
                this.dbSet = Db.Set<TEntity>();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }
        }

        public async Task<TEntity> ObterPorId(Guid id) => await dbSet.FindAsync(id);

        public async Task<TEntity> ObterPorFiltro(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null, bool isTracking = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> ObterTodos(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = null, bool isTracking = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            if (orderBy != null)
                return orderBy(query);

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
        public async Task Adicionar(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await SaveChanges();
        }

        public async Task AdicionarLista(IEnumerable<TEntity> entity)
        {
            await dbSet.AddRangeAsync(entity);
            await SaveChanges();
        }
        public virtual async Task Atualizar(TEntity entity)
        {
            Db.Update(entity);
            await SaveChanges();
        }
        public async Task<int> SaveChanges() => await Db.SaveChangesAsync();
        public Task<bool> CheckExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (query.Count() > 0)
                return Task.Run(() => true);

            return Task.Run(() => false);
        }

        public async Task Remover(TEntity entity)
        {
            dbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task Remover(Guid id)
        {
            dbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task RemoverLista(IEnumerable<TEntity> entity)
        {
            dbSet.RemoveRange(entity);
            await SaveChanges();
        }

        public void Dispose() => Db?.Dispose();
    }
}