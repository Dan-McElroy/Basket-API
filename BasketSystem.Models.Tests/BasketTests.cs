using System;
using Xunit;

namespace BasketSystem.Models.Tests
{
    public class BasketTests
    {
        private Basket Basket;

        public BasketTests()
        {
            Basket = new Basket();
        }

        [Fact]
        public void CanAddItemToBasket()
        {
            Basket.AddItem("test1", 1);
            Assert.Equal(1, Basket.Items.Count);
        }

        [Fact]
        public void CanAddMultipleItemsToBasket()
        {
            Basket.AddItem("test1", 1);
            Basket.AddItem("test2", 2);
            Basket.AddItem("test3", 3);
            Basket.AddItem("test4", 4);
            Assert.Equal(4, Basket.Items.Count);
        }

        [Fact]
        public void AddIncreasesExistingQuantity()
        {
            Basket.AddItem("test", 5);
            Basket.AddItem("test", 4);
            var item = Basket.FindById("test");
            Assert.Equal(9, item.Quantity);
        }

        [Fact]
        public void CanEditQuantityOfExistingItem()
        {
            Basket.AddItem("test", 5);
            Basket.EditItemQuantity("test", 3);
            var item = Basket.FindById("test");
            Assert.Equal(3, item.Quantity);
        }

        [Fact]
        public void EditingMissingItemThrows()
        {
            Basket.AddItem("test1", 1);
            Basket.AddItem("test2", 2);
            Assert.Throws<InvalidOperationException>(
                () => Basket.EditItemQuantity("test3", 3));
        }

        [Fact]
        public void EditingItemToZeroRemovesFromBasket()
        {
            Basket.AddItem("test", 1);
            Basket.EditItemQuantity("test", 0);
            Assert.Null(Basket.FindById("test"));
        }

        [Fact]
        public void CanRemoveItem()
        {
            Basket.AddItem("test", 1);
            Basket.RemoveItem("test");
            Assert.Null(Basket.FindById("test"));
        }

        [Fact]
        public void RemovingNonExistentElementDoesNotThrow()
        {
            Basket.AddItem("test1", 1);
            Basket.RemoveItem("test2");
        }

        [Fact]
        public void ClearItemsRemovesAll()
        {
            Basket.AddItem("test1", 1);
            Basket.AddItem("test2", 2);
            Basket.AddItem("test3", 3);
            Basket.AddItem("test4", 4);
            Basket.Clear();
            Assert.Empty(Basket.Items);
        }
    }
}