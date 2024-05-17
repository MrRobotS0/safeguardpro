public class UserToken {
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
    public IList<string> Roles { get; set; }
}