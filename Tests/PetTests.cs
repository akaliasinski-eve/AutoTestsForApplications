using AutoTestsForApplications.Helpers;
using AutoTestsForApplications.Interfaces.PetStore;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AutoTestsForApplications;

public class PetTests
{
    public IPetApi PetApi { get; set; }

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
        };
        services.AddRefitClient<IPetApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://petstoreapi.com/v1");
            })
            .ConfigurePrimaryHttpMessageHandler(() => handler);
        var provider = services.BuildServiceProvider();
        PetApi = provider.GetService<IPetApi>();
    }

    [Test]
    public async Task GetAllPetsAsync()
    {
        var pets = await PetApi.GetAllPetsAsync();
        pets.Data.Should().HaveCount(20);
    }

    [Test]
    public async Task GetPetByIdAsync()
    {
        var pets = await PetApi.GetAllPetsAsync();
        pets.Data.Should().HaveCount(20);

        var rndId = RandomHelper.GetRandomItem(pets.Data).Id;
        var pet = await PetApi.GetPetByIdAsync(rndId);
        var pet2 = pets.Data.Where(p => p.Id == pet.Id).FirstOrDefault();
        
        pet.AgeMonths.Should().Be(pet2.AgeMonths);
        pet.Should().NotBeNull();
    }
    
    [Test]
    public async Task GetAllPetsByStatusAndLimitAsync()
    {
        var pets = await PetApi.GetAllPetsByStatusAndLimitAsync("ADOPTED", 12);
        pets.Data.Should().HaveCount(12);
    }
}