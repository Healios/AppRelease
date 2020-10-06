namespace GraphQL.Models.Inputs
{
    public class UpdateAppReleaseInput
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Version { get; set; } = "";
    }
}
