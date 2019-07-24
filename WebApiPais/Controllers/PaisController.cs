namespace WebApiPais.Controllers
{
    /*en swagger ProducesResponseType remplaza SwaggerResponse, la descripcion se especifica en response*/

    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using WebApiPais.Models;
    using WebApiPais.ValidacionModelo;

    /// <summary>
    /// Defines the <see cref="PaisController" />
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    //definir el esquema de autorizacion, en este caso json web token
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaisController : Controller
    {
        /// <summary>
        /// Defines the context
        /// </summary>
        private readonly AplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaisController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="AplicationDbContext"/></param>
        public PaisController(AplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Obtener todos los paises.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Pais}"/></returns>
        /// <response code="200">Operación realizada con éxito.</response>
        /// <response code="404">No existen datos para la consulta realizada.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpGet]
        [ProducesResponseType(typeof(RespuestaPais), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RespuestaPais), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RespuestaPais), (int)HttpStatusCode.InternalServerError)]
        public IActionResult ObtenerPaises()
        {
            List<Pais> pais = (from P in context.Paises
                               select new Pais
                               {
                                   Id = P.Id,
                                   Nombre = P.Nombre,
                                   Provincias = context.Provincias.Where(c => c.PaisId == P.Id).ToList()
                               }).ToList();

            RespuestaPais respuestaPais = new RespuestaPais { Mensaje = new Mensaje { Identificador = 1, Titulo = "Exito Generico", Contenido = "Éxito Generico" }, Pais = pais };
            return this.StatusCode((int)HttpStatusCode.OK, respuestaPais);
        }

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpGet("{id}", Name = "PaisCreado")]
        public IActionResult ObtenerPaises(int id)
        {
            var pais = context.Paises.Include(p => p.Provincias).FirstOrDefault(c => c.Id == id);
            if (pais == null)
                return NotFound();
            return Ok(pais);
        }

        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="pais">The pais<see cref="Pais"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateModel]
        public IActionResult Post([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Paises.Add(pais);
                context.SaveChanges();
                //redirecciona a la acion GetById con el verbo PaisCreado, pasando como parametro el id del nuevo pais creado.
                return new CreatedAtRouteResult("PaisCreado", new { id = pais.Id }, pais);
            }
            //permite llevar los mensajes de validacion definidos en el modelo
            //hasta la aplicacion cliente cuando trata de guardar un nombre que
            //pasa de los 30 caracteres para el pais           
            return BadRequest(ModelState);
        }

        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="pais">The pais<see cref="Pais"/></param>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpPut("{id}")]
        public IActionResult Post([FromBody] Pais pais, int id)
        {
            if (pais.Id != id)
            {
                return BadRequest();
            }
            context.Entry(pais).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pais = context.Paises.FirstOrDefault(c => c.Id == id);
            if (pais == null)
            {
                return NotFound();
            }
            context.Paises.Remove(pais);
            context.SaveChanges();
            context.Paises.Remove(pais);
            return Ok();
        }
    }
}
