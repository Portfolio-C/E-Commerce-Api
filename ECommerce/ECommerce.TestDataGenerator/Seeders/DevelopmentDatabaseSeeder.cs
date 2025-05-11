using Bogus;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Interfaces;
using ECommerce.TestDataGenerator.Configurations;
using ECommerce.TestDataGenerator.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.TestDataGenerator.Seeders;

internal sealed class DevelopmentDatabaseSeeder() : IDatabaseSeeder
{
    private static readonly Faker _faker;

    static DevelopmentDatabaseSeeder()
    {
        _faker = new Faker();
        Randomizer.Seed = new Random(123);
    }

    public async Task SeedDatabaseAsync(IApplicationDbContext context, UserManager<IdentityUser> userManager, DataSeedSettings settings)
    {
        await CreateUsersAsync(context, userManager);
        await CreateCategoriesAsync(context);
        await CreateProductsAsync(context);
        await CreateOrderItemsAsync(context);
        await CreateOrdersAsync(context);
        await CreateBasketsAsync(context);
        await CreateFavoritesAsync(context);
    }

    private async Task CreateUsersAsync(IApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        if (context.ApplicationsUsers.Any())
        {
            return;
        }

        for (int i = 1; i <= 5; i++)
        {
            var user = new ApplicationUser
            {
                UserName = $"user-{i}",
                Email = $"user{i}@example.com",
                EmailConfirmed = true,
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Role = _faker.PickRandom<ApplicationUserRole>()
            };

            var reuslt = await userManager.CreateAsync(user, "Password123!");
            if (!reuslt.Succeeded)
            {
                throw new Exception($"Foydalanuvchi {user.UserName} ni yaratishda xato");
            }
        }
    }

    private async Task CreateCategoriesAsync(IApplicationDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        var mockCategories = new Faker<Category>()
        .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
        .RuleFor(c => c.Description, f => f.Lorem.Sentence(5, 10));

        var cateogires = mockCategories.Generate(10);

        await context.Categories.AddRangeAsync(cateogires);
        await context.SaveChangesAsync();
    }

    private async Task CreateProductsAsync(IApplicationDbContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        var categories = context.Categories.ToList();

        if (!categories.Any())
        {
            return;
        }

        var mockProducts = new Faker<Product>()
        .RuleFor(p => p.Name, f => f.Commerce.Product())
        .RuleFor(p => p.Price, f => f.Random.Decimal(5, 500))
        .RuleFor(p => p.Quantity, f => f.Random.Int(1, 100))
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id)
        .RuleFor(p => p.Category, f => f.PickRandom(categories));

        var products = mockProducts.Generate(30);

        await context.Products.AddRangeAsync(products);

        foreach (var category in categories)
        {
            category.Products = new HashSet<Product>(products.Where(p => p.CategoryId == category.Id));
        }

        await context.SaveChangesAsync();
    }

    private async Task CreateOrderItemsAsync(IApplicationDbContext context)
    {
        if (context.OrderItems.Any())
        {
            return;
        }

        var products = context.Products.ToList();

        if (!products.Any())
        {
            return;
        }

        var orderItemFaker = new Faker<OrderItem>()
        .RuleFor(oi => oi.ProductId, f => f.PickRandom(products).Id)
        .RuleFor(oi => oi.Product, f => f.PickRandom(products))
        .RuleFor(oi => oi.Quantity, f => f.Random.Int(1, 5))
        .RuleFor(oi => oi.Price, (f, oi) => oi.Product.Price);

        var orderItems = orderItemFaker.Generate(30);
        await context.OrderItems.AddRangeAsync(orderItems);
        await context.SaveChangesAsync();
    }

    private async Task CreateOrdersAsync(IApplicationDbContext context)
    {
        if (context.Orders.Any())
        {
            return;
        }

        var users = context.ApplicationsUsers.ToList();
        var orderItems = context.OrderItems.ToList();
        if (!users.Any() || !orderItems.Any())
        {
            return;
        }

        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
            .RuleFor(o => o.OrderDate, f => f.Date.Past(1))
            .RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>());

        var orders = orderFaker.Generate(10);

        foreach (var order in orders)
        {
            var itemCount = _faker.Random.Int(1, 3);
            var selectedItems = _faker.PickRandom(orderItems, itemCount).ToList();

            order.OrderItems = new HashSet<OrderItem>(selectedItems);
            order.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.Price);

            foreach (var item in order.OrderItems)
            {
                item.Order = order;
                item.OrderId = order.Id;
            }
        }

        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();
    }

    private async Task CreateFavoritesAsync(IApplicationDbContext context)
    {
        if (context.Favorites.Any())
        {
            return;
        }

        var products = context.Products.ToList();
        var users = context.ApplicationsUsers.ToList();
        if (!products.Any() || !users.Any())
        {
            return;
        }

        var favoriteFaker = new Faker<Favorite>()
            .RuleFor(f => f.UserId, f => f.PickRandom(users).Id)
            .RuleFor(f => f.ProductId, f => f.PickRandom(products).Id)
            .RuleFor(f => f.Product, f => f.PickRandom(products))
            .RuleFor(f => f.AddedDate, f => f.Date.Past(1));

        var favorites = favoriteFaker.Generate(15);
        await context.Favorites.AddRangeAsync(favorites);
        await context.SaveChangesAsync();
    }

    private async Task CreateBasketsAsync(IApplicationDbContext context)
    {
        if (context.Baskets.Any())
        {
            return;
        }

        var products = context.Products.ToList();
        var users = context.ApplicationsUsers.ToList();
        if (!products.Any() || !users.Any())
        {
            return;
        }

        var basketFaker = new Faker<Basket>()
            .RuleFor(b => b.UserId, f => f.PickRandom(users).Id)
            .RuleFor(b => b.ProductId, f => f.PickRandom(products).Id)
            .RuleFor(b => b.Product, f => f.PickRandom(products))
            .RuleFor(b => b.Quantity, f => f.Random.Int(1, 5))
            .RuleFor(b => b.AddedDate, f => f.Date.Past(1));

        var baskets = basketFaker.Generate(15);
        await context.Baskets.AddRangeAsync(baskets);
        await context.SaveChangesAsync();
    }
}
