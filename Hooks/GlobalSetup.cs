

[SetUpFixture] //глобальные настройки запускаются один раз перед стартом всех автотестов
public class GlobalSetup
{
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Console.WriteLine("Выполняю 1 раз перед стартом всего");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("Выполняю 1 раз после окончания всего");
    }
}