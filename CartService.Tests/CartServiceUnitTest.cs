using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace CartService.Tests
{
    public class CartServiceUnitTest
    {
        private Mock<IRepo> _repo;
        private ProductRepo _productRepo;
        private CustomerCart _cartService;

        public CartServiceUnitTest()
        {
            _repo = new Mock<IRepo>();
            var fakeRepo = new FakeRepo();

            _repo.Setup(r => r.Products).Returns(fakeRepo.Products);
            _repo.Setup(r => r.DiscountRules).Returns(fakeRepo.DiscountRules);

            _productRepo = new ProductRepo(_repo.Object);

            _cartService = new CustomerCart(_repo.Object);
        }

        [Fact]
        public void CartService_YealdsFullPriceEqualWithTotalPrice_IfNoDicounts()
        {
            var cartItems = new List<CartItem>() {
                new CartItem {
                    Product =  _productRepo.GetByName("Bread"),
                    Quantity = 1
                },
                new CartItem {
                    Product =  _productRepo.GetByName("Butter"),
                    Quantity = 1
                },
                new CartItem {
                    Product =  _productRepo.GetByName("Milk"),
                    Quantity = 1
                }
            };

            _cartService.Items = cartItems;

            Assert.Equal(2.95m, _cartService.FullPrice);
            Assert.Equal(2.95m, _cartService.TotalPrice);
        }

        [Fact]
        public void CartService_YealdsPrice_IfOneDiecount()
        {
            var cartItems = new List<CartItem>() {
                new CartItem {
                    Product =  _productRepo.GetByName("Bread"),
                    Quantity = 2
                },
                new CartItem {
                    Product =  _productRepo.GetByName("Butter"),
                    Quantity = 2
                }
            };

            _cartService.Items = cartItems;

            Assert.Equal(3.6m, _cartService.FullPrice);
            Assert.Equal(3.1m, _cartService.TotalPrice);
        }

        [Fact]
        public void CartService_YealdsPrice_IfDiecountAppliesTwice()
        {
            var cartItems = new List<CartItem>() {
                new CartItem {
                    Product =  _productRepo.GetByName("Bread"),
                    Quantity = 4
                },
                new CartItem {
                    Product =  _productRepo.GetByName("Butter"),
                    Quantity = 4
                }
            };

            _cartService.Items = cartItems;

            Assert.Equal(7.2m, _cartService.FullPrice);
            Assert.Equal(6.2m, _cartService.TotalPrice);
        }

        [Fact]
        public void CartService_YealdsPrice_IfOneDiscount()
        {
            var cartItems = new List<CartItem>() {
                new CartItem {
                    Product =  _productRepo.GetByName("Milk"),
                    Quantity = 4
                }
            };

            _cartService.Items = cartItems;

            Assert.Equal(4.6m, _cartService.FullPrice);
            Assert.Equal(3.45m, _cartService.TotalPrice);
        }

        [Fact]
        public void CartService_YealdsPrice_IfMultipleDiscounts()
        {
            var cartItems = new List<CartItem>() {
                new CartItem {
                    Product =  _productRepo.GetByName("Butter"),
                    Quantity = 2
                },
                new CartItem {
                    Product =  _productRepo.GetByName("Bread"),
                    Quantity = 1
                },
                new CartItem {
                    Product =  _productRepo.GetByName("Milk"),
                    Quantity = 8
                }
            };

            _cartService.Items = cartItems;

            Assert.Equal(11.8m, _cartService.FullPrice);
            Assert.Equal(9m, _cartService.TotalPrice);
        }
    }
}
