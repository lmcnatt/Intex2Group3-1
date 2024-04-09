﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Product prod, int quantity)
        {
            CartLine? line = Lines
              .Where(x => x.Product.ProductId == prod.ProductId)
              .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = prod,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product prod) => Lines.RemoveAll(x => x.Product.ProductId == prod.ProductId);

        public virtual void Clear() => Lines.Clear();

        public decimal CalculateTotal() => Lines.Sum(x => x.Product.Price * x.Quantity);

        [PrimaryKey(nameof(OrderId), nameof(ProductId))]
        public class CartLine
        {
            public Order Order {get; set;}
            public int OrderId { get; set; }
            public Product Product { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public int Rating { get; set; }
        }
    }
}
