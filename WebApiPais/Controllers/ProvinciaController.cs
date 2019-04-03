namespace WebApiPais.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using WebApiPais.Models;
    using WebApiPais.ValidacionModelo;

    /// <summary>
    /// Defines the <see cref="ProvinciaController" />
    /// </summary>
    [Produces("application/json")]
    //se agrega a la regla de ruteo el pais y luego la provincia para obtener la informacion por
    //pais y luego las provincias de ese pais
    [Route("api/Provincia")]
    public class ProvinciaController : Controller
    {
        /// <summary>
        /// Defines the context
        /// </summary>
        private readonly AplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProvinciaController"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="AplicationDbContext"/></param>
        public ProvinciaController(AplicationDbContext context)
        {
            this.context = context;
        }

        //http://localhost:63146/api/pais/2/provincia
        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="PaisId">The PaisId<see cref="int?"/></param>
        /// <returns>The <see cref="IEnumerable{Provincia}"/></returns>
        [HttpGet]
        public IEnumerable<Provincia> Get(int? PaisId)
        {
            return context.Provincias.Where(c => c.PaisId == PaisId).ToList();
        }

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpGet("{id}", Name = "ProvinciaCreada")]
        public IActionResult GetById(int id)
        {
            var pais = context.Provincias.FirstOrDefault(c => c.Id == id);
            if (pais == null)
                return NotFound();
            return Ok(pais);
        }

        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="provincia">The provincia<see cref="Provincia"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateModel] //DUM:permite validar por medio de un filtro, las propiedades del modelo.
        public IActionResult Post([FromBody] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                context.Provincias.Add(provincia);
                context.SaveChanges();
                //redirecciona a la acion GetById con el verbo PaisCreado, pasando como parametro el id del nuevo pais creado.
                return new CreatedAtRouteResult("ProvinciaCreada", new { id = provincia.Id }, provincia);
            }
            //permite llevar los mensajes de validacion definidos en el modelo
            //hasta la aplicacion cliente cuando trata de guardar un nombre que
            //pasa de los 30 caracteres para el pais           
            return BadRequest(ModelState);
        }

        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="provincia">The provincia<see cref="Provincia"/></param>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpPost("{id}")]
        public IActionResult Post([FromBody] Provincia provincia, int id)
        {
            if (provincia.Id != id)
            {
                return BadRequest();
            }
            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        //[HttpPut("{id}")]
        //[Route("Delete")]
        //public IActionResult Delete(int id)
        //{
        //    var provincia = context.Provincias.FirstOrDefault(c => c.Id == id);
        //    if (provincia == null)
        //    {
        //        return NotFound();
        //    }
        //    context.Provincias.Remove(provincia);
        //    context.SaveChanges();
        //    context.Provincias.Remove(provincia);
        //    return Ok();
        //}
    }
}
