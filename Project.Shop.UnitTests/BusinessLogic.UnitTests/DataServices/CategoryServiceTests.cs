using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using NUnit.Framework;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.BusinessLogic.DataServices;
using Project.Shop.DataAccess.Interfaces;
using Project.Shop.DataAccess.DataModels;
using Moq;

namespace Project.Shop.UnitTests.BusinessLogic.UnitTests.DataServices
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private ICategoryService _categoryService ;
        private Mock<IGenericRepository<Category>> _context;
        private int _categoryId;
        private List<Category> _allCategories;
        private Category _newCategory;
        private Category _existingCategory;
        private Category _deletedCategory;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IGenericRepository<Category>>();
            _allCategories = new List<Category>()
                {
                    new Category(){
                        CategoryId = 1,
                        Name = "a",
                        Description ="a_description",
                        IsInactive = false
                    },
                     new Category(){
                        CategoryId = 2,
                        Name = "b",
                        Description ="b_description",
                        IsInactive = true
                    },
                     new Category(){
                        CategoryId = 3,
                        Name = "c",
                        Description ="c_description",
                        IsInactive = false
                    }

                };
            _context
            .Setup( (ctx)=>ctx.GetAll())
            .Returns(() => 
            {
                return _allCategories.AsQueryable();
            });
            
            _context
            .Setup(ctx => ctx.Get(a =>a.IsInactive == false))
            .Returns( ()=>
            {
                return _allCategories.AsQueryable().Where(a =>a.IsInactive == false);
            } );
           
           _categoryId=2;
            _context.
            Setup( ctx => ctx.GetBySelectedIdAsync(_categoryId))
            .Returns(async ( )=>{
                return await Task.Run(()=>{
                    return  _allCategories.AsQueryable().Where(a => a.CategoryId == _categoryId).FirstOrDefault();
                });
            });


            _newCategory = new Category(){
                CategoryId = 5,
                Name = "a",
                Description = "description"
            };
            _context.
            Setup(ctx =>ctx.AddAsync(_newCategory))
            .Returns(async ()=>{
                return await Task.Run(()=>{
                    return _newCategory;
                });
            });
           
            _context.
            Setup(ctx => ctx.UpdateAsync(_existingCategory) )
            .Returns(async ()=>{
                var result =await Task.Run(()=>{
                    return _existingCategory;
                });
                return result;
            });

            _deletedCategory = _allCategories[1];
            _context.
            Setup(ctx => ctx.DeleteAsync(_deletedCategory.CategoryId))
            .Returns(async ()=>{
                return await Task.Run(()=>{
                    return  _deletedCategory;
                });                
            });

            _categoryService = new CategoryService(_context.Object);
        }

        [Test]
        public void GetAllCategories_WhenCalled_ReturnsAllCategoriesActiveAndInactive()
        {
           //Arrange
           //Act
           var result = _categoryService.GetAllCategories();
           //Assert
           Assert.That(result.Count(), Is.EqualTo(3));
           _context.Verify((c)=>c.GetAll(), Times.Once);
        }

        [Test]
        public void GetActiveCategories_WhenCalled_ReturnsActiveCategoriesOnly()
        {
            //Arrange
            //Act
            var result = _categoryService.GetActiveCategories();
            //Assert            
            Assert.That(result.Count(), Is.EqualTo(2));
            _context.Verify((c)=>c.Get( a =>a.IsInactive == false),Times.Once);
        }

        [Test]
        public async Task GetSingleCategoryByIdAsync_WhenCalled_ReturnsExistingCategory()
        {
            //Arrange
            _categoryId = 2;
            //Act
            var result =await _categoryService.GetSingleCategoryByIdAsync(_categoryId);

            //Assert        
            _context.Verify(c=>c.GetBySelectedIdAsync(_categoryId), Times.Once);
        }

        [Test]
        public async Task CreateCategoryAsync_WhenCalled_ReturnsANewCategory()
        {
            //Arrange
            //Act
            var result =await _categoryService.CreateCategoryAsync(_newCategory);
            //Assert
            _context.Verify(c =>c.AddAsync(_newCategory), Times.Once);
        }

        
        
        [Test]
        public async Task UpdateCategoryAsync_WhenCalled_SavesToDatabaseAndReturnsInteger1()
        {
            //Arrange             
             _existingCategory = _allCategories[1];
            _existingCategory.Name = "a_updatedCategoryName";
            _categoryId = _existingCategory.CategoryId;
            //Act
            var result = await _categoryService.UpdateCategoryAsync(_existingCategory);
            //Assert
            _context.Verify((c)=>c.UpdateAsync(_existingCategory), Times.Once);

        }
        [Test]
        public async Task UpdateCategoryAsync_CategoryIdDoesNotExtis_ReturnInteger0()
        {
            var category = new Category();
            category.CategoryId = 6;
            var result = await _categoryService.UpdateCategoryAsync(category);
            //Assert
            _context.Verify(c =>c.UpdateAsync(category), Times.Never); 
        }

        [Test]
        public async Task DeleteCategoryAsync_IfItemExists_ReturnsDeletedItem()
        {
            //_deletedCategory
            _deletedCategory = _allCategories[1];
            var id = _deletedCategory.CategoryId;
            var result = await _categoryService.DeleteCategoryAsync(id);
            _context.Verify(c =>c.DeleteAsync(id), Times.Once);
        }
    }
}