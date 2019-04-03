namespace WebApiPais.ValidacionModelo
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="ValidationError" />
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Gets the Field
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NombreCampo { get; }

        /// <summary>
        /// Gets the Message
        /// </summary>
        public string MensajeValidacion { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="field">The field<see cref="string"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        public ValidationError(string field, string message)
        {
            NombreCampo = field != string.Empty ? field : null;
            MensajeValidacion = message;
        }
    }
}
