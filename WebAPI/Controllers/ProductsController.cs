using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        /// <summary>
        /// 取得所有商品
        /// </summary>
        /// <returns></returns>
        // GET: api/Products
        
        //[Route("products")]
        [Route("")]
        public IQueryable<Product> GetProduct()
        {
            return db.Product;
        }

        public ProductsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// 取得特定商品 By ID
        /// </summary>
        /// <param name="id">ProductId</param>
        /// <returns></returns>
        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        //[Route("products/{id}")]
        [Route("{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [ResponseType(typeof(Product))]
        [ResponseType(typeof(IQueryable<OrderLine>))]
        //[Route("products/{id}/orderlines")]
        [Route("{id}/orderlines")]
        public IHttpActionResult GetProductOrderLines(int id)
        {
            var orderlines = db.OrderLine.Where(p => p.ProductId == id);
             return Ok(orderlines);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        //[Route("products/{id}")]
        [Route("{id}")]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        //[Route("products")]
        [Route("")]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        //[Route("products/{id}")]
        [Route("{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Product.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Product.Count(e => e.ProductId == id) > 0;
        }
    }
}