﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using Intex2.Controllers;
using Intex2.Models;
using Xunit;

namespace SportsStore.Tests
{

    public class OrderControllerTests
    {

        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange - create a mock repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - create an empty cart
            Cart cart = new Cart();
            // Arrange - create the order
            Order order = new Order();
            Customer customer= new Customer();
            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart, customer);

            // Act
            ViewResult? result = target.Checkout(order) as ViewResult;

            // Assert - check that the order hasn't been stored 
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            // Assert - check I am passing an invalid model to the view
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {

            // Arrange - create a mock order repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            Customer customer = new Customer();
            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart, customer);
            // Arrange - add an error to the model
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            ViewResult? result = target.Checkout(new Order()) as ViewResult;

            // Assert - check that the order hasn't been passed stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            // Assert - check that I am passing an invalid model to the view
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - create a mock order repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            Customer customer = new Customer();
            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart, customer);

            // Act - try to checkout
            RedirectToPageResult? result = target.Checkout(new Order()) as RedirectToPageResult;

            // Assert - check that the order has been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            // Assert - check the method is redirecting to the Completed action
            Assert.Equal("/Completed", result?.PageName);
        }
    }
}