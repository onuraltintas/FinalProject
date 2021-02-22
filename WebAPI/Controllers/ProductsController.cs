using Bussiness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //=> c#'da attiribute denir. Java'da annotation denir.
    public class ProductsController : ControllerBase
    {
        //Dependency Chain - Bağımlılık zinciri
        //loosely coupled - gevşek bağımlılık (constructor injection yapıldığından
        //gevşek bağımlılık oldu.
        //naming convention - isimlendirme standardı (fieldlar _ ile başlatılır.
        //IoC Container -- Invertion of Control

        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
