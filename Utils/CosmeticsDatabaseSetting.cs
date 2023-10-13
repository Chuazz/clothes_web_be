namespace Cosmetics.Utils;

public class CosmeticsDatabaseSetting
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UsersCollectionName { get; set; } = null!;

    public string ProductsCollectionName { get; set; } = null!;

    public string ShopsCollectionName { get; set; } = null!;

    public string ProductCategoriesCollectionName { get; set; } = null!;

    public string OrdersCollectionName { get; set; } = null!;
}