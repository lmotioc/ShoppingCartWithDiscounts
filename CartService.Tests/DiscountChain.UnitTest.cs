using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace CartService.Tests
{
    public class DiscountChainUnitTest
    {
        private Mock<IRepo> _repo;
        private ProductRepo _productRepo;
        public IDiscountChain _chain;

        public DiscountChainUnitTest()
        {
            _repo = new Mock<IRepo>();
            var fakeRepo = new FakeRepo();

            _repo.Setup(r => r.Products).Returns(fakeRepo.Products);
            _repo.Setup(r => r.DiscountRules).Returns(fakeRepo.DiscountRules);

            _productRepo = new ProductRepo(_repo.Object);
        }

        [Fact]
        public void DicountChain_YealdsEmptyList_IfNoDicounts()
        {
            var _chain = new DiscountChain(new DiscountRuleFactory(_repo.Object));
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

            var discounts = _chain.GetAppliedDiscounts(cartItems);

            Assert.Empty(discounts);
        }

        [Fact]
        public void DiscountChain_YealdsOneDiscountAppliedOnce_IfOneDicounts()
        {
            var _chain = new DiscountChain(new DiscountRuleFactory(_repo.Object));
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

            var discounts = _chain.GetAppliedDiscounts(cartItems);

            Assert.Single(discounts);
            Assert.Equal(1, discounts[0].Item1);
        }

        [Fact]
        public void DiscountChain_YealdsOneDiscountAppliedTwice_IfSameDicount()
        {
            var _chain = new DiscountChain(new DiscountRuleFactory(_repo.Object));
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

            var discounts = _chain.GetAppliedDiscounts(cartItems);

            Assert.NotEmpty(discounts);
            Assert.Single(discounts); 
            Assert.Equal(2, discounts[0].Item1);
        }

        [Fact]
        public void DiscountChain_YealdsTwoDiscounts_IfDiffrentDiscounts()
        {
            var _chain = new DiscountChain(new DiscountRuleFactory(_repo.Object));
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

            var discounts = _chain.GetAppliedDiscounts(cartItems);

            Assert.NotEmpty(discounts);
            Assert.Equal(2, discounts.Count()); 
            Assert.Equal(1, discounts[0].Item1);
            Assert.Equal(2, discounts[1].Item1);
        }
    }
}
