using Api.Controllers;
using Api.V1.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers
{
    [Authorize(Roles = "RegularUsers")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoriesController : MainController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController( INotificator notificator, IUser appUser, 
            ICategoryService categoryService, 
            IMapper mapper ) : base(notificator, appUser)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost("create-category")]
        public async Task<ActionResult> CreateCategory(CategoryViewModel newCategoryViewModel)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            newCategoryViewModel.CategoryCreatedByUserId = UserId;
            newCategoryViewModel.CreatedAt = DateTime.Now;

            await _categoryService.Add(_mapper.Map<Category>(newCategoryViewModel));

            return CustomResponse(newCategoryViewModel);
        }

        [HttpGet("list")]
        public async Task<IEnumerable<CategoryViewModel>> ListCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await GetCategoriesByUserId());
        }

        [HttpGet("revenue-categories")]
        public async Task<IEnumerable<CategoryViewModel>> ListCategoriesByRevenueType()
        {
            var result = _mapper.Map<IEnumerable<CategoryViewModel>>(await GetCategoriesByUserId());

            return result.Where(t => t.TransactionType == 0);
        }

        [HttpGet("expense-categories")]
        public async Task<IEnumerable<CategoryViewModel>> ListCategoriesByExpenseType()
        {
            var result = _mapper.Map<IEnumerable<CategoryViewModel>>(await GetCategoriesByUserId());

            return result.Where(t => t.TransactionType == 1);
        }

        private async Task<IEnumerable<Category>> GetCategoriesByUserId()
        {
            return await _categoryService.GetCategoriesByUserId(UserId);
        }
    }
}
