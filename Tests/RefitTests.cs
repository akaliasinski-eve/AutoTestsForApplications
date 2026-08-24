using System.Net;
using AutoTestsForApplications.DTO;
using AutoTestsForApplications.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AutoTestsForApplications;

public class RefitTests
{
    private IUserApiClient _client;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddRefitClient<IUserApiClient>()
            .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://reqres.in/api"); });

        var provider = services.BuildServiceProvider();
        _client = provider.GetRequiredService<IUserApiClient>();
    }

    [Test]
    public async Task Test1()
    {
        var result = await _client.GetUserAsync(2);
        Assert.Multiple(() =>
            {
                Assert.That(result.Data.Id, Is.EqualTo(2));
                Assert.That(result.Data.Email, Is.Not.Null);
            }
        );
    }

    [Test]
    public async Task Test2()
    {
        var newUser = new CreateUserRequestDTO(){ Name = "Jack", Job = "Company1"};
        var response = await _client.PostUserAsync(newUser);
        Assert.That(response.Name, Is.EqualTo("Jack"));
    }
    
    [Test]
    public async Task Test3()
    {
        var updatedUser = new CreateUserRequestDTO(){ Name = "Jack", Job = "Company2"};
        var response = await _client.PutUserAsync(2, updatedUser);
        Assert.That(response.Job, Is.EqualTo("Company2"));
    }
    
    [Test]
    public async Task Test4()
    {
        var response = await _client.DeleteUserAsync(2);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}