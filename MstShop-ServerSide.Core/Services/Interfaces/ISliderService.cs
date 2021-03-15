using MstShop_ServerSide.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MstShop_ServerSide.Core.Services.Interfaces
{
    public interface ISliderService : IDisposable
    {
        Task<List<Slider>> GetAllSliders();
        Task<List<Slider>> GetActiveSliders();
        Task AddSlider(Slider slider);
        Task UpdateSlider(Slider slider);
        Task<Slider> GetSliderById(long sliderId);
    }
}
