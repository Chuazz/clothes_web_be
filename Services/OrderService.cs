using Cosmetics.Models;
using Cosmetics.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cosmetics.Services;

public class OrderService
{
    private readonly IMongoCollection<Order> _ordersCollection;

    public OrderService(
        IOptions<CosmeticsDatabaseSetting> CosmeticsDatabaseSetting)
    {
        var mongoClient = new MongoClient(
            CosmeticsDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            CosmeticsDatabaseSetting.Value.DatabaseName);

        _ordersCollection = mongoDatabase.GetCollection<Order>(
            CosmeticsDatabaseSetting.Value.OrdersCollectionName);
    }

    public async Task<List<Order>> GetAsync() =>
        await _ordersCollection.Find(_ => true).ToListAsync();

    public async Task<List<Order>> GetByUserId(string user_id)
    {
        return await _ordersCollection.Find(x => x.user!.id == user_id).ToListAsync();
    }

    public async Task<Order?> GetAsync(string id) =>
        await _ordersCollection.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Order Order) =>
        await _ordersCollection.InsertOneAsync(Order);

    public async Task UpdateAsync(string id, Order updatedOrder) =>
        await _ordersCollection.ReplaceOneAsync(x => x.id == id, updatedOrder);

    public async Task RemoveAsync(string id) =>
        await _ordersCollection.DeleteOneAsync(x => x.id == id);
}