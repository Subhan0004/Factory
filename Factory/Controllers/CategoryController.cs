using Factory.Core.Domain.Abstract;
using Factory.Core.Domain.Entities;
using Factory.Helpers;
using Factory.Mappers;
using Factory.Models;
using Factory.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Controllers
{
    [Authorize(Roles = "A")]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork db, UserManager<User> userManager) : base(db, userManager) { }

        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            List<Category> categories = DB.CategoryRepository.Get();

            CategoryViewModel viewModel = new CategoryViewModel();

            CategoryMapper categoryMapper = new CategoryMapper();

            foreach (var category in categories)
            {
                var categoryModel = categoryMapper.Map(category);
                viewModel.Categories.Add(categoryModel);
            }

            EnumerationUtil.Enumerate(viewModel.Categories);


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult SaveCategory(int id)
        {
            CategoryModel model = new CategoryModel();

            if (id != 0)
            {
                Category category = DB.CategoryRepository.Get(id);

                if (category == null)
                {
                    return Content("Current category not found!");
                }

                CategoryMapper mapper = new CategoryMapper();

                model = mapper.Map(category);

            }

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveCategory(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return Content("Model is invalid");
            }

            CategoryMapper mapper = new CategoryMapper();

            Category category = mapper.Map(categoryModel);

            category.Creator = CurrentUser;

            if (category.Id != 0)
            {
                DB.CategoryRepository.Update(category);
            }

            else
            {
                DB.CategoryRepository.Add(category);
            }

            TempData["Message"] = "Saved successfully";
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Delete(CategoryViewModel categoryViewModel)
        {
            Category category = DB.CategoryRepository.Get(categoryViewModel.DeletedId);

            if (category == null)
            {
                return Content("Current category not found!");
            }

            category.Creator = CurrentUser;

            category.LastModifiedDate = DateTime.Now;

            category.IsDeleted = true;

            DB.CategoryRepository.Update(category);

            return RedirectToAction("Index");
        }
    }
}
