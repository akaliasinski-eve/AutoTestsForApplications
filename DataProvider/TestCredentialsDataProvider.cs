namespace AutoTestsForApplications.DataProvider;

public static class TestCredentialsDataProvider
{
    private const string CredentialsDataFilePath = @"Resources\TestCredentials.csv";

    public static IEnumerable<TestCaseData> GetCredentialsCases()
    {
        string baseDirectory = AppContext.BaseDirectory;
        string fullPath = Path.Combine(baseDirectory, CredentialsDataFilePath);
        var lines = File.ReadAllLines(fullPath);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(',');
            string login = parts[0];
            string password = parts[1];
            yield return new TestCaseData(login, password);
        }
    }
}