namespace MenuAPI.Interface;
using MenuAPI.Models;
public interface IFoodItemService {
    public IEnumerable<FoodItem> GetFoodItems();

};
