using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.DataAccess.Interfaces;

namespace Project.Shop.BusinessLogic.DataServices
{
    public class SizeService : ISizeService
    {
        private readonly IGenericRepository<Size> _context;
        public SizeService(IGenericRepository<Size> context)
        {
            _context = context;
        }

        public List<Size> GetAllSizes()
        {
            var result = _context.GetAll().ToList();
            return result;
        }

        public async Task<Size> GetSingleSizeByIdAsync(int id)
        {
            var selectedSize =await _context.GetBySelectedIdAsync(id);
            return selectedSize;
        }

        public IEnumerable<Size> GetActiveSizes()
        {
            var sizes =_context.Get(a =>a.IsInactive == false).ToList();
            return sizes;
        }

        public async Task<Size> CreateSizeAsync(Size size)
        {
            var result = await _context.AddAsync(size);
            return result;
        }
        
        public async Task<int> UpdateSizeAsync(Size size)
        {
            Size selectedSize =await _context.GetBySelectedIdAsync(size.SizeId);
            if(selectedSize !=null)
            {
                selectedSize.Description = size.Description;
                selectedSize.IsInactive = size.IsInactive;
                await _context.UpdateAsync(selectedSize);
                return 1;
            }
            return 0;
        }
        
        public async Task<Size> DeleteSizeAsync(int id)
        {
            var result = await _context.DeleteAsync(id);
            return result;
        }
    }
}