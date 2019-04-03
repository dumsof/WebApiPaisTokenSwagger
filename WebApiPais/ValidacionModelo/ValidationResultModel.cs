namespace WebApiPais.ValidacionModelo
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Defines the <see cref="ValidationResultModel" />
    /// </summary>
    public class ValidationResultModel
    {
        /// <summary>
        /// Mensaje con la definicion del tipo de validacion, en este caso para los campos.
        /// </summary>
        private string TipoMensaje { get; }

        /// <summary>
        /// almacena los datos del nombre del campo y su respectiva validacion que no cumplio.
        /// </summary>
        private List<ValidationError> Errores { get; }

        /// <summary>
        /// DUM: Metodo que permite obtener o capturar las validaciones del models con sus mensajes de validación <see cref="ValidationResultModel"/> class.
        /// </summary>
        /// <param name="modelState">objeto donde se encuentra el nombre de la propiedad y el mensaje de validación<see cref="ModelStateDictionary"/></param>
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            TipoMensaje = "Validación de los Campos.";
            Errores = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}
