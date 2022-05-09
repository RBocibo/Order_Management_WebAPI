﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Contracts.Data.Entities;
using OrderManagement.Core.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace OrderManagement.Controllers
{
    // <summary>
    /// Provides operations to manage orders
    /// </summary>
    [ApiController]
    [SwaggerTag("Provides operations to manage the current identity")]
    [Produces("application/json")]
    [Route("[controller]")]
    [Authorize]
    public class IdentityController : ControllerBase
    {

        /// <summary>
        /// Retrieves the details of the current identity 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Identity Get()
        {
            var identity = (ClaimsIdentity)User.Identity;

            string user = identity.FindFirst(JwtClaimNames.Sub)?.Value;

            string username = identity.FindFirst(JwtClaimNames.Email)?.Value;
            string email = identity.FindFirst(JwtClaimNames.Email)?.Value;

            var roles = (from c in identity.FindAll(JwtClaimNames.Role)
                         select c.Value).ToList();

            var name = identity.FindFirst(JwtClaimNames.Name)?.Value;
            string surname = identity.FindFirst(JwtClaimNames.Surname)?.Value;

            return new Identity
            {
                UserId = user,
                Surname = surname,
                Name = name,
                Email = email,
                Roles = roles,
                Username = username,
                TelephoneNumber = "",
                UserType = roles.FirstOrDefault()
            };
        }
    }
}


