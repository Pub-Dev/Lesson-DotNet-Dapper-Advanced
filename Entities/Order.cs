using Lesson_DotNet_Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_DotNet_Dapper_Advanced.Entities;

internal class Order
{
    public int OrderId { get; set; }
    public bool Paid { get; set; }
    public IList<Item>? Items { get; set; } = new List<Item>();
    public decimal? Total => Items?.Sum(x => x.Value * x.Quantity);

    public override string ToString()
    {
        return $@"
OrderId = {OrderId}
Paid = {(Paid ? "PAID $$" : "NOT PAID ;(")}
Total = {Total:C2}
Items = [{(Items is not null ? string.Join(",\n", Items) : "No Items ;(")}]";
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Value * Quantity;

        public override string ToString()
        {
            return $@"{{
    ItemId = {ItemId}
    Name = {Name}
    Value = {Value:C2}
    Quantity = {Quantity}    
    Total = {Total:C2}
}}";
        }
    }
}
