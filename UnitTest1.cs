using System.Net.Http.Json;
using System.Text.Json;
using AutoTestsForApplications.DTO;

namespace AutoTestsForApplications;

public class Tests
{
    private static HttpClient client;

    [OneTimeSetUp]
    public void Setup()
    {
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://reqres.in/api/")
        };
        client.DefaultRequestHeaders.Add("x-api-key", "free_user_3HpEVY8bH2spKvIokQS9nQmkm6s");
    }

    [Test]
    public async Task Test1()
    {
        //Get запрос
        using HttpResponseMessage response = await client.GetAsync("users/2");
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task Test2()
    {
        //Get запрос
        using HttpResponseMessage response = await client.GetAsync("users/2");
        string jsonGet = await response.Content.ReadAsStringAsync();
        UserResponseDTO userResponse = JsonSerializer.Deserialize<UserResponseDTO>(jsonGet);
        UserDataDTO userData = userResponse.Data;
        if (userData.Id == 2)
        {
        }
        else
        {
            throw new Exception();
        }
    }

    [Test]
    public async Task Test3()
    {
        var newUser = new CreateUserRequestDTO(){ Name = "Jack", Job = "Company1"};
        using HttpResponseMessage response = await client.PostAsJsonAsync("users", newUser);
        string jsonPost = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        CreatedUserDTO createdUser = JsonSerializer.Deserialize<CreatedUserDTO>(jsonPost);
    }
    
    [Test]
    public async Task Test4()
    {
        var newUser = new CreateUserRequestDTO(){ Name = "Jack", Job = "Company2"};
        using HttpResponseMessage response = await client.PutAsJsonAsync("users/2", newUser);
        response.EnsureSuccessStatusCode();
    }
    
    [Test]
    public async Task Test5()
    {
        using HttpResponseMessage response = await client.DeleteAsync("users/2");
        response.EnsureSuccessStatusCode();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        client.Dispose();
    }
}
//test x-api-key free_user_3HpEVY8bH2spKvIokQS9nQmkm6s