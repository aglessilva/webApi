
using DAL.Api;
using DTO.Api;
using System;
using System.Collections.Generic;

namespace BLL.Api
{
    public class UsuariosBusiness : IRepositoryBusiness<Usuarios>
    {
       
        private IRepositoryContext<Usuarios> repositoryContext = null;
        private ICollection<Usuarios> list = null;


        Usuarios IRepositoryBusiness<Usuarios>.Delete(int id)
        {
            repositoryContext = new DataContract<Usuarios>();
            return repositoryContext.Delete(x => x.IdUsuario == id);
        }

        ICollection<Usuarios> IRepositoryBusiness<Usuarios>.Filter(Func<Usuarios, bool> arrowFunction, Usuarios entity)
        {
            repositoryContext = new DataContract<Usuarios>();
            try
            {
                list = repositoryContext.Filter(arrowFunction);
            }               
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        ICollection<Usuarios> IRepositoryBusiness<Usuarios>.Filter(Func<Usuarios, bool> p)
        {
            repositoryContext = new DataContract<Usuarios>();
            return repositoryContext.Filter(p);
        }

        ICollection<Usuarios> IRepositoryBusiness<Usuarios>.GetAll(int? id)
        {
            repositoryContext = new DataContract<Usuarios>();
            try
            {
                if (id == null)
                    list = repositoryContext.GetAll();
                else
                    list = repositoryContext.Filter(x => x.IdUsuario == (int)id) ;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return list;
        }

        Usuarios IRepositoryBusiness<Usuarios>.GetById(int id)
        {
            repositoryContext = new DataContract<Usuarios>();
            return repositoryContext.GetById(id);
        }


        Usuarios IRepositoryBusiness<Usuarios>.Post(Usuarios entityObject)
        {
            repositoryContext = new DataContract<Usuarios>();
            return repositoryContext.Post(entityObject);
        }

        int IRepositoryBusiness<Usuarios>.Put(Usuarios entityObject)
        {
            repositoryContext = new DataContract<Usuarios>();
            return repositoryContext.Put(entityObject);
        }

        Usuarios IRepositoryBusiness<Usuarios>.Login(string login, string senha)
        {
            Usuarios usuario = null;

            repositoryContext = new DataContract<Usuarios>();
            int retorno = repositoryContext.Login(login, senha);

            if (retorno > 0)
            {
                repositoryContext = new DataContract<Usuarios>();
                usuario = repositoryContext.GetById(retorno);

                if (!usuario.IsAuthentication)
                {
                    usuario.IsAuthentication = true;
                    int x = repositoryContext.Put(usuario);

                    if (x == 0)
                    {
                        return null;
                    }
                }
                else
                {
                    usuario.IdUsuario = 0;
                    return usuario;
                }

            }

            return usuario;
        }

        int IRepositoryBusiness<Usuarios>.Logout(int id)
        {
            return 0;
        }
    }
}
