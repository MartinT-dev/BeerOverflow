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
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services
{
    public class StyleService : IStyleService
    {
        private readonly BeeroverflowContext context;
        private readonly IDateTimeProvider dateTimeProvider;

        public StyleService(BeeroverflowContext context, IDateTimeProvider dateTimeProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }
        public async Task<StyleDTO> CreateStyleAsync(StyleDTO styleDTO)
        {
            var check = await this.context.Styles
                   .FirstOrDefaultAsync(x => x.Name == styleDTO.Name);

            if (check != null)
            {
                throw new ArgumentNullException("Style already existing");
            }

            var style = new Style
            {
                Name = styleDTO.Name,
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };

            await this.context.Styles.AddAsync(style);
            await this.context.SaveChangesAsync();
            styleDTO.Id = style.Id;
            return styleDTO;
        }

        public async Task DeleteStyleAsync(int id)
        {
            try
            {
                var style = await this.context.Styles
                    .Where(x => x.isDeleted == false)
                    .FirstOrDefaultAsync(x => x.Id == id);

                style.isDeleted = true;
                style.DeletedOn = this.dateTimeProvider.GetDateTime();
                await this.context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Style not existing.");
            }
        }

        public async Task<ICollection<StyleDTO>> GetAllStylesAsync()
        {
            var styles = await this.context.Styles 
               .Where(x => x.isDeleted == false)
               .ToListAsync();

            if (styles.Any() == false)
            {
                throw new ArgumentNullException("No styles exist.");
            }

            return styles.GetDtos();
        }

        public async Task<StyleDTO> GetStyleAsync(int id)
        {
            var style = await this.context.Styles
               .Include(x => x.Beers)
               .ThenInclude(x => x.Brewery)
               .ThenInclude(x => x.Country)
               .Where(x => x.isDeleted == false)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (style == null)
            {
                throw new ArgumentNullException("Style does not exist.");
            }

            return style.GetDto();
        }

        public async Task<StyleDTO> UpdateStyleAsync(int id, string newName)
        {
            var style = await this.context.Styles
               .Where(x => x.isDeleted == false)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (style == null)
            {
                throw new ArgumentNullException("Style does not exist.");
            }

            style.Name = newName;

            style.ModifiedOn = this.dateTimeProvider.GetDateTime();
            await this.context.SaveChangesAsync();
            return style.GetDto();
        }
    }
}
