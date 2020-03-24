using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoWeb.Services
{
    public interface IGenericRest<TEntity>
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<TEntity> Create(TEntity entity);

        Task Update(int id, TEntity entity);

        Task Delete(int id);
    }
}