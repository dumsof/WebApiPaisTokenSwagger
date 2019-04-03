namespace WebApiPais.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="Provincia" />
    /// </summary>
    public class Provincia
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Nombre
        /// </summary>
        [StringLength(30, ErrorMessage = "El nombre de la provincia debe contener menos de 30 caracteres.")]
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the PaisId
        /// propiedad para establecer relacion con la tabla paises
        /// </summary>
        [ForeignKey("Pais")]
        //[Key]
        //[Column(Order = 1)]
        public int PaisId { set; get; }

        /// <summary>
        /// Gets or sets the Pais
        /// cada provincia le corresponde un pais.
        /// </summary>
        [JsonIgnore]
        public Pais Pais { set; get; }
    }
}
