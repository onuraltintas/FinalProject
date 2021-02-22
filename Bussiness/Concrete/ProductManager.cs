using System;
using System.Collections.Generic;
using System.Linq;
using Bussiness.Abstract;
using Bussiness.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Bussiness.Concrete
{
    public class ProductManager:IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IResult Add(Product product)
        {
            if (product.ProductName.Length<2)
            {
                //magic string
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new Result(true,"Ürün Güncellendi...");
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new Result(true,"Ürün Silindi...");
        }

        public IDataResult<List<Product>> GetAll()
        {
           // iş kodları
            return new DataResult<List<Product>>(_productDal.GetAll(),true,Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id).ToList());
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
