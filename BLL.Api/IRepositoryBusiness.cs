using System;
using System.Collections.Generic;
using DTO.Api;

namespace BLL.Api
{
    public interface IRepositoryBusiness<TEntity>  where TEntity: class
    {
        ICollection<TEntity> GetAll(int? id = null);

        TEntity GetById(int id);

        ICollection<TEntity> Filter(Func<TEntity, bool> arrowFunction, TEntity entity);

        TEntity Post(TEntity entityObject);

        int Put(TEntity entityObject);

        TEntity Delete(int id);

        ICollection<Usuarios> Filter(Func<Usuarios, bool> p);

        TEntity Login(string login, string senha);

        int Logout(int id);
    }
}
