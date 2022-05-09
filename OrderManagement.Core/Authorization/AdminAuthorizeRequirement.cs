using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Authorization
{
    public class AdminAuthorizeRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Gets or sets the role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Initialises the constructor
        /// </summary>
        public AdminAuthorizeRequirement(string role)
        {
            Role = role;
        }
    }
}
