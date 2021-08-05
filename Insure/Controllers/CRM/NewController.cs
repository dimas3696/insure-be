using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Insure.Exceptions.CRM;
using Insure.Models;
using Insure.Models.CRM;
using Insure.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Insure.Controllers.CRM
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : ControllerBase
    {
        private readonly CrmContext _db;

        public NewController(CrmContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns>Array of news</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<New>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<New>>> Get()
        {
            try
            {
                return Ok(await _db.News.Include(x => x.Authors).Include(t => t.Tags).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Get new by id
        /// </summary>
        /// <param name="id">New id</param>
        /// <returns>New object</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(New), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<New>> Get(int id)
        {
            try
            {
                var @new = await _db.News.Include(x => x.Authors)
                    .Include(x => x.Tags)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return @new != null ? Ok(@new) : NotFound(new NotFoundError($"The new with id = {id} not found"));
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Add new new
        /// </summary>
        /// <param name="new">New object</param>
        /// <returns>Id added new</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomError), 600)]
        public async Task<ActionResult<int>> AddNew([FromBody] New @new)
        {
            try
            {
                if (@new == null)
                    return BadRequest(new CustomError(ErrorTypes.NullObject.ToString(),
                        "Income new object is null"));

                if (await _db.News.FirstOrDefaultAsync(x => x.Title == @new.Title) != null)
                    return BadRequest(new CustomError(ErrorTypes.AlreadyExist.ToString(),
                        "New with same title already exist"));

                _db.News.Add(@new);
                await _db.SaveChangesAsync();

                return Ok(@new.Id);
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Update new info
        /// </summary>
        /// <param name="new">New object</param>
        /// <returns>Updated new object</returns>
        [HttpPut]
        [ProducesResponseType(typeof(New), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomError), 600)]
        public async Task<ActionResult<New>> Update([FromBody] New @new)
        {
            try
            {
                if (@new == null)
                    return BadRequest(new CustomError(ErrorTypes.NullObject.ToString(),
                        "Income new object is null"));

                _db.Entry(@new).State = EntityState.Modified;
                UpdateHelper.UpdateOrCreateRelatedObj(@new.Authors, _db);
                UpdateHelper.UpdateOrCreateRelatedObj(@new.Tags, _db);
                
                await _db.SaveChangesAsync();

                return Ok(@new);
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Delete new
        /// </summary>
        /// <param name="id">Id new for delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var @new = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
                if (@new == null)
                    return NotFound(new NotFoundError($"The new with id = {id} not found"));

                _db.News.Remove(@new);
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