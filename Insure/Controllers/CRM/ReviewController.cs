using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Insure.Exceptions.CRM;
using Insure.Models;
using Insure.Models.CRM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Insure.Controllers.CRM
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly CrmContext _db;

        public ReviewController(CrmContext context)
        {
            _db = context;
        }

        
        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <returns>Array of reviews</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Review>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Review>>> Get()
        {
            try
            {
                return Ok(await _db.Reviews.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        
        /// <summary>
        /// Get review by id
        /// </summary>
        /// <param name="id">Review id</param>
        /// <returns>Review object</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Review), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Review>> Get(int id)
        {
            try
            {
                var review = await _db.Reviews.FirstOrDefaultAsync(x => x.Id == id);
                return review != null ? Ok(review) : NotFound(new NotFoundError($"Review with id = {id} not found!"));
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Get reviews from page
        /// </summary>
        /// <param name="page">Page number (default first page)</param>
        /// <param name="size">Page size (default 10 items)</param>
        /// <returns>Reviews from page</returns>
        [HttpGet("{page:int}/{size:int}")]
        [ProducesResponseType(typeof(IEnumerable<Review>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomError), 600)]
        public async Task<ActionResult<IEnumerable<Review>>> GetPage(int page = 1, int size = 10)
        {
            try
            {
                var reviews = _db.Reviews.AsQueryable();
                var count = await reviews.CountAsync();

                var pagesCount = Math.Ceiling((decimal) count / size);

                if (page > pagesCount)
                    return BadRequest(new CustomError(ErrorTypes.UnavailableParameter.ToString(),
                        $"Page size can`t be more than {pagesCount} with page size as {size}"));
                
                var items = await reviews.Skip((page - 1) * size).Take(size).ToListAsync();

                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Add new review
        /// </summary>
        /// <param name="review">Review object</param>
        /// <returns>Added review id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomError), 600)]
        public async Task<ActionResult<int>> AddReview([FromBody] Review review)
        {
            try
            {
                if (review == null)
                    return BadRequest(new CustomError(ErrorTypes.NullObject.ToString(),
                        "Income review object is null"));
                
                _db.Reviews.Add(review);
                await _db.SaveChangesAsync();

                return Ok(review.Id);
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Delete review
        /// </summary>
        /// <param name="id">Id review for delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var review = await _db.Reviews.FirstOrDefaultAsync(x => x.Id == id);
                if (review == null)
                    return BadRequest(new NotFoundError($"The review with id = {id} not found!"));

                _db.Remove(review);
                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }
    }
}