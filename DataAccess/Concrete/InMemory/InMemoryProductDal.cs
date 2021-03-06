﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal:IProductDal
    {
        private List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product{CategoryId = 1,ProductId = 1,ProductName = "Bardak",UnitsInStock = 15,UnitPrice = 15},
                new Product{CategoryId = 1,ProductId = 2, ProductName = "Kamera",UnitPrice = 500,UnitsInStock = 3},
                new Product{CategoryId = 2,ProductId = 3, ProductName = "Telefon",UnitPrice = 1500,UnitsInStock = 2},
            new Product{CategoryId = 2,ProductId = 4, ProductName = "Klavye",UnitPrice = 150,UnitsInStock = 65},
            new Product{CategoryId = 2,ProductId = 5, ProductName = "Fare",UnitPrice = 85,UnitsInStock = 1}
            };
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
             
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p=>p.CategoryId==product.ProductId);

            if (productToUpdate != null)
            {
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.UnitsInStock = product.UnitsInStock;
                productToUpdate.UnitPrice = product.UnitPrice;
            }
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.SingleOrDefault(p=>p.CategoryId==product.ProductId);

            _products.Remove(productToDelete);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
