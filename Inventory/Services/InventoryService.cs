using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Model;
using Serilog;

namespace Inventory.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ILogger _logger;

        public InventoryService()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.File("logs/inventory.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public async Task<List<Food>> GetAllFoodAsync()
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    return await context.Foods.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all food items");
                throw;
            }
        }

        public async Task<List<Food>> GetExpiringFoodAsync(int daysThreshold = 7)
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    var date = DateTime.Now.AddDays(daysThreshold);
                    return await context.Foods
                        .Where(f => f.ExpirationDate <= date && f.ExpirationDate > DateTime.Now)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting expiring food items");
                throw;
            }
        }

        public async Task<List<Food>> GetExpiredFoodAsync()
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    return await context.Foods
                        .Where(f => f.ExpirationDate < DateTime.Now)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting expired food items");
                throw;
            }
        }

        public async Task<List<Food>> GetFoodByLocationAsync(string location)
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    return await context.Foods
                        .Where(f => f.Location == location)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting food items from location: {location}");
                throw;
            }
        }

        public async Task<Food> GetFoodByBarcodeAsync(string barcode)
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    return await context.Foods
                        .FirstOrDefaultAsync(f => f.Barcode == barcode);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting food item with barcode: {barcode}");
                throw;
            }
        }

        public async Task<bool> AddFoodAsync(Food food)
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    context.Foods.Add(food);
                    await context.SaveChangesAsync();
                    _logger.Information($"Added new food item: {food.Name} ({food.Barcode})");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error adding food item: {food.Name} ({food.Barcode})");
                return false;
            }
        }

        public async Task<bool> UpdateFoodAsync(Food food)
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    var existingFood = await context.Foods.FirstOrDefaultAsync(f => f.Barcode == food.Barcode);
                    if (existingFood == null) return false;

                    context.Entry(existingFood).CurrentValues.SetValues(food);
                    food.ModifiedDate = DateTime.Now;
                    await context.SaveChangesAsync();
                    _logger.Information($"Updated food item: {food.Name} ({food.Barcode})");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error updating food item: {food.Name} ({food.Barcode})");
                return false;
            }
        }

        public async Task<bool> DeleteFoodAsync(string barcode)
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    var food = await context.Foods.FirstOrDefaultAsync(f => f.Barcode == barcode);
                    if (food == null) return false;

                    context.Foods.Remove(food);
                    await context.SaveChangesAsync();
                    _logger.Information($"Deleted food item: {food.Name} ({food.Barcode})");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error deleting food item with barcode: {barcode}");
                return false;
            }
        }

        public async Task<List<string>> GetAllLocationsAsync()
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    return await context.Foods
                        .Select(f => f.Location)
                        .Distinct()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all locations");
                throw;
            }
        }

        public async Task<List<string>> GetAllCategoriesAsync()
        {
            try
            {
                using (var context = new InventoryDbContext())
                {
                    return await context.Foods
                        .Select(f => f.Category)
                        .Distinct()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all categories");
                throw;
            }
        }
    }
} 