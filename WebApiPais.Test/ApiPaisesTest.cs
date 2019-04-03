using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApiPais.Test
{
    using Moq;
    using WebApiPais;
    using WebApiPais.Controllers;
    using WebApiPais.Models;

    [TestClass]
    public class ApiPaisesTest
    {
        Mock<AplicationDbContext> context;
        /// <summary>
        /// metodo que se inicializa anted de ejecutar cada prueba.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            context = new Mock<AplicationDbContext>() ;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var referencia = context.Setup(sp => sp);
        }
    }
}
