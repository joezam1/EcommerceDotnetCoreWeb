using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using Project.Shop.DataAccess.Interfaces;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.BusinessLogic.DataServices;
using Project.Shop.BusinessLogic.Interfaces;

namespace Project.Shop.UnitTests.DataServices
{
    [TestFixture]
    public class SizeServiceTests
    {

        private Mock<IGenericRepository<Size>> _context;
        private ISizeService _sizeService;
        private int _existingSizeId;
        private List<Size> _sizes;
        private Size _newSize;
        private Size _existingSize;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IGenericRepository<Size>>();

             _sizes = new List<Size>(){
                new Size(){SizeId = 1, Description="size_a",IsInactive=false},
                new Size(){SizeId = 2, Description="size_b",IsInactive=true},
                new Size(){SizeId = 3, Description="size_c",IsInactive=false},
            };

            _context.
            Setup(c =>c.GetAll())
            .Returns(()=>{
                return _sizes.AsQueryable();
            });           

            _newSize =  new Size(){SizeId = 4, Description="size_d",IsInactive=false};

            // var newSize = new Size(){
            //     SizeId = 5,
            //     Description = "size_e"
            // };
            _sizeService = new SizeService(_context.Object);
        }

        [Test]
        public void GetAllSizes_WhenCalled_ReturnsAllSizes()
        {
            var result = _sizeService.GetAllSizes();
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetSingleSizeByIdAsync_WhenCalled_ReturnsSelectedId()
        {
            //Arrange
             _existingSizeId = 2;
            _context.
            Setup(c =>c.GetBySelectedIdAsync(_existingSizeId))
            .Returns(async ( )=>{
                return await Task.Run(()=>{
                    return  _sizes.AsQueryable().Where(a=>a.SizeId == _existingSizeId).FirstOrDefault();
                });               
            });

            //Act
            var result = await _sizeService.GetSingleSizeByIdAsync(_existingSizeId);
            //Assert
            Assert.That(result.SizeId, Is.EqualTo(_existingSizeId));
        }

        [Test]
        public void GetActiveSizes_WhenCalled_ReturnsAllActiveSizesOnly()
        {
            //arrange
            _context
            .Setup(c =>c.Get(a =>a.IsInactive == false))
            .Returns(()=>{
                var result = _sizes.Where(a=>a.IsInactive == false).ToList();
                return result;
            });
            //act
            var result = _sizeService.GetActiveSizes();
            //assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task CreateSizeAsync_WhenCalled_AddsObjectToList()
        {
            //Arrange
            _context
            .Setup(c=>c.AddAsync(_newSize))
            .Returns(async ()=>{
                return await Task.Run(() =>{
                    return _newSize;
                });
            });
            //Act
            var result = await _sizeService.CreateSizeAsync(_newSize);
            //Assert
            _context.Verify(c =>c.AddAsync(_newSize), Times.Once);
        }

        [Test]
        public async Task UpdateSizeAsync_WhenObjectIsNotFound_ReturnsZero()
        {
            //Arrange
            _context
            .Setup(c=>c.GetBySelectedIdAsync(_newSize.SizeId))
            .Returns(async ()=>{
                return await Task.Run(()=>{
                    return _sizes.Where(a =>a.SizeId == _newSize.SizeId).FirstOrDefault();
                });
            });
            //Act
            var result =await _sizeService.UpdateSizeAsync(_newSize);
            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateSizeAsync_WhenObjectIsFound_ReturnsIntegerOne()
        {
            //Arrange
            var currentSize = _sizes[1];
            _context
            .Setup(c=>c.GetBySelectedIdAsync(currentSize.SizeId))
            .Returns(async ()=>{
                return await Task.Run(()=>{
                    return _sizes.Where(a =>a.SizeId == currentSize.SizeId).FirstOrDefault();
                });
            });
            //Act
            var result =await _sizeService.UpdateSizeAsync(currentSize);
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteSizeAsync_IfObjectExists_ReturnsDeletedObject()
        {
            //arrange
            _existingSize = _sizes[0];
            _context
            .Setup(c =>c.DeleteAsync(_existingSize.SizeId))
            .Returns(async()=>{
                return await Task.Run(()=>{
                    return _existingSize;    
                });
            });
            //act
            var result = await _sizeService.DeleteSizeAsync(_existingSize.SizeId);
            //assert
            _context.Verify(c =>c.DeleteAsync(_existingSize.SizeId), Times.Once);
        }
        [Test]
        public async Task DeleteSizeAsync_IfObjectDoesNotExist_ReturnsNull()
        {
            //arrange
            _context
            .Setup(c =>c.DeleteAsync(_newSize.SizeId))
            .Returns(async()=>{
                return await Task.Run(()=>{
                     Size existing = null;
                     return existing;
                });
            });
            //act
            var result = await _sizeService.DeleteSizeAsync(_newSize.SizeId);
            //assert
            _context.Verify(c=>c.DeleteAsync(_newSize.SizeId), Times.Once);
        }
    }

}