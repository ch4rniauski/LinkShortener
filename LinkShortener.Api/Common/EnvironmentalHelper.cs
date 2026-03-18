using DotNetEnv;

namespace ch4rniauski.LinkShortener.Api.Common;

public static class EnvironmentalHelper
{
    public static void SetEnvironmentVariablesFromDotEnvFile()
    {
        var envPath = Path.GetFullPath(Path.Combine("..", ".env"));
        Env.Load(envPath);
    }
}
