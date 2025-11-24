using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Abstraccion
{
    public interface ITokensParameters
    {       
        public string Id { get; set; }
        public string UserName {  get; set; }
        public string Email { get; set; }
        public string PaswordHash { get; set; }
        IList<string>? Roles { get; set; }

    }
}
