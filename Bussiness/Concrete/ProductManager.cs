using System.Collections.Generic;
using System.Linq;
using Bussiness.Abstract;
using Bussiness.Constants;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;


namespace Bussiness.Concrete
{
    public class ProductManager:IProductService
    {
        IProductDal _productDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
           IResult result= BusinessRules.Run(CheckIfProductCountOfCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());

           if (result!=null)
           {
               return result;
           }

           _productDal.Add(product);
           return new SuccessResult(Messages.ProductAdded);
           
        }
        
        [ValidationAspect(typeof(ProductValidator))]
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

        private IResult CheckIfProductCountOfCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result>=15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductSameName);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CheckIfCategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
