namespace MenuAPI.Interface;
using MenuAPI.Models;
public interface IFoodTypeService {
    public IEnumerable<FoodType> GetFoodTypes();
};
