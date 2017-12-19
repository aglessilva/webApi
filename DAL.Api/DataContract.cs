using DTO.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Api
{
    public class DataContract<TEntity> : IRepositoryContext<TEntity> where TEntity : class
    {
        private DataContexto ctx = null;
        private TEntity entity = null;
        private ICollection<TEntity> listEntity = null;

        public DataContract()
        {
            ctx = new DataContexto();
        }

        TEntity IRepositoryContext<TEntity>.Delete(Func<TEntity, bool> predicate)
        {
            using (ctx = new DataContexto())
            {
                ctx.Set<TEntity>().Where(predicate).ToList().ForEach(d => { entity = ctx.Set<TEntity>().Remove(d); });
                ctx.SaveChanges();
                return entity;
            }
        }

        ICollection<TEntity> IRepositoryContext<TEntity>.Filter(Func<TEntity, bool> arrowFunction)
        {
            try
            {
                listEntity = ctx.Set<TEntity>().Where(arrowFunction).ToList();
            }
            catch (Exception ex)
            {
                Dispose(true);
            }

            return listEntity;
        }

        ICollection<TEntity> IRepositoryContext<TEntity>.GetAll()
        {
            try
            {
                listEntity = ctx.Set<TEntity>().ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return listEntity;
        }

        TEntity IRepositoryContext<TEntity>.GetById(int id)
        {
            try
            {
                entity = ctx.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }

        TEntity IRepositoryContext<TEntity>.Post(TEntity entityObject)
        {
            try
            {
                entity = ctx.Set<TEntity>().Add(entityObject);
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        int IRepositoryContext<TEntity>.Put(TEntity entityObject)
        {
            ctx.Entry(entityObject).State = EntityState.Modified;
            return ctx.SaveChanges();
        }

        int IRepositoryContext<TEntity>.Login(string login, string senha)
        {
            var obj = ctx.Usuarios.SingleOrDefault(x => x.Login == login && x.Senha == senha);

            if (obj == null)
                return 0;
            return obj.IdUsuario;

        }

        int IRepositoryContext<TEntity>.Logout(int id)
        {
            return 0;
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar chamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }

                // TODO: liberar recursos não gerenciados (objetos não gerenciados) e substituir um finalizador abaixo.
                // TODO: definir campos grandes como nulos.

                disposedValue = true;
            }
        }

        // TODO: substituir um finalizador somente se Dispose(bool disposing) acima tiver o código para liberar recursos não gerenciados.
        ~DataContract()
        {
            // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
            Dispose(false);
        }

        // Código adicionado para implementar corretamente o padrão descartável.
        void IDisposable.Dispose()
        {
            // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
            Dispose(true);
            // TODO: remover marca de comentário da linha a seguir se o finalizador for substituído acima.
             GC.SuppressFinalize(this);
        }


        #endregion
    }
}
