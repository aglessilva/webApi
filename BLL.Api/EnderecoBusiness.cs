using DAL.Api;
using DTO.Api;
using System.Collections.Generic;

namespace BLL.Api
{
    public class EnderecoBusiness
    {

        private ContextOperation contextOperation = new ContextOperation();

        public IEnumerable<Enderecos> GetAll(int? id =null)
        {
            IEnumerable<Enderecos> endereco = contextOperation.getAllEndereco(id);
            return endereco;
        }

        public int UpdateEndereco(Enderecos endereco)
        {
           return contextOperation.updateBairro(endereco);
        }

        public int InsertEndereco(Enderecos endereco)
        {
            return contextOperation.InsertEndereco(endereco);
        }

        public int DeleteEndereco(int id)
        {
            return contextOperation.DeleteEndereco(id);
        }
    }
}
