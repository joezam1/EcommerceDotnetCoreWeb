using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.DataAccess.Interfaces;

namespace Project.Shop.BusinessLogic.DataServices
{
    public class StatusService : IStatusService
    {
        private readonly IGenericRepository<Status> _context;
        public StatusService(IGenericRepository<Status> context)
        {
            _context = context;
        }

        public List<Status> GetAllStatuses()
        {
            var result = _context.GetAll().ToList();
            return result;
        }

        public IEnumerable<Status> GetActiveStatuses()
        {
              var status =_context.Get(a =>a.IsInactive == false).ToList();
            return status;
        }

        public async Task<Status> GetSingleStatusByIdAsync(int id)
        {
            var selectedStatus =await _context.GetBySelectedIdAsync(id);
            return selectedStatus;
        }

        public async Task<Status> CreateStatusAsync(Status status)
        {
             var result = await _context.AddAsync(status);
            return result;
        }

        public async Task<int> UpdateStatusAsync(Status status)
        {
            Status selectedStatus =await _context.GetBySelectedIdAsync(status.StatusId);
            if(selectedStatus !=null)
            {
                selectedStatus.Name = status.Name;
                selectedStatus.Description = status.Description;
                selectedStatus.IsInactive = status.IsInactive;
                await _context.UpdateAsync(selectedStatus);
                return 1;
            }
            return 0;
        }

        public async Task<Status> DeleteStatusAsync(int id)
        {
             var result = await _context.DeleteAsync(id);
            return result;
        }
    }
}