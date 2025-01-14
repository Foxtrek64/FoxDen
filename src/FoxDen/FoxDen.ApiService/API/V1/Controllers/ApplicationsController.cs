using FoxDen.Data;
using FoxDen.Data.Models;
using FoxDen.Data.Models.Partials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoxDen.ApiService.API.V1.Controllers
{
    /// <summary>
    /// Defines a controller for accessing and manipulating application registrations.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApplicationsController(FoxDenDbContext dbContext) : ControllerBase
    {
        /// <summary>
        /// Gets an enumerable collection of well-known app ids and their names.
        /// </summary>
        /// <returns></returns>
        // GET: api/v{Version}/<ApplicationsController>
        [HttpGet]
        public async Task<ActionResult<Dictionary<Guid, string>>> Get()
        {
            var dict = await dbContext.AppRegistrations.ToDictionaryAsync(it => it.Id, it => it.AppName);
            return Ok(dict);
        }

        /// <summary>
        /// Gets the app registration for the specified id.
        /// </summary>
        /// <param name="id">The unique id of the app registration.</param>
        /// <returns>The requested <see cref="AppRegistration"/> or a 404 result.</returns>
        // GET api/<ApplicationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppRegistration?>> Get(Guid id)
        {
            var app = await dbContext.AppRegistrations.FindAsync(id);

            return app is not null
                ? Ok(app)
                : NotFound();
        }

        /// <summary>
        /// Creates a new AppRegistration.
        /// </summary>
        /// <param name="appRegistration">The application to register.</param>
        // POST api/<ApplicationsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AppRegistration appRegistration)
        {
            dbContext.AppRegistrations.Add(appRegistration);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction("GetStudent", new { id = appRegistration.Id }, appRegistration);
        }

        /// <summary>
        /// Updates the AppRegistration with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The unique id of the AppRegistration.</param>
        /// <param name="partialApp">A partial app. Properties that are omitted are not updated. Properties set to null are nulled.</param>
        // PUT api/<ApplicationsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AppRegistration>> Put(Guid id, [FromBody] PartialAppRegistration partialApp)
        {
            var app = await dbContext.AppRegistrations.FindAsync(id);

            if (app is null)
            {
                if (partialApp.AppName.HasValue && partialApp.AppBarButton.HasValue)
                {
                    app = new AppRegistration()
                    {
                        AppName = partialApp.AppName.Value,
                        AppBarButton = partialApp.AppBarButton.Value
                    };
                    dbContext.AppRegistrations.Add(app);
                    return Ok(app);
                }
                
                return NotFound();
            }

            if (partialApp.AppName.HasValue && app.AppName != partialApp.AppName.Value)
            {
                app.AppName = partialApp.AppName.Value;
            }

            if (partialApp.AppBarButton.HasValue && app.AppBarButton != partialApp.AppBarButton.Value)
            {
                app.AppBarButton = partialApp.AppBarButton.Value;
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes the registration with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<ApplicationsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var app = await dbContext.AppRegistrations.FindAsync(id);
            if (app is not null)
            {
                dbContext.AppRegistrations.Remove(app);
                await dbContext.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
    }
}
