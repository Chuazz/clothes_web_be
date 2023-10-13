using Cosmetics.Models;
using Cosmetics.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cosmetics.Services;

public class AuthService
{
    private readonly IMongoCollection<User> _UsersCollection;

    public AuthService(
        IOptions<CosmeticsDatabaseSetting> CosmeticsDatabaseSetting)
    {
        var mongoClient = new MongoClient(
            CosmeticsDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            CosmeticsDatabaseSetting.Value.DatabaseName);

        _UsersCollection = mongoDatabase.GetCollection<User>(
            CosmeticsDatabaseSetting.Value.UsersCollectionName);
    }

    public async Task<User?> Login(string account, string password)
    {
        return await _UsersCollection.Find(x => x.account == account && x.password == password).FirstOrDefaultAsync(); ;
    }

    public async Task CreateAccount(User User) =>
        await _UsersCollection.InsertOneAsync(User);

    public async Task UpdateAccount(string id, User updatedUser) =>
        await _UsersCollection.ReplaceOneAsync(x => x.id == id, updatedUser);
}