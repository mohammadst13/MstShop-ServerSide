using Microsoft.EntityFrameworkCore;
using MstShop_ServerSide.Core.Services.Interfaces;
using MstShop_ServerSide.DataLayer.Entities.Site;
using MstShop_ServerSide.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MstShop_ServerSide.Core.Services.Implementations
{
    public class SliderService : ISliderService
    {
        #region constructor

        private IGenericRepository<Slider> sliderRepository;

        public SliderService(IGenericRepository<Slider> sliderRepository)
        {
            this.sliderRepository = sliderRepository;
        }

        #endregion

        #region slider

        public async Task<List<Slider>> GetAllSliders()
        {
            return await sliderRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<List<Slider>> GetActiveSliders()
        {
            return await sliderRepository.GetEntitiesQuery().Where(s => !s.IsDelete).ToListAsync();
        }

        public async Task AddSlider(Slider slider)
        {
            await sliderRepository.AddEntity(slider);
            await sliderRepository.SaveChanges();
        }

        public async Task UpdateSlider(Slider slider)
        {
            sliderRepository.UpdateEntity(slider);
            await sliderRepository.SaveChanges();
        }

        public async Task<Slider> GetSliderById(long sliderId)
        {
            return await sliderRepository.GetEntityById(sliderId);
        }

        #endregion

        #region dispose

        public void Dispose()
        {
            sliderRepository?.Dispose();
        }

        #endregion
    }
}
