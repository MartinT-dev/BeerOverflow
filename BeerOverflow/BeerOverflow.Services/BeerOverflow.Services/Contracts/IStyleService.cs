using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IStyleService
    {
        Task<StyleDTO> GetStyleAsync(int id);
        Task<StyleDTO> UpdateStyleAsync(int id, string newName);
        Task<StyleDTO> CreateStyleAsync(StyleDTO styleDTO);
        Task<ICollection<StyleDTO>> GetAllStylesAsync();
        Task DeleteStyleAsync(int id);
    }
}
