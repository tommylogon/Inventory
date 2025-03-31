using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Model;

namespace Inventory.Services
{
    public interface IInventoryService
    {
        Task<List<Food>> GetAllFoodAsync();
        Task<List<Food>> GetExpiringFoodAsync(int daysThreshold = 7);
        Task<List<Food>> GetExpiredFoodAsync();
        Task<List<Food>> GetFoodByLocationAsync(string location);
        Task<Food> GetFoodByBarcodeAsync(string barcode);
        Task<bool> AddFoodAsync(Food food);
        Task<bool> UpdateFoodAsync(Food food);
        Task<bool> DeleteFoodAsync(string barcode);
        Task<List<string>> GetAllLocationsAsync();
        Task<List<string>> GetAllCategoriesAsync();
    }
} 