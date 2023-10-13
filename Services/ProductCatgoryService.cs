using Cosmetics.Models;
using Cosmetics.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cosmetics.Services;

public class ProductCategoryService
{
    private readonly IMongoCollection<ProductCategory> _productCategoriesCollection;

    public ProductCategoryService(
        IOptions<CosmeticsDatabaseSetting> CosmeticsDatabaseSetting)
    {
        var mongoClient = new MongoClient(
            CosmeticsDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            CosmeticsDatabaseSetting.Value.DatabaseName);

        _productCategoriesCollection = mongoDatabase.GetCollection<ProductCategory>(
            CosmeticsDatabaseSetting.Value.ProductCategoriesCollectionName);
    }

    public async Task<List<ProductCategory>> GetAsync() =>
        await _productCategoriesCollection.Find(_ => true).ToListAsync();

    public async Task<ProductCategory?> GetAsync(string id) =>
        await _productCategoriesCollection.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ProductCategory productCategory) =>
        await _productCategoriesCollection.InsertOneAsync(productCategory);

    public async Task UpdateAsync(string id, ProductCategory updatedProduct) =>
        await _productCategoriesCollection.ReplaceOneAsync(x => x.id == id, updatedProduct);

    public async Task RemoveAsync(string id) =>
        await _productCategoriesCollection.DeleteOneAsync(x => x.id == id);
}