using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UdemyWEBAPI.Data;
using UdemyWEBAPI.Interfaces;

namespace UdemyWEBAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]

    #region Acıklama
    
    /* Yapmak istedigimiz sey Rest mimarisine uygun endpointler olusturmak
    
     
        /api/products   --> dedigimizde ilk Action ile tum product'lar json formatında gelsin
                            // sadece api/products ile veri ekleme,guncelleme,getirme,silme islemlerini yapabiliyoruz..REST bize bunları saglıyor.
        
        /api/products/2   --> dedigizde [HttpGet ("{id}")] ile id parametresi bildirdigimizde api/product/2 dedigimde 2 id li product gelecek...
     
     */
    #endregion
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productRepository.GetAllAsync();

            return Ok(result);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
           var data = await _productRepository.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound($"girdiginiz {id} degerine ye uygun bir ürün bulunamadı");
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(Product product)
        {

            var addedProduct = await _productRepository.CreateProductAsync(product);

            return Created(string.Empty, addedProduct);  
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {

            var checkProduct = await _productRepository.GetByIdAsync(product.ID);
            if(checkProduct == null)
                 return NotFound($"{product.ID} ile eslesen herhangi bir kayıt bulunamadı..");

            await _productRepository.UpdateProductAsync(product);

            return NoContent();
             
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var checkProduct = await _productRepository.GetByIdAsync(id);
            if (checkProduct == null)
                return NotFound();
            
            await _productRepository.RemoveProductAsync(id);
            
            return NoContent();

        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm]IFormFile formFile)
        {
            var newName = Guid.NewGuid() + "." + Path.GetExtension(formFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory() + "wwwroot",newName);
            var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream);

            return Created(string.Empty, formFile);

        }
 

        [HttpGet("[action]")]
        public IActionResult Test([FromForm] string name, [FromHeader] string auth)
        {
            var authentication = HttpContext.Request.Headers["auth"];

                
            var name2 = HttpContext.Request.Form["name"];   
            
            return Ok();
        }


        [HttpGet("[action]")]
        public IActionResult Test2([FromServices] IDummyRepository dummyRepository )
        {
            return Ok(dummyRepository.GetName());
        }

    }

}
