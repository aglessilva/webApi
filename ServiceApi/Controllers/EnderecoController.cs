using BLL.Api;
using DTO.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ServiceApi.Controllers
{
    public class EnderecoController : ApiController
    {
        private EnderecoBusiness business = new EnderecoBusiness();

        // GET: api/Endereco
        public IEnumerable<Enderecos> GetEndereco(int? id = null)
        {
            return business.GetAll(id);
        }

        // GET: api/Endereco/5
        [ResponseType(typeof(Enderecos))]
        public IEnumerable<Enderecos> GetEndereco(int id)
        {

            if (id == 0)
            {
                return null; ;
            }

            return business.GetAll(id);
        }

        // PUT: api/Endereco/5
        [ResponseType(typeof(void))]
        
        public HttpResponseMessage PutEndereco(Enderecos endereco)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            int ret = business.UpdateEndereco(endereco);
            return ret == 1 ?  Request.CreateResponse(HttpStatusCode.OK, ret) :  Request.CreateResponse(HttpStatusCode.BadRequest); 
        }

        // POST: api/Endereco
        [ResponseType(typeof(Enderecos))]
        public IHttpActionResult PostEndereco(Enderecos endereco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            business.InsertEndereco(endereco);

            return CreatedAtRoute("DefaultApi", new { id = endereco.Id }, endereco);
        }

        //// DELETE: api/Endereco/5
        [ResponseType(typeof(Enderecos))]
        [HttpDelete]
        public HttpResponseMessage DeleteEndereco(int id)
        {
            try
            {
                int ret = business.DeleteEndereco(id);
                return Request.CreateResponse(HttpStatusCode.OK,ret); 
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message); 
            }
        }
    }
}