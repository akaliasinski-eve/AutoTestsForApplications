using AutoTestsForApplications.Hooks;

namespace AutoTestsForApplications.GroupTests;

[TestFixture]
//[Parallelizable(ParallelScope.Fixtures)] // параллельно ходят фикстуры(классы), а тесты внутри идут попорядку
//[Parallelizable(ParallelScope.Children)] //внутри фикстуры с тестами тесты идут параллельно, но сами фикстуры последовательно 
//[Parallelizable(ParallelScope.All)]//=[Parallelizable]
[Parallelizable(ParallelScope.Self)] // тест может идти параллельно с другими, но не с собственными параметризованными запусками
public class Group1Tests : HooksForGroupTests1
{
    [Test]
    [Parallelizable]
    [Description("")] //описание теста
    [Repeat(10)] // запустить 10 раз - если хоть раз упадет, то будет красным 
    public void Test11()
    {
        Assert.Pass();
    }

    [Test]
    [Timeout(30000)]
    [Category("QA")]
    [CancelAfter(50000)] // если не уложится, то будет красным
    public void Test12()
    {
        Assert.Pass();
    }

    [Test]
    [Ignore("reason")]
    [Order(1)]
    [Retry(2)] // перезапустит тест после падения
    public void Test13()
    {
        Assert.Pass();
    }
}