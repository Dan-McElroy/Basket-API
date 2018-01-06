using System;

namespace BasketSystem.Models
{
    /// <summary>
    /// A representation of the items in a <see cref="Basket"/>,
    /// and their quantities.
    /// </summary>
    public class BasketItem
    {
        #region Private Fields

        private string _id;
        private int _quantity;

        #endregion

        #region Properties

        /// <summary>
        /// Unique ID of the basket item.
        /// </summary>
        /// <remarks>
        /// The ID cannot be null.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Throws if set to null.
        /// </exception>
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value
                   ?? throw new ArgumentNullException(nameof(value),
                        "Id cannot be null.");
            }
        }

        /// <summary>
        /// Quantity of this item in the basket.
        /// </summary>
        /// <remarks>Quantity must be greater than 0.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if set to a value less than 1.
        /// </exception>
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value > 0
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        nameof(value), value,
                        "Quantity must be greater than 0.");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Basic constructor for a BasketItem.
        /// </summary>
        /// <param name="id">ID of the item, which should not be null.</param>
        /// <param name="quantity">
        /// Quantity of the item, which should not be less than 1.
        /// </param>
        public BasketItem(string id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        #endregion
    }
}