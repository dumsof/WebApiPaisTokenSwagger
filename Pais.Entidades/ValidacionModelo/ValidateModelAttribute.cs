namespace Pais.Entidades.ValidacionModelo
{
    using Microsoft.AspNetCore.Mvc.Filters;
    ///estos paquetes se necesitan para poder correr el filtro de validacion.
    ///NOTA DUM: se realizan la instalación de los paquetes nuget 1: Install-Package Microsoft.AspNetCore -Version 2.2.0
    ///NOTA DUM: se realizan la instalación de los paquetes nuget 2: Install-Package Microsoft.AspNetCore.Mvc -Version 2.2.0

    /// <summary>
    /// Defines the <see cref="ValidateModelAttribute" />
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The OnActionExecuting
        /// </summary>
        /// <param name="context">The context<see cref="ActionExecutingContext"/></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}
