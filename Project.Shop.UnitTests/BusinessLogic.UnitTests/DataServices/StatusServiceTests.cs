using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Project.Shop.DataAccess.Interfaces;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.BusinessLogic.DataServices;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Shop.UnitTests.BusinessLogicUnitTests.DataServices
{
    [TestFixture]
    public class StatusSErviceTests
    {
        private Mock<IGenericRepository<Status>> _context;
        private IStatusService _statusService;
        private List<Status> _statuses;
        private Status _existingStatus;
        private Status _newStatus;
        private int _existingId;


        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IGenericRepository<Status>>();

            _statuses = new List<Status>()
            {
                new Status(){StatusId=1, Description="status_a", IsInactive=false},
                new Status(){StatusId=2, Description="status_b", IsInactive=true},
                new Status(){StatusId=3, Description="status_c", IsInactive=false},
            };
            _newStatus =  new Status(){StatusId=4, Description="status_d", IsInactive=false};
            _existingStatus = _statuses[0];
            _context
            .Setup(c =>c.GetAll())
            .Returns(()=>{
                return _statuses.AsQueryable();
            });

            _statusService = new StatusService(_context.Object);
        }
        
        [Test]
        public void GetAllStatuses_WhenCalled_ReturnsAllStatuses()
        {
            //arrange
            //act
            var result = _statusService.GetAllStatuses();
            //assert
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetActiveStatuses_WhenCalled_ReturnsTwoItems()
        {
            _context
            .Setup(c=>c.Get(a=>a.IsInactive == false))
            .Returns(()=>{
                return _statuses.Where(a=>a.IsInactive== false ).ToList();
            });
            var result = _statusService.GetActiveStatuses();
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetSingleStatusByIdAsync_WhenCallingExistingId_ReturnsObject()
        {
           
            _context
            .Setup(c=>c.GetBySelectedIdAsync(_existingStatus.StatusId))
            .Returns(async ()=>{
                return  await Task.Run(()=>{
                    return _existingStatus;
                });
            });

            var result =await  _statusService.GetSingleStatusByIdAsync(_existingStatus.StatusId);
            _context.Verify(c=>c.GetBySelectedIdAsync(_existingStatus.StatusId), Times.Once);
        }

        [Test]
        public void CreateStatusAsync_WhenCalled_ReturnsCreatedObject()
        {
            _context
            .Setup(c=>c.AddAsync(_newStatus))
            .Returns(async ()=>{
                return await Task.Run(()=>{
                    return _newStatus;
                });
            });

            var result = _statusService.CreateStatusAsync(_newStatus);
            _context.Verify(c=>c.AddAsync(_newStatus), Times.Once);
        }

        [Test]
        public async Task UpdateStatusAsync_WhenUpdatedObjectExists_ReturnsOne()
        {
            _existingStatus.Description= "updatedDescription";

            _context
            .Setup(c=>c.GetBySelectedIdAsync(_existingStatus.StatusId))
            .Returns(async()=>{
                return await Task.Run(()=>{
                    return _statuses.Where(a=>a.StatusId == _existingStatus.StatusId).FirstOrDefault();
                });               
            });

            _context
            .Setup(c=>c.UpdateAsync(_existingStatus))
            .Returns(async()=>{
                return await Task.Run(()=>{
                    return _existingStatus;
                });
            });

            var result = await _statusService.UpdateStatusAsync(_existingStatus);

            _context.Verify(c=>c.UpdateAsync(_existingStatus), Times.Once);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateStatusAsync_WhenUpdatedObjectExists_ReturnsZero()
        {
            _newStatus.Description= "updatedDescription";

            _context
            .Setup(c=>c.GetBySelectedIdAsync(_newStatus.StatusId))
            .Returns(async()=>{
                return await Task.Run(()=>{
                    return _statuses.Where(a=>a.StatusId == _newStatus.StatusId).FirstOrDefault();
                });               
            });

            _context
            .Setup(c=>c.UpdateAsync(_newStatus))
            .Returns(async()=>{
                return await Task.Run(()=>{
                    return _newStatus;
                });
            });

            var result =await _statusService.UpdateStatusAsync(_newStatus);

            _context.Verify(c=>c.UpdateAsync(_newStatus), Times.Never);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteStatusAsync_WhenCalledIfObjectExist_ReturnsDeletedObject()
        {
            _context
            .Setup(c=>c.DeleteAsync(_existingStatus.StatusId))
            .Returns(async()=>{
                return await Task.Run(()=>{
                    return _existingStatus;
                });
            });

            var result =await _statusService.DeleteStatusAsync(_existingStatus.StatusId);

            _context.Verify(c=>c.DeleteAsync(_existingStatus.StatusId), Times.Once);
            Assert.IsNotNull(result);
        }

         [Test]
        public async Task DeleteStatusAsync_WhenCalledIfObjectDoesNotExist_ReturnsNull()
        {
            _context
            .Setup(c=>c.DeleteAsync(_newStatus.StatusId))
            .Returns(async()=>{
                return await Task.Run(()=>{
                    Status newStatusItem = null;
                    return newStatusItem;
                });
            });
            var result =await _statusService.DeleteStatusAsync(_existingStatus.StatusId);
            _context.Verify(c=>c.DeleteAsync(_existingStatus.StatusId), Times.Once);
            Assert.IsNull(result);
        }
        
    }
}