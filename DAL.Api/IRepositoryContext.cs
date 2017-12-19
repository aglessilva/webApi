using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IRepositoryContext<TEntity>: IDisposable where TEntity : class
    {
        ICollection<TEntity> GetAll();

        ICollection<TEntity> Filter(Func<TEntity, bool> arrowFunction);

        TEntity GetById(int id);

        TEntity Post(TEntity entityObject);

        int Put(TEntity entityObject);

        TEntity Delete(Func<TEntity, bool> predicate);

        int Login(string login, string senha);

        int Logout(int id);
    }
}
