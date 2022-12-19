using Octokit;

namespace Tagger
{
    partial class Program
    {
        public static string GetEnvironmentVariable(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName) ?? throw new ArgumentNullException(variableName, "Environment variable not found.");
        }

        static void Main(string[] args)
        {
            // Get values from the environment variables
            string _outputFile = GetEnvironmentVariable("GITHUB_OUTPUT");
            string _owner = GetEnvironmentVariable("_owner");
            string _repo = GetEnvironmentVariable("_repo");
            string _token = GetEnvironmentVariable("_token");
            string _tag = GetEnvironmentVariable("_tag");
            string _refsha = GetEnvironmentVariable("_refsha");
            string _tag_message = GetEnvironmentVariable("_tag_message");
            string _tag_author = GetEnvironmentVariable("_tag_author");
            string _tag_author_email = GetEnvironmentVariable("_tag_author_email");

            // Create a github client to interact with repositories
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Example"));
            client.Credentials = new Credentials(_token);

            // Get a repository
            Repository repo = Task.Run(() => client.Repository.Get(_owner, _repo)).Result;

            var tag = new NewTag
            {
                Message = _tag_message,
                Tag = _tag,
                Object = _refsha,
                Type = TaggedType.Commit,
                Tagger = new Committer(_tag_author, _tag_author_email, DateTime.UtcNow)
            };
            var result = Task.Run(() => client.Git.Tag.Create(repo.Id, tag)).Result;

            var reference = new NewReference($"refs/tags/{_tag}", _refsha);

            var resultRef = Task.Run(() => client.Git.Reference.Create(repo.Id, reference)).Result;
        }
    }
}
