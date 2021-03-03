using System.Collections.Generic;
using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Bussiness.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }

        public IResult Add(Category category)
        {
           _categoryDal.Add(category);
           return new Result(true);
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new Result(true);
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new Result(true);
        }
    }
}
