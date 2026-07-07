namespace apisafeguardpro.Models;

public class UserToken
{
    public string Token { get; set; } = null!;

    public DateTime Expiration { get; set; }

    public IList<string> Roles { get; set; } = new List<string>();

    public int IdCol { get; set; }
}
