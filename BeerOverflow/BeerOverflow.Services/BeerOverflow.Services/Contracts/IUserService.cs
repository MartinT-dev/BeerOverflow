using BeerOverflow.Data.Entities;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDTO> GetUserAsync(int id);
        Task<ICollection<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task<UserDTO> UpdateUserAsync(int id,string newName);
        Task DeleteUserAsync(int id);
        Task ReviewLikeAsync(int reviewId, int userId, User user);
        Task ReviewDislikeAsync(int reviewId, int userId, User user);
        Task ReviewFlagAsync(int reviewId);
        Task ReviewUnflagAsync(int reviewId);
        Task ReviewDeleteAsync(int reviewId);

        Task<ICollection<ReviewDTO>> RetreiveAllFlaggedReviewsAsync();
        Task AddToWishListAsync(int userId, User user, int beerId);
        Task RemoveFromWishListAsync(int userId, User user, int beerId);
        Task AddToDrankListAsync(int userId, User user, int beerId);
        Task RemoveFromDrankListAsync(int userId, User user, int beerId);
        Task BanUserAsync(int userId);
        Task UnbanUserAsync(int userId);

        Task<ICollection<BeerDTO>> RetreiveWishListAsync(int userId);

        Task<ICollection<BeerDTO>> RetreiveDrankListAsync(int userId);
    }
}
