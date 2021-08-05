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
    public class OfficeController : ControllerBase
    {
        private readonly CrmContext _db;

        public OfficeController(CrmContext context)
        {
            _db = context;
        }
        
        /// <summary>
        /// Get all offices
        /// </summary>
        /// <returns>Array of offices</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Office>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Office>>> Get()
        {
            try
            {
                return Ok(await _db.Offices.Include(x => x.DaySchedules).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Get office by id
        /// </summary>
        /// <param name="id">Office Id</param>
        /// <returns>Office object</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Office), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Office>> Get(int id)
        {
            try
            {
                var office = await _db.Offices.Include(x => x.DaySchedules).FirstOrDefaultAsync(x => x.Id == id);
                return office != null ? Ok(office) : 
                    NotFound(new NotFoundError($"The office with id = {id} not found!"));
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Add new office
        /// </summary>
        /// <param name="office">Office object</param>
        /// <returns>Id added office</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomError), 600)]
        public async Task<ActionResult<int>> AddOffice([FromBody] Office office)
        {
            try
            {
                if (office == null)
                    return BadRequest(new CustomError(ErrorTypes.NullObject.ToString(),
                        "Income office object is null"));

                if (await _db.Offices.FirstOrDefaultAsync(x => x.Name.Equals(office.Name)) != null)
                    return BadRequest(new CustomError(ErrorTypes.AlreadyExist.ToString(),
                        "Office with same name already exist"));

                _db.Offices.Add(office);
                await _db.SaveChangesAsync();
                
                return Ok(office.Id);
            }
            catch(Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Update office info
        /// </summary>
        /// <param name="office">Office object</param>
        /// <returns>Updated office object</returns>
        [HttpPut]
        [ProducesResponseType(typeof(Office), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomError), 600)]
        public async Task<ActionResult<Office>> Update([FromBody] Office office)
        {
            try
            {
                if (office == null)
                    return BadRequest(new CustomError(ErrorTypes.NullObject.ToString(),
                        "Income office object is null"));

                if(!_db.Offices.AsNoTracking().Any(x => x.Id == office.Id))
                    return NotFound(new NotFoundError($"The office with id = {office.Id} not found!"));

                var scheduleForDel = _db.DaySchedules.Where(x => x.OfficeId == office.Id);
                _db.DaySchedules.RemoveRange(scheduleForDel);
                
                _db.Update(office);
                await _db.SaveChangesAsync();

                return Ok(office);
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError(e.Message));
            }
        }

        /// <summary>
        /// Delete office
        /// </summary>
        /// <param name="id">Id office for delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var office = _db.Offices.FirstOrDefault(x => x.Id == id);
                if(office == null)
                    return NotFound(new NotFoundError($"The office with id = {id} not found!"));

                _db.Offices.Remove(office);
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