﻿namespace Application.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Size { get; set; }
        public double Price { get; set; }
        public double Total
        {
            get { return Quantity * Price; }
        }
        public string Image { get; set; }
        public CartItem()
        {
        }
        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Price = product.Price;
            Quantity = 1;
            Size = product.Size;
            Image = product.Image;
        }
    }
}
