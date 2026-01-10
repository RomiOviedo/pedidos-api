

using Pedidos.Abstraccion;

namespace Pedidos.AplicacionWeb.Configurations
{
    public class TokenParameters :ITokensParameters
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PaswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IList<string>? Roles { get; set; }
    }
}
