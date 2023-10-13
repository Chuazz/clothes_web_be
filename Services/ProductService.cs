using Cosmetics.Models;
using Cosmetics.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cosmetics.Services;

public class ProductService
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductService(
        IOptions<CosmeticsDatabaseSetting> CosmeticsDatabaseSetting)
    {
        var mongoClient = new MongoClient(
            CosmeticsDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            CosmeticsDatabaseSetting.Value.DatabaseName);

        _productsCollection = mongoDatabase.GetCollection<Product>(
            CosmeticsDatabaseSetting.Value.ProductsCollectionName);
    }

    public async Task<List<Product>> GetAsync() =>
        await _productsCollection.Find(_ => true).ToListAsync();

    public async Task<List<Product>> GetAsync(string keyword, string[] categories_code, int min_price, int max_price, string[] locations)
    {
        FilterDefinition<Product> filters =
            Builders<Product>.Filter.Lte(t => t.price, max_price != 0 ? max_price : 100000000) &
            Builders<Product>.Filter.Gte(t => t.price, min_price != 0 ? min_price : 0);

        if (categories_code.Length > 0)
        {
            filters &= Builders<Product>.Filter.In("product_category._id", categories_code);
        }

        if (locations.Length > 0)
        {
            filters &= Builders<Product>.Filter.In("shop.location_code", locations);
        }

        if (keyword != string.Empty)
        {
            filters &= Builders<Product>.Filter.Regex("name", new MongoDB.Bson.BsonRegularExpression(keyword, "i"));
        }

        return await _productsCollection.Aggregate().Match(filters).ToListAsync();
    }


    public async Task<Product?> GetAsync(string id) =>
        await _productsCollection.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Product product) =>
        await _productsCollection.InsertOneAsync(product);

    public async Task UpdateAsync(string id, Product updatedProduct)
    {
        await _productsCollection.ReplaceOneAsync(x => x.id == id, updatedProduct);
    }

    public async Task UpdateInStock(string id, int qty)
    {
        await _productsCollection.UpdateOneAsync(x => x.id == id, Builders<Product>.Update.Set("in_stock", qty));
    }

    public async Task RemoveAsync(string id) =>
        await _productsCollection.DeleteOneAsync(x => x.id == id);
}