using Ecommerce.Repositories;
using ECommerceManagement.Models;

namespace Ecommerce.Services;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)

    {

        _categoryRepository = categoryRepository;

    }
    
    public async Task<int> AddAsync(Category category)

    {
        ValidateCategoryName(category.CategoryName);
        category.CategoryName = category.CategoryName.Trim();
        category.IsActive = true;
        return await _categoryRepository.AddAsync(category);
    }
    
    
    private static void ValidateCategoryName(string? categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            throw new ArgumentException(
                "Category name cannot be empty.");
        }
        if (categoryName.Trim().Length > 100)
        {
            throw new ArgumentException(
                "Category name cannot exceed 100 characters.");
        }
    }
    
    
}