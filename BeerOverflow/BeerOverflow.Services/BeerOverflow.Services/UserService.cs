using BeerOverflow.Data;
using BeerOverflow.Data.BeerOverflowContext;
using BeerOverflow.Data.Entities;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOmappers;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Services
{
    public class UserService : IUserService
    {
        private readonly BeeroverflowContext context;
        private readonly IDateTimeProvider dateTimeProvider;
        public UserService(BeeroverflowContext context, IDateTimeProvider dateTimeProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }
        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            var check = await this.context.Users
                    .FirstOrDefaultAsync(x => x.UserName == userDTO.Username);

            if (check != null)
            {
                throw new ArgumentNullException("User already exist.");
            }
            var user = new User
            {
                UserName = userDTO.Username,
                Country = userDTO.Country,
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();
            userDTO.Id = user.Id;
            return userDTO;
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var user = await this.context.Users
                    .Where(user => user.isDeleted == false)
                    .FirstOrDefaultAsync(user => user.Id == id);

                user.isDeleted = true;
                user.DeletedOn = this.dateTimeProvider.GetDateTime();
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentNullException("User does not exist.");
            }
        }

        public async Task<ICollection<UserDTO>> GetAllUsersAsync()
        {
            var users = await this.context.Users
                 .Where(users => users.isDeleted == false)
                 .ToListAsync();

            if (users.Any() == false)
            {
                throw new ArgumentNullException("No users exist.");
            }

            return users.GetDtos();
        }

        public async Task<UserDTO> GetUserAsync(int id)
        {
            var user = await this.context.Users
                .Include(u=>u.DrankLists)
                //.ThenInclude(d=>d.Beer)
                //.ThenInclude(b=>b.Brewery)
                //.ThenInclude(b=>b.Country)
                .Include(u=>u.WishLists)
                //.ThenInclude(d => d.Beer)
                //.ThenInclude(b => b.Brewery)
                //.ThenInclude(b => b.Country)
                .Include(u=>u.Ratings)
                .ThenInclude(d => d.Beer)
                .ThenInclude(b => b.Brewery)
                .ThenInclude(b => b.Country)
                .Include(u=>u.Reviews)
                //.ThenInclude(d => d.Beer)
                //.ThenInclude(b => b.Brewery)
                //.ThenInclude(b => b.Country)
                .Include(u=>u.Likes)
                 .Where(u => u.isDeleted == false)
                 .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentNullException("User does not exist.");
            }
            return user.GetDto();
        }

        public async Task<UserDTO> UpdateUserAsync(int id, string newName)
        {
            var user = await this.context.Users
                .Where(user => user.isDeleted == false)
                .FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                throw new ArgumentNullException("User does not exist.");
            }

            user.UserName = newName;
            user.ModifiedOn = this.dateTimeProvider.GetDateTime();

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
            };

            await this.context.SaveChangesAsync();
            return userDTO;
        }

        public async Task ReviewLikeAsync(int reviewId, int userId, User user)
        {
            var review = await this.context.Reviews
                .Include(r=>r.User)
                .Where(x => x.Id == reviewId)
                .FirstOrDefaultAsync();

            var check = await this.context.Likes
                .Include(r => r.User)
                .Where(x => x.ReviewId == reviewId)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (check != null)
            {
                if (check.isLiked == true)
                {
                    check.isLiked = false;
                    await this.context.SaveChangesAsync();
                    return;
                }
                else
                {
                    check.isLiked = true;
                    check.isDisliked = false;
                    await this.context.SaveChangesAsync();
                    return;
                }
            }

            var like = new Like
            {
                UserId = userId,
                User = user,
                ReviewId = reviewId,
                Review = review,
                isLiked = true,
                isDisliked=false
            };

            await this.context.Likes.AddAsync(like);
            await this.context.SaveChangesAsync();
        }
        public async Task ReviewDislikeAsync(int reviewId, int userId, User user)
        {
            var review = await this.context.Reviews
               .Where(x => x.Id == reviewId)
               .FirstOrDefaultAsync();

            var check = await this.context.Likes
                .Where(x => x.ReviewId == reviewId)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (check != null)
            {
                if (check.isDisliked == true)
                {
                    check.isDisliked = false;
                    await this.context.SaveChangesAsync();
                    return;
                }
                else
                {
                    check.isDisliked = true;
                    check.isLiked = false;
                    await this.context.SaveChangesAsync();
                    return;
                }
            }

            var like = new Like
            {
                UserId = userId,
                User = user,
                ReviewId = reviewId,
                Review = review,
                isLiked = false,
                isDisliked = true
            };

            await this.context.Likes.AddAsync(like);
            await this.context.SaveChangesAsync();
        }
        public async Task ReviewFlagAsync(int reviewId)
        {
            var review = await this.context.Reviews
               .Where(x => x.Id == reviewId)
               .FirstOrDefaultAsync();

            review.isFlagged = true;

            await this.context.SaveChangesAsync();
        }

        public async Task ReviewDeleteAsync(int reviewId)
        {
            var review = await this.context.Reviews
               .Where(x => x.Id == reviewId)
               .FirstOrDefaultAsync();

            review.isDeleted = true;
            review.UserId = null;
            review.User = null;
            review.BeerId = null;
            review.User = null;

            await this.context.SaveChangesAsync();
        }

        public async Task <ICollection<ReviewDTO>> RetreiveAllFlaggedReviewsAsync()
        {
            var reviews = await this.context.Reviews
                .Include(r=>r.Beer)
                .Include(r=>r.User)
                .Where(x => x.isFlagged == true)
                .Where(x=>x.isDeleted == false)
                .ToListAsync();

            var reviewDTOs = reviews.GetDtos();

            return reviewDTOs;
        }

        public async Task ReviewUnflagAsync(int reviewId)
        {
            var review = await this.context.Reviews
               .Where(x => x.Id == reviewId)
               .FirstOrDefaultAsync();

            review.isFlagged = false;

            await this.context.SaveChangesAsync();
        }

        public async Task AddToWishListAsync(int userId, User user, int beerId)
        {
            var beer = await this.context.Beers
               .Where(x => x.Id == beerId)
               .FirstOrDefaultAsync();

            var check = await this.context.WishLists
                .Include(r => r.User)
                .Where(x => x.BeerId == beerId)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (check !=null)
            {
                return;
            }

            var wishlist = new WishList
            {
                UserId = userId,
                User = user,
                BeerId = beerId,
                Beer = beer
            };

            await this.context.WishLists.AddAsync(wishlist);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveFromWishListAsync(int userId, User user, int beerId)
        {
            var beer = await this.context.Beers
               .Where(x => x.Id == beerId)
               .FirstOrDefaultAsync();

            var check = await this.context.WishLists
                .Include(r => r.User)
                .Where(x => x.BeerId == beerId)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (check == null)
            {
                return;
            }

            context.WishLists.Remove(check);
            await this.context.SaveChangesAsync();
        }

        public async Task AddToDrankListAsync(int userId, User user, int beerId)
        {
            var beer = await this.context.Beers
               .Where(x => x.Id == beerId)
               .FirstOrDefaultAsync();

            var check = await this.context.DrankLists
                .Include(r => r.User)
                .Where(x => x.BeerId == beerId)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (check != null)
            {
                return;
            }

            var dranklist = new DrankList
            {
                UserId = userId,
                User = user,
                BeerId = beerId,
                Beer = beer
            };

            await this.context.DrankLists.AddAsync(dranklist);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveFromDrankListAsync(int userId, User user, int beerId)
        {
            var beer = await this.context.Beers
               .Where(x => x.Id == beerId)
               .FirstOrDefaultAsync();

            var check = await this.context.DrankLists
                .Include(r => r.User)
                .Where(x => x.BeerId == beerId)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (check == null)
            {
                return;
            }
            context.DrankLists.Remove(check);
            await this.context.SaveChangesAsync();
        }

        public async Task<ICollection<BeerDTO>> RetreiveWishListAsync(int userId)
        {
            var beers = await this.context.WishLists
               .Where(x => x.UserId == userId)
               .Select(x=>x.BeerId)
               .ToListAsync();

            var result = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .Include(b => b.Country)
                .Include(b => b.Ratings)
                .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                .ThenInclude(u => u.Likes)
                .ThenInclude(l => l.User)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .Where(x => beers.Contains(x.Id))
                .ToListAsync();

            var beerDTOs = result.GetDtos();
            return beerDTOs;
        }

        public async Task <ICollection<BeerDTO>> RetreiveDrankListAsync(int userId)
        {
            var beers = await this.context.DrankLists
               .Where(x => x.UserId == userId)
               .Select(x => x.BeerId)
               .ToListAsync();

            var result = await this.context.Beers
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .Include(b => b.Country)
                .Include(b => b.Ratings)
                .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                .ThenInclude(u => u.Likes)
                .ThenInclude(l => l.User)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .Where(x => beers.Contains(x.Id))
                .ToListAsync();

            var beerDTOs = result.GetDtos();
            return beerDTOs;
        }

        public async Task BanUserAsync(int userId)
        {
            var user = await this.context.Users
               .Where(x => x.Id == userId)
               .FirstOrDefaultAsync();

            user.isBanned = true;
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.Now.AddDays(7);

            await this.context.SaveChangesAsync();
        }

        public async Task UnbanUserAsync(int userId)
        {
            var user = await this.context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            user.isBanned = false;
            user.LockoutEnd = DateTime.Now;

            await this.context.SaveChangesAsync();
        }
    }
}
