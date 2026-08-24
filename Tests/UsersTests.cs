using System.Text.Json;
using AutoTestsForApplications.DTO.UsersDTOs;
using FluentAssertions;

namespace AutoTestsForApplications;

public class UsersTests
{
    private RootDTO usersData;

    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "UsersData.json");
        string data = File.ReadAllText(path);
        usersData = JsonSerializer.Deserialize<RootDTO>(data);
    }


    //2.1 Проверить, что количество юзеров из файла равно 10
    [Test]
    public void CheckNumberOfUsers()
    {
        var numberOfUsers = usersData.Data.Count;
        numberOfUsers.Should().Be(10);
    }


    //2.2 Проверить, что первый юзер - Alice Johnson
    [Test]
    public void CheckFirstUser()
    {
        var firstUserFullname = usersData.Data.First().Profile.FullName;
        firstUserFullname.Should().Be("Alice Johnson");
    }

    //2.3 Проверить, что все Id уникальны
    [Test]
    public void CheckAllUserIdsAreUnique()
    {
        var listOfUserIds = usersData.Data.Select(x => x.Id).ToList();
        bool allUnique = listOfUserIds.Distinct().Count() == listOfUserIds.Count;
        allUnique.Should().BeTrue();
    }

    //2.4 Проверить, что есть хотя бы один премиум-пользователь (тег premium)
    [Test]
    public void CheckAtLeastOneUserIsPremium()
    {
        var listOfPremiumUsers = usersData.Data.Where(x => x.Profile.Tags.Contains("premium")).ToList();
        listOfPremiumUsers.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    //2.5 Проверить, что у всех юзеров поле город - не пустой
    [Test]
    public void CheckAllUsersHaveNotEmptyCity()
    {
        var listOfUsersWithoutCity = usersData.Data.Where(x => string.IsNullOrEmpty(x.Profile.Address.City)).ToList();
        listOfUsersWithoutCity.Count.Should().Be(0);
    }

    //2.6 Проверить, что есть хотя бы один пользователь из Стокгольма
    [Test]
    public void CheckAtLeastOneUserIsFromStockholm()
    {
        bool isAnobodyFromStockholm = usersData.Data.Any(x => x.Profile.Address.City == "Stockholm");
        isAnobodyFromStockholm.Should().BeTrue();
    }

    //2.7 Проверить, что возраст всех юзеров в диапазоне 18-60 лет
    [Test]
    public void CheckAllUsersAreBetween18And60()
    {
        bool allUsersAreBetween18And60 = !usersData.Data.Any(x => x.Profile.Age < 18 || x.Profile.Age > 60);
        allUsersAreBetween18And60.Should().BeTrue();
    }

    //2.8 Проверить, что есть хотя бы один юзер с ролью admin
    [Test]
    public void CheckAtLeastOneUsrIsAdmin()
    {
        bool thereIsAtLeasOneAdmin = usersData.Data.Any(x => x.Roles.Contains("admin"));
        thereIsAtLeasOneAdmin.Should().BeTrue();
    }

    //3. Проверить, что все юзеры (их координаты) находятся в диапазоне Швеции
    [Test]
    public void CheckAllUserCoordinatesAreInSweden()
    {
        var SwedenNorthLat = 69.04;
        var SwedenSouthLat = 55.2;
        var SwedenEastLng = 24.15;
        var SwedenWestLng = 11.11;
        bool allUserCoordinatesAreInSweden = !usersData.Data.Any(x =>
            x.Profile.Address.Geo.Lat > SwedenNorthLat || x.Profile.Address.Geo.Lat < SwedenSouthLat ||
            x.Profile.Address.Geo.Lng > SwedenEastLng || x.Profile.Address.Geo.Lat < SwedenWestLng);
        allUserCoordinatesAreInSweden.Should().BeTrue();
    }
}