using Dapper;
using Lesson_DotNet_Dapper.Entities;
using Lesson_DotNet_Dapper_Advanced.Entities;
using System.Data;
using static Lesson_DotNet_Dapper_Advanced.Entities.Order;

namespace Lesson_DotNet_Dapper_Advanced.Repositories;

internal class OrderReporitory
{
    private readonly IDbConnection _dbConnection;

    public OrderReporitory(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Order>> GetAsync()
    {
        var queryCommand = @"
            SELECT
                pedido.PedidoID AS OrderId,
                pedido.Pago AS Paid,
	            pedido.DataCriacao AS CreateDate,
	            item.PedidoID AS OrderId,
	            item.PedidoItemID AS ItemId,
	            item.Item AS Name,
	            item.ValorUnitario AS Value,
	            item.Quantidade AS Quantity
            FROM dbo.tblPedido pedido
            INNER JOIN dbo.tblPedidoItem item on item.PedidoID = pedido.PedidoID";

        var ordersDictionary = new Dictionary<int, Order>();

        await _dbConnection.QueryAsync<Order, Item, Order>(
            queryCommand,
            (order, item) => 
            {
                Order? orderEntry;

                if (!ordersDictionary.TryGetValue(order.OrderId, out orderEntry))
                {
                    orderEntry = order;

                    orderEntry.Items = orderEntry.Items ?? new List<Item>();

                    ordersDictionary.Add(orderEntry.OrderId, orderEntry);
                }

                orderEntry.Items?.Add(item);

                return orderEntry;
            },
            splitOn: "OrderId"
            );

        return ordersDictionary.Values;
    }
}
