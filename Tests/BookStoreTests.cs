using AutoTestsForApplications.DTO.BookStoreDTO;
using AutoTestsForApplications.Helpers;
using AutoTestsForApplications.Interfaces.BookStore;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AutoTestsForApplications;

public class BookStoreTests
{
    private IBookStore api;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddRefitClient<IBookStore>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://demoqa.com"));

        var provider = services.BuildServiceProvider();
        api = provider.GetRequiredService<IBookStore>();
    }

    [Test]
    public async Task CreateUser()
    {
        var user = new UserDTO { UserName = "Andrei", Password = "StrongPass322!" };
        var response = await api.CeateUserAsync(user);


        //UserId = ac3a06f9-28cf-4664-b889-124391df5be3
    }

    [Test]
    public async Task GnerateToken()
    {
        var user = new UserDTO { UserName = "Andrei", Password = "StrongPass322!" };
        var response = await api.GetTokenAsync(user);
        response.Token.Should().NotBeNullOrEmpty();
        response.Status.Should().Be("Success");
        response.Result.Should().Contain("authorized");
    }

    [Test]
    public async Task LoginUserAsync()
    {
        var user = new UserDTO { UserName = "Andrei", Password = "StrongPass322!" };
        var response = await api.LoginUserAsync(user);
        response.UserId.Should().NotBeNullOrEmpty();
        response.UserName.Should().Be(user.UserName);
    }

    [Test]
    public async Task AddBookAsync()
    {
        var userId = await GetUserIdAsync();
        var token = await GetTokenAsync();

        var newBook = new AddBookRequestDTO
        {
            UserId = userId, CollectionOfIsbns = new List<BookDTO> { new BookDTO { Isbn = "9781449325862" } }
        };

        var addBookResponse = await api.AddBookAsync(newBook, token);
        addBookResponse.Books.Should().HaveCount(1);
    }

    [Test]
    public async Task GetAllBookAsync()
    {
        var response = await api.GetBooksAsync();
        response.Should().NotBeNull();
        response.Books.Should().HaveCount(8);
    }

    [Test]
    public async Task GetBookByIsbnAsync()
    {
        var response = await api.GetBooksAsync();
        var rndBook = RandomHelper.GetRandomItem(response.Books);
        var bookIsbn = rndBook.Isbn;
        var book = await api.GetBookByIsbnAsync(bookIsbn);
        book.SubTitle.Should().Be(rndBook.SubTitle);
    }

    [Test]
    public async Task AddMultipleBooksAsync()
    {
        var userId = await GetUserIdAsync();
        var token = await GetTokenAsync();

        var newBook = new AddBookRequestDTO
        {
            UserId = userId, CollectionOfIsbns = new List<BookDTO>
            {
                new BookDTO { Isbn = "9781449325862" },
                new BookDTO { Isbn = "9781449331818" }
            }
        };

        var addBookResponse = await api.AddBookAsync(newBook, token);
        addBookResponse.Books.Should().HaveCount(2);
    }

    [Test]
    public async Task AddBookWithoutToken()
    {
        var userId = await GetUserIdAsync();
        var newBook = new AddBookRequestDTO
        {
            UserId = userId, CollectionOfIsbns = new List<BookDTO>
            {
                new BookDTO { Isbn = "9781449325862" },
                new BookDTO { Isbn = "9781449331818" }
            }
        };
        Func<Task> action = async () =>
            await api.AddBookAsync(newBook, token: null); // делегат
        action.Should().ThrowAsync<ApiException>().Where(e => e.StatusCode == System.Net.HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task AddMultipleBooksInvalidIsbnAsync()
    {
        var userId = await GetUserIdAsync();
        var token = await GetTokenAsync();

        var newBook = new AddBookRequestDTO
        {
            UserId = userId, CollectionOfIsbns = new List<BookDTO>
            {
                new BookDTO { Isbn = "9781449325862" },
                new BookDTO { Isbn = "INVALIDISBN" }
            }
        };

        Func<Task> action = async () =>
            await api.AddBookAsync(newBook, token); // делегат
        action.Should().ThrowAsync<ApiException>().Where(e => e.StatusCode == System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task DeleteBookByIsbnAsync()
    {
        var userId = await GetUserIdAsync();
        var token = await GetTokenAsync();
        var user = await api.GetUserAsync(userId, token);
        foreach (var book in user.Books)
        {
            var b = new DeleteBookRequestDTO { Isbn = book.Isbn, UserId = userId };
            await api.DeleteBookByIsbnAsync(b, token);
        }

        var updatedUser = await api.GetUserAsync(userId, token);
        updatedUser.Books.Should().HaveCount(0);
    }

    private async Task<string> GetTokenAsync()
    {
        var user = new UserDTO { UserName = "Andrei", Password = "StrongPass322!" };
        var getToken = await api.GetTokenAsync(user);
        var token = $"Bearer {getToken.Token}";
        return token;
    }

    private async Task<string> GetUserIdAsync()
    {
        var user = new UserDTO { UserName = "Andrei", Password = "StrongPass322!" };
        var response = await api.LoginUserAsync(user);
        var userId = response.UserId;
        return userId;
    }
}