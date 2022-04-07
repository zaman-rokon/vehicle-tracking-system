using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using VehicleTrackingSystem.Application.ResponseModels;
using VehicleTrackingSystem.DataAccess.DbContext;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.Application.Managers
{
    public class VehicleTrackingUserManager : IVehicleTrackingUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public VehicleTrackingUserManager(UserManager<ApplicationUser> userManager
            , IConfiguration configuration
            , ApplicationDbContext applicationDbContext
            , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
        }


        public async Task<TokenResponse> GetUserToken(string email, string password)
        {
            TokenResponse tokenResponse = null;
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null && await _userManager.CheckPasswordAsync(user, password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var vehicle = _applicationDbContext.Vehicles.Where(it => it.ApplicationUser.Id == user.Id)
                        ?.FirstOrDefault();

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(VehicleTrackingSystemConstant.AUTHORIZATION_CLAIM_USERID, user.Id.ToString()),
                        new Claim(VehicleTrackingSystemConstant.AUTHORIZATION_CLAIM_VEHICLE, vehicle.Id)
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return tokenResponse = new TokenResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Validity = token.ValidTo
                    };
                }
            }
            catch (Exception ex)
            {

            }

            return tokenResponse;
        }

        public async Task<bool> RegisterUser(string email, string password, string firstName, string lastName, string vehicleRegistrationNumber, string vehicleModelName)
        {
            bool respnse = false;

            var userExists = await _userManager.FindByEmailAsync(email);
            if (userExists != null)
            {
                respnse = false;
            }
            else
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email,
                    LastName = lastName,
                    FirstName = firstName,
                    Vehicles = new List<Vehicle>
                    {
                        new Vehicle()
                        {
                            Id = Guid.NewGuid().ToString(),
                            RegistrationNumber = vehicleRegistrationNumber,
                            Model = vehicleModelName
                        }
                    }
                };
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    respnse = false;
                }
                else
                {
                    result = await _userManager.AddToRoleAsync(user, ApplicationRole.ApplicationUser.ToString());
                    if (result.Succeeded)
                    {
                        respnse = true;
                    }
                }
            }

            return respnse;
        }

        public async Task<bool> AssignUserRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return false;
            await _userManager.AddToRoleAsync(user, roleName);

            return true;
        }

        public async Task<bool> RemoveUserRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return false;
            await _userManager.RemoveFromRoleAsync(user, roleName);

            return true;
        }
    }
}
