using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevIO.Business.Intefaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> ObterPorId(Guid id);
        Task<IEnumerable<TEntity>> ObterTodos(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            bool isTracking = false
        );
        Task<TEntity> ObterPorFiltro(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            bool isTracking = false
        );
        Task Adicionar(TEntity entity);
        Task AdicionarLista(IEnumerable<TEntity> entity);
        Task Atualizar(TEntity entity);
        Task<int> SaveChanges();
        Task<bool> CheckExistAsync(Expression<Func<TEntity, bool>> filter);
        Task Remover(TEntity entity);
        Task Remover(Guid id);
        Task RemoverLista(IEnumerable<TEntity> entity);
    }
}