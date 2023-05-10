public class IngredientsRequest
{
    public string StoreName { get; set; } = string.Empty;
    public List<string> Restrictions { get; set; } = new List<string>();
}