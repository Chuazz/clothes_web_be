using Cosmetics.Models;
using Cosmetics.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cosmetics.Services;

public class ShopService
{
    private readonly IMongoCollection<Shop> _shopsCollection;

    public ShopService(
        IOptions<CosmeticsDatabaseSetting> CosmeticsDatabaseSetting)
    {
        var mongoClient = new MongoClient(
            CosmeticsDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            CosmeticsDatabaseSetting.Value.DatabaseName);

        _shopsCollection = mongoDatabase.GetCollection<Shop>(
            CosmeticsDatabaseSetting.Value.ShopsCollectionName);
    }

    public async Task<List<Shop>> GetAsync() =>
        await _shopsCollection.Find(_ => true).ToListAsync();

    public async Task<Shop?> GetAsync(string id) =>
        await _shopsCollection.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Shop Shop) =>
        await _shopsCollection.InsertOneAsync(Shop);

    public async Task UpdateAsync(string id, Shop updatedShop) =>
        await _shopsCollection.ReplaceOneAsync(x => x.id == id, updatedShop);

    public async Task RemoveAsync(string id) =>
        await _shopsCollection.DeleteOneAsync(x => x.id == id);
}