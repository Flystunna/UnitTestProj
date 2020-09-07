using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TestAPP2.Models;

namespace TestAPP2.Controllers
{
    public class ProductController : ApiController
    {
        // modify the type of the db field
        private IStoreAppContext db = new StoreAppContext();

        // add these constructors
        public ProductController() { }

        public ProductController(IStoreAppContext context)
        {
            db = context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return db.Products.AsEnumerable();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await Task.FromResult(GetAllProducts());
        }
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            //db.Entry(product).State = EntityState.Modified;
            db.MarkAsModified(product);

            // rest of method not shown
            return Ok();
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public async Task<IHttpActionResult> GetProductAsync(int id)
        {
            return await Task.FromResult(GetProduct(id));
        }

        // POST api/Product
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }
        // rest of class not shown
    }
}