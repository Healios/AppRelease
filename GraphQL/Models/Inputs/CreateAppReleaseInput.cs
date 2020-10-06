namespace GraphQL.Models.Inputs
{
    public class CreateAppReleaseInput
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Version { get; set; } = "";
        public byte[] APKFile { get; set; } = new byte[0];
    }
}
