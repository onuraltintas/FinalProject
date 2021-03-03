using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Bussiness.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        IDataResult<Category> GetById(int categoryId);
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);

    }
}
