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
    [ApiController]
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private readonly CrmContext _db;

        public FaqController(CrmContext context)
        {
            _db = context;
        }
        
        /// <summary>
        /// Get all faqs
        /// </summary>
        /// <returns>Faqs arrays</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FAQ>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<FAQ>>> Get()
        {
            try
            {
                return Ok(await _db.Faqs.Include(x => x.FaqsDocuments).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }
    
        /// <summary>
        /// Get faq by id
        /// </summary>
        /// <param name="id">Faq id</param>
        /// <returns>Faq object</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FAQ), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<FAQ>> Get(int id)
        {
            try
            {
                var faq = await _db.Faqs.Include(x => x.FaqsDocuments).FirstOrDefaultAsync(x => x.Id == id);
                return faq != null ? Ok(faq) : NotFound(new NotFoundError($"The faq with id = {id} not found!"));
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }
    
        /// <summary>
        /// Add new faq
        /// </summary>
        /// <param name="faq">New faq object</param>
        /// <returns>Id added faq</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomError), 600)]
        public async Task<ActionResult<int>> AddFaq([FromBody] FAQ faq)
        {
            try
            {
                if (faq == null)
                    return BadRequest(new CustomError(ErrorTypes.NullObject.ToString(),
                        "Income faq object is null"));

                if (await _db.Faqs.FirstOrDefaultAsync(x => x.Question == faq.Question) != null)
                    return BadRequest(new CustomError(ErrorTypes.AlreadyExist.ToString(),
                        "Faq with same question already exist"));

                _db.Faqs.Add(faq);
                await _db.SaveChangesAsync();

                return Ok(faq.Id);

            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Update faq info
        /// </summary>
        /// <param name="faq">Updated faq object</param>
        /// <returns>Updated faq object</returns>
        [HttpPut]
        [ProducesResponseType(typeof(FAQ), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomError), 600)]
        public async Task<ActionResult<FAQ>> Update([FromBody] FAQ faq)
        {
            try
            {
                if (faq == null)
                    return BadRequest(new CustomError(ErrorTypes.NullObject.ToString(),
                        "Income faq object is null"));

                if (!_db.Faqs.AsNoTracking().Any(x => x.Id == faq.Id))
                    return NotFound(new NotFoundError($"The faq with id = {faq.Id} not found"));

                _db.Update(faq);
                await _db.SaveChangesAsync();

                return Ok(faq);
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Delete faq
        /// </summary>
        /// <param name="id">Id faq for delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var faq = await _db.Faqs.Include(x => x.FaqsDocuments).FirstOrDefaultAsync(x => x.Id == id);
                if (faq == null)
                    return NotFound(new NotFoundError($"The faq with id = {id} not found"));

                _db.Faqs.Remove(faq);
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