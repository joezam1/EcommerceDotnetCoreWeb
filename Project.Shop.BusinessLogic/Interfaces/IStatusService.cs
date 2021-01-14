using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Shop.DataAccess.DataModels;

namespace Project.Shop.BusinessLogic.Interfaces
{
    public interface IStatusService
    {
        List<Status> GetAllStatuses();
        Task<Status> GetSingleStatusByIdAsync(int id);
        IEnumerable<Status> GetActiveStatuses();
        Task<Status> CreateStatusAsync(Status status);
        Task<int> UpdateStatusAsync(Status status);
        Task<Status> DeleteStatusAsync(int id);
    }
}