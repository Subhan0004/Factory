using Factory.Core.Domain.Abstract;
using Factory.Core.Domain.Entities;
using Factory.Helpers;
using Factory.Mappers;
using Factory.Models;
using Factory.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork db, UserManager<User> userManager) : base(db, userManager) { }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            List<Product> products = DB.ProductRepository.Get();
            ProductViewModel viewModel = new ProductViewModel();

            ProductMapper mapper = new ProductMapper();

            foreach (var product in products)
            {
                ProductModel productModel = mapper.Map(product);
                viewModel.Products.Add(productModel);
            }

            EnumerationUtil.Enumerate(viewModel.Products);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult SaveProduct(int id)
        {
            ProductModel model = new ProductModel();

            if (id != 0)
            {
                Product product = DB.ProductRepository.FindById(id);

                if (product == null)
                    Content("Product not found!");

                ProductMapper mapper = new ProductMapper();

                model = mapper.Map(product);
            }

            List<Category> categories = DB.CategoryRepository.Get();

            List<SelectListItem> categoryModels = new List<SelectListItem>();

            foreach (var category in categories)
            {
                categoryModels.Add(new SelectListItem(category.Name, category.Id.ToString()));

            }

            model.Categories = categoryModels;

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
   .Where(x => x.Value.Errors.Count > 0)
   .Select(x => new { x.Key, x.Value.Errors })
   .ToArray();


                //return Content("Model is invalid");
            }

            ProductMapper mapper = new ProductMapper();

            Product product = mapper.Map(productModel);

            product.Creator = CurrentUser;

            if (product.Id != 0)
            {
                DB.ProductRepository.Update(product);
            }
            else
            {
                DB.ProductRepository.Add(product);
            }


            TempData["Message"] = "Saved successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(ProductViewModel viewModel)
        {
            Product product = DB.ProductRepository.FindById(viewModel.DeletedId);

            if (product == null)
                Content("Current meal not found");


            product.Creator = CurrentUser;

            product.LastModifiedDate = DateTime.Now;

            product.IsDeleted = true;

            DB.ProductRepository.Update(product);

            return RedirectToAction("Index");

        }
    }
}
