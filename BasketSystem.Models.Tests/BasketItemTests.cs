using System;
using Xunit;

namespace BasketSystem.Models.Tests
{
    public class BasketItemTests
    {
        [Fact]
        public void BasketItemNameCannotBeNullInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new BasketItem(null, 1));
        }

        [Fact]
        public void BasketItemNameCannotBeSetToNull()
        {
            var item = new BasketItem("Test Item", 1);
            Assert.Throws<ArgumentNullException>(() => item.Id = null);
        }

        [Fact]
        public void BasketQuantityCannotBeBelow1InConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new BasketItem(null, 0));
        }

        [Fact]
        public void BasketQuantityCannotBeSetBelow1()
        {
            var item = new BasketItem("Test Item", 1);
            Assert.Throws<ArgumentOutOfRangeException>(() => item.Quantity = -1);
        }
    }
}