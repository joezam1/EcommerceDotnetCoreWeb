using System.Threading.Tasks;
using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Project.Shop.BusinessLogic.DataServices;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.BusinessLogic.ViewModels;
using Project.Shop.DataAccess.Interfaces;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.DataAccess.Repositories;
using Newtonsoft.Json;

namespace Project.Shop.UnitTests.BusinessLogic.UnitTests.DataServices
{
    [TestFixture]
    public class ShopServiceTests
    {
        private  Mock<IJsonHelper> _jsonHelper;
        private  Mock<IShopRepository> _shopRepository;
        private IShopService _shopService;
        private ShopComponentsModel _shopModel;

        [SetUp]
        public void SetUp()
        {           
            _shopRepository = new Mock<IShopRepository>();
            _jsonHelper = new Mock<IJsonHelper>();


            List<Product> products = new List<Product>() {
                new Product(){ProductId=1,Name="Product_a", Price=10, IsInactive=false},
                new Product(){ProductId=2,Name="Product_b", Price=10, IsInactive=false},
                new Product(){ProductId=3,Name="Product_c", Price=10, IsInactive=false},
                new Product(){ProductId=4,Name="Product_d", Price=10, IsInactive=false},
            };
            List<Category> categories = new List<Category>(){
                new Category(){CategoryId=1, Name="Category_a",IsInactive=false},
                new Category(){CategoryId=2, Name="Category_b",IsInactive=false},
                new Category(){CategoryId=3, Name="Category_c",IsInactive=false},
                new Category(){CategoryId=4, Name="Category_d",IsInactive=false},
            };
            List<Status> statuses =new List<Status>(){
                new Status(){StatusId=1,Name="Status_a",IsInactive=false},
                new Status(){StatusId=2,Name="Status_b",IsInactive=false},
                new Status(){StatusId=3,Name="Status_c",IsInactive=false},
                new Status(){StatusId=4,Name="Status_d",IsInactive=false},
            };
            List<Size> sizes = new List<Size>(){
                new Size(){SizeId=1, Description="size_a", IsInactive=false},
                new Size(){SizeId=2, Description="size_a", IsInactive=false},
                new Size(){SizeId=3, Description="size_a", IsInactive=false},
                new Size(){SizeId=4, Description="size_a", IsInactive=false},
            };
            
            List<object> allItems = new List<object>(){products,categories,statuses,sizes};
            
            _shopModel = new ShopComponentsModel(){
                Products = products,
                Categories = categories,
                Statuses = statuses,
                Sizes = sizes,

            };
            
            _shopRepository
            .Setup(c=>c.GetComponentsAsync())
            .Returns(async()=>{
                return await Task.Run(()=>{
                    return allItems;
                });
            });
           
            _shopService = new ShopService(_shopRepository.Object,_jsonHelper.Object);
        }

        [Test]
        public void GetAllStoreComponentsAsync_WhenCalled_ShopRepositoryIsCalledOnce()
        {
            //act
            var result = _shopService.GetAllStoreComponentsAsync();
            //assert
            _shopRepository.Verify(sr=>sr.GetComponentsAsync(),Times.Once);
        }       
    }
}