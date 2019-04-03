namespace WebApiPais.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// Defines the <see cref="Pais" />
    /// </summary>
    public class Pais
    {
        //para que la lista de provincia no se presente en null, para que muestre un arreglo vacio.
        /// <summary>
        /// Initializes a new instance of the <see cref="Pais"/> class.
        /// </summary>
        public Pais()
        {
            Provincias = new List<Provincia>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Nombre
        /// </summary>
        [Required(ErrorMessage = "El campos {0} es requerido, por favor verifique.")]
        [StringLength(30, ErrorMessage = "El nombre del pais debe contener menos de 30 caracteres.")]
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the Provincias
        /// cada pais le corresponde un listado de provincias, para realizar la 
        /// relación con la tabla de provincias.
        /// </summary>
        public List<Provincia> Provincias { set; get; }
    }
}
