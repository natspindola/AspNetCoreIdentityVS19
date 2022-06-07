using System;

namespace AspNetCoreIdentityVS19.Controllers
{
    internal class ClaimAuthorizeAttribute : Attribute
    {
        public ClaimAuthorizeAttribute(string v1, string v2)
        {
        }
    }
}