namespace AutoTestsForApplications.Hooks;

public class HooksForGroupTests1
{
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Console.WriteLine("Выполняюсь 1 раз перед стартов всех тестов группы 1");
    }

    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Выполняюсь перед каждым тестом группы 1");
    }

    [TearDown]
    public void TearDown()
    {
        Console.WriteLine("Выполняюсь после каждого теста группы 1");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("Выполняюсь 1 раз после окончания всех тестов группы 1");
    }
}