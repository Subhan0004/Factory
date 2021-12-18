using Factory.Core.Domain.Entities;
using Factory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Mappers
{
    public class ProductMapper : BaseMapper<Product, ProductModel>
    {
        public override Product Map(ProductModel productModel)
        {
            if (productModel == null)
                return null;

            CategoryMapper categoryMapper = new CategoryMapper();

            Product product = new Product()
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Price = productModel.Price,
                Quantity = productModel.Quantity,
                Note = productModel.Note
            };

            product.Category = categoryMapper.Map(productModel.Category);

            return product;
        }

        public override ProductModel Map(Product product)
        {
            if (product == null)
                return null;

            CategoryMapper categoryMapper = new CategoryMapper();

            ProductModel productModel = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Note = product.Note
            };

            productModel.Category = categoryMapper.Map(product.Category);

            return productModel;
        }
    }
}
