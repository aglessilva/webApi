using DTO.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public class ContextOperation
    {
        private DataContexto db = new DataContexto();

        public IEnumerable<Enderecos> getAllEndereco(int? id)
        {
            IEnumerable<Enderecos> enderecos = null;

            if (id == null)
                enderecos = db.Enderecos.ToList();
            else
                enderecos = db.Enderecos.ToList().FindAll(x => x.Id == id);

            return enderecos;
        }

        public Enderecos GetEndereco(int id)
        {
            Enderecos endereco = db.Enderecos.Find(id);
            if (endereco == null)
            {
                return null;
            }

            return endereco;
        }


        public int updateBairro(Enderecos endereco)
        {
            int retorno = 0;
            try
            {
                db.Entry(endereco).State = EntityState.Modified;
                retorno = db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                    throw;   
            }

            return retorno;
        }

        public int InsertEndereco(Enderecos endereco)
        {
            try
            {
                db.Enderecos.Add(endereco);
                return db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteEndereco(int id)
        {
            Enderecos endereco = db.Enderecos.Find(id);
            try
            {
                db.Enderecos.Remove(endereco);
                return db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
