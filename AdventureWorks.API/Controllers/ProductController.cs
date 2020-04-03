using AdventureWorks.Services.Production;
using System.Collections.Generic;
using System.Web.Http;

namespace AdventureWorks.API.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        public IEnumerable<Product> Get()
        {
            return _productService.GetProducts();
        }

        // GET: api/Product/5
        public Product Get(int id)
        {
            return _productService.GetProduct(id);
        }

        // POST: api/Product
        public void Post(Product value)
        {
            _productService.AddProduct(value);
        }

        // PUT: api/Product/5
        public void Put(Product value)
        {
            _productService.UpdateProduct(value);
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
