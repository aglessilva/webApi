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
    public class UsuariosController : ApiController
    {
        private IRepositoryBusiness<Usuarios> business = new UsuariosBusiness();
        private ICollection<Usuarios> list = null;

        // GET: api/Usuarios
        public HttpResponseMessage GetUsuarios()
        {
            list = business.GetAll();
            return Request.CreateResponse<ICollection<Usuarios>>(HttpStatusCode.OK, list);
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        public HttpResponseMessage GetUsuarios(int id)
        {
            Usuarios user = business.GetById(id);
            return Request.CreateResponse<Usuarios>(HttpStatusCode.OK, user);
        }

        [Route("api/Usuarios/filter")]
        [ResponseType(typeof(Usuarios))]
        [HttpPost]
        public HttpResponseMessage FilterUsuario(Usuarios usuarios)
        {
            list = business.Filter(x => x.Nome.StartsWith(usuarios.Nome.Trim(), StringComparison.CurrentCultureIgnoreCase) 
                                    || x.Sexo.Equals(usuarios.Sexo.Trim())
                                    || x.Email.Equals(usuarios.Email.Trim())
                                    || x.DataNascimento.Value.Equals(usuarios.DataNascimento));

            return Request.CreateResponse<ICollection<Usuarios>>(HttpStatusCode.OK, list);
        }

        //// PUT: api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        public HttpResponseMessage PutUsuarios(Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.PaymentRequired);
            }

            if (usuarios.IdUsuario == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var result = business.Put(usuarios);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //// POST: api/Usuarios
        [ResponseType(typeof(Usuarios))]
        public HttpResponseMessage PostUsuarios(Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (usuarios.Sexo == "true")
                usuarios.Sexo = "F";
            else
                usuarios.Sexo = "M";
            var result = business.Post(usuarios);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //// DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        [HttpDelete]
        public HttpResponseMessage DeleteUsuarios(int id)
        {
            if (id == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro:  codigo de usuario" + id + "não localizado");
            }

            var result = business.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [Route("api/Usuarios/Login")]
        [ResponseType(typeof(Usuarios))]
        [HttpPost]
        public HttpResponseMessage UserLogin(Usuarios  usuario)
        {
            Usuarios user = business.Login(usuario.Login, usuario.Senha);

            if (null == user)
            {
                return Request.CreateResponse<Usuarios>(HttpStatusCode.NotFound, user);
            }

            if (user.IdUsuario == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "este usuario ja esta logado em outra sessao");
            }
            return Request.CreateResponse<Usuarios>(HttpStatusCode.OK,user);
        }

        [Route("api/Usuarios/Logout")]
        [ResponseType(typeof(Usuarios))]
        [HttpPut]
        public HttpResponseMessage UserLogout (Usuarios usuarios)
        {
            HttpResponseMessage user = GetUsuarios((int)usuarios.IdUsuario);
            Usuarios userLogout = (Usuarios)((ObjectContent)user.Content).Value;

            usuarios.IdUsuario = userLogout.IdUsuario;
            usuarios.Nome = userLogout.Nome.Trim();
            usuarios.Documento = userLogout.Documento.Trim();
            usuarios.DataNascimento = userLogout.DataNascimento;
            usuarios.Sexo = userLogout.Sexo.Trim();
            usuarios.Email = userLogout.Email.Trim();
            usuarios.Login = userLogout.Login.Trim();
            usuarios.Senha = userLogout.Senha.Trim();
            usuarios.IsAuthentication = false;

            HttpResponseMessage ret = PutUsuarios(usuarios);
            if(ret.StatusCode == HttpStatusCode.OK)
                return Request.CreateResponse<int>(HttpStatusCode.OK,1);
             else
                return Request.CreateResponse<int>(HttpStatusCode.NotModified, 0);
        }

     

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool UsuariosExists(int id)
        //{
        //    return db.Usuarios.Count(e => e.IdUsuario == id) > 0;
        //}
    }
}