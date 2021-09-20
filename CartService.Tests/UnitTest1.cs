using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace CartService.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test2()
        {
            var cartService = new CustomerCart();
            var repo = new ProductRepo();

            var cartItems = new List<CartItem> () {
                new CartItem {
                    Product =  repo.GetByName("Bread"),
                    Quantity = 1
                },
                new CartItem {
                    Product =  repo.GetByName("Butter"),
                    Quantity = 1
                },
                new CartItem {
                    Product =  repo.GetByName("Milk"),
                    Quantity = 1
                }
            };

            cartService.Items = cartItems;

            Assert.Equal(2.95m, cartService.FullPrice);
            Assert.Equal(2.95m, cartService.TotalPrice);       

        }

        [Fact]
        public void Test3() {
            var cartService = new CustomerCart();
            var repo = new ProductRepo();

            var cartItems = new List<CartItem> () {
                new CartItem {
                    Product =  repo.GetByName("Bread"),
                    Quantity = 2
                },
                new CartItem {
                    Product =  repo.GetByName("Butter"),
                    Quantity = 2
                }
            };  

            cartService.Items = cartItems;

            Assert.Equal(3.6m, cartService.FullPrice);       
            Assert.Equal(3.1m, cartService.TotalPrice);       
        }

        [Fact]
        public void Test4() {
            var cartService = new CustomerCart();
            var repo = new ProductRepo();

            var cartItems = new List<CartItem> () {
                new CartItem {
                    Product =  repo.GetByName("Bread"),
                    Quantity = 4
                },
                new CartItem {
                    Product =  repo.GetByName("Butter"),
                    Quantity = 4
                }
            };  

            cartService.Items = cartItems;

            Assert.Equal(7.2m, cartService.FullPrice);       
            Assert.Equal(6.2m, cartService.TotalPrice);       
        }

        [Fact]
        public void Test5() {
            var cartService = new CustomerCart();
            var repo = new ProductRepo();

            var cartItems = new List<CartItem> () {
                new CartItem {
                    Product =  repo.GetByName("Milk"),
                    Quantity = 4
                }
            };  

            cartService.Items = cartItems;

            Assert.Equal(4.6m, cartService.FullPrice);       
            Assert.Equal(3.45m, cartService.TotalPrice);       
        }

        [Fact]
        public void Test6() {
            var cartService = new CustomerCart();
            var repo = new ProductRepo();

            var cartItems = new List<CartItem> () {
                new CartItem {
                    Product =  repo.GetByName("Butter"),
                    Quantity = 2
                },
                new CartItem {
                    Product =  repo.GetByName("Bread"),
                    Quantity = 1
                },
                new CartItem {
                    Product =  repo.GetByName("Milk"),
                    Quantity = 8
                }
            };  

            cartService.Items = cartItems;

            Assert.Equal(11.8m, cartService.FullPrice);       
            Assert.Equal(9m, cartService.TotalPrice);       
        }
    }
}
