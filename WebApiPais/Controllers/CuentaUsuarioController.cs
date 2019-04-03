namespace WebApiPais.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using WebApiPais.Models;

    /// <summary>
    /// Defines the <see cref="CuentaUsuarioController" />
    /// </summary>
    [Produces("application/json")]
    [Route("api/CuentaUsuario")]
    public class CuentaUsuarioController : Controller
    {
        /// <summary>
        /// Defines the _userManager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Defines the _signInManager
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Defines the _configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CuentaUsuarioController"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{ApplicationUser}"/></param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{ApplicationUser}"/></param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
        public CuentaUsuarioController(UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager,
             IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// The CrearUsuario
        /// </summary>
        /// <param name="model">The model<see cref="InformacionUsuario"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] InformacionUsuario model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Usuario o contraseña invalida, por favor verifique.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// The Logueo
        /// </summary>
        /// <param name="userInfo">The userInfo<see cref="InformacionUsuario"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Route("Logueo")]
        [HttpPost]
        public async Task<IActionResult> Logueo([FromBody] InformacionUsuario userInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, true, false);
                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Logueo invalido, por favor verifique.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// The BuildToken
        /// </summary>
        /// <param name="informacionUsuario">The informacionUsuario<see cref="InformacionUsuario"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>        
        private IActionResult BuildToken(InformacionUsuario informacionUsuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, informacionUsuario.Email),
                new Claim("miValor", "lo que yo quiera"), //se puede pasar cualquier valor 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //valor unico por token, identificador para poder revocar el token si se desea.
            };


            //la clave secrete debe tener una longitud de mas de 128 bit.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["va_clave_super_secreta"]));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                                                           issuer: "yourdomain.com",
                                                           audience: "yourdomain.com",
                                                           claims: claims,
                                                           expires: expiration,
                                                           signingCredentials: credencial
                                    );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiracion = expiration
            });
        }
    }
}
