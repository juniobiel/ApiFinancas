using Api.Controllers;
using Api.V2.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V2.Controllers
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
        public async Task<ActionResult> CreateCategory( CategoryViewModel newCategoryViewModel )
        {
            if (!UserAuthenticated)
                return BadRequest("Efetue o login novamente!");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoryService.Add(_mapper.Map<Category>(newCategoryViewModel));

            return CustomResponse(newCategoryViewModel);
        }

        [HttpGet("list")]
        public async Task<IEnumerable<CategoryViewModel>> ListCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetCategories());
        }

        [HttpGet("revenue-categories")]
        public async Task<IEnumerable<CategoryViewModel>> ListCategoriesByRevenueType()
        {
            var result = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetCategories());

            return result.Where(t => t.TransactionType == 0);
        }

        [HttpGet("expense-categories")]
        public async Task<IEnumerable<CategoryViewModel>> ListCategoriesByExpenseType()
        {
            var result = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetCategories());

            return result.Where(t => t.TransactionType == 1);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> EditCategory( CategoryViewModel categoryEditViewModel )
        {
            var category = await _categoryService.GetCategoryById((int)categoryEditViewModel.CategoryId);

            if (!ModelState.IsValid) return CustomResponse(categoryEditViewModel);

            if (category == null)
            {
                return NotFound("Não é uma categoria válida!");
            }

            if (category.TransactionType != (TransactionType)categoryEditViewModel.TransactionType)
            {
                return BadRequest("Não é possível alterar o tipo de transação vinculado a esta categoria");
            }

            await _categoryService.Update(_mapper.Map<Category>(categoryEditViewModel));

            return StatusCode(200, new
            {
                message = "Categoria atualizada com sucesso!",
                categoryEditViewModel
            });
        }

        [HttpDelete("delete/{categoryId:int}")]
        public async Task<ActionResult> DeleteCategory( int categoryId )
        {
            var category = await _categoryService.GetCategoryById(categoryId);

            if (category == null) return NotFound("Não foi possível excluir a categoria selecionada");

            await _categoryService.Remove(categoryId);
            return StatusCode(202, "A categoria foi excluída!");
        }
    }
}
