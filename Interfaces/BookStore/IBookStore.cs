using AutoTestsForApplications.DTO;
using AutoTestsForApplications.DTO.BookStoreDTO;
using Refit;

namespace AutoTestsForApplications.Interfaces.BookStore;

public interface IBookStore
{
    [Post("/Account/v1/User")]
    Task<CreateUserResponseDTO> CeateUserAsync([Body] UserDTO user);
    [Post("/Account/v1/GenerateToken")]
    Task <GetTokenDTO> GetTokenAsync([Body] UserDTO user);
    [Post("/Account/v1/Login")]
    Task <LoginUserResponseDTO> LoginUserAsync([Body] UserDTO user);
    [Post("/BookStore/v1/Books")]
    Task <AddBookResponseDTO> AddBookAsync([Body] AddBookRequestDTO book, [Header("Authorization")] string token);
    [Get("/BookStore/v1/Books")]
    Task<BooksListDTO> GetBooksAsync();
    [Get("/BookStore/v1/Book")]
    Task<BookDTO> GetBookByIsbnAsync([Query] string ISBN);
    [Delete("/BookStore/v1/Book")]
    Task<BookDTO> DeleteBookByIsbnAsync([Body] DeleteBookRequestDTO book,
        [Header("Authorization")] string token);
    [Get("/Account/v1/User/{UUID}")]
    Task <AddBookResponseDTO> GetUserAsync(string uuid,[Header("Authorization")] string token);
}