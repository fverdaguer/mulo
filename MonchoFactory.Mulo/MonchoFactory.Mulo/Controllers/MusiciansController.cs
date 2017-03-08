using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MonchoFactory.Mulo.WebApi.Core;
using MonchoFactory.Mulo.WebApi.Helpers;
using MonchoFactory.Mulo.WebApi.Models;

namespace MonchoFactory.Mulo.WebApi.Controllers
{
    [RoutePrefix("api/musicians")]
    [Authorize]
    public class MusiciansController : ApiController
    {
        private MuloContext db = new MuloContext();

        // GET: api/Musicians
        public async Task<IHttpActionResult> GetMusicians()
        {
            return Ok(await db.Musicians.ToListAsync());
        }

        [HttpGet]
        [Route("{instruments}/{genres}/{latitude}/{longitude}")]
        // GET: api/Musicians/instruments/genres/locationLatitude/locationLongitude
        public async Task<IHttpActionResult> GetMusicians(string instruments, string genres, string latitude, string longitude)
        {
            var instrumentIds = instruments.Split(',').Select(int.Parse).ToList();
            var genreIds = genres.Split(',').Select(int.Parse).ToList();

            var musicians = await db.Musicians.ToListAsync();

            var result = musicians.Where(musician => musician.Instruments.Any(x => instrumentIds.Any(y => y == x.Id))).ToList();
            result = result.Where(musician => musician.Genres.Any(x => genreIds.Any(y => y == x.Id))).ToList();
            
            return Ok(result.OrderBy(
                x =>
                    LocationHelper.GetDistanceKM(x.LocationLatitude, x.LocationLongitude, Convert.ToDouble(latitude),
                        Convert.ToDouble(longitude))));
        }

        // GET: api/Musicians/5
        [ResponseType(typeof(Musician))]
        public async Task<IHttpActionResult> GetMusician(int id)
        {
            Musician musician = await db.Musicians.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }

            return Ok(musician);
        }

        // PUT: api/Musicians/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMusician(int id, Musician musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != musician.Id)
            {
                return BadRequest();
            }

            musician.Genres = musician.Genres.Select(genre => db.Genres.Find(genre.Id)).ToList();
            musician.Instruments = musician.Instruments.Select(instrument => db.Instruments.Find(instrument.Id)).ToList();

            var original = db.Musicians.Include("Genres").Include("Instruments").Single(x => x.Id == musician.Id);

            original.Genres = musician.Genres;
            original.Instruments = musician.Instruments;
            original.Email = musician.Email;
            original.FirstName = musician.FirstName;
            original.LastName = musician.LastName;
            original.LocationGooglePlaceId = musician.LocationGooglePlaceId;
            original.LocationLatitude = musician.LocationLatitude;
            original.LocationLongitude = musician.LocationLongitude;
            original.LocationName = musician.LocationName;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicianExists(id))
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

        // POST: api/Musicians
        [ResponseType(typeof(Musician))]
        public async Task<IHttpActionResult> PostMusician([FromBody] Musician musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            musician.Genres = musician.Genres.Select(genre => db.Genres.Find(genre.Id)).ToList();

            musician.Instruments = musician.Instruments.Select(instrument => db.Instruments.Find(instrument.Id)).ToList();

            db.Musicians.Add(musician);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = musician.Id }, musician);
        }

        // DELETE: api/Musicians/5
        [ResponseType(typeof(Musician))]
        public async Task<IHttpActionResult> DeleteMusician(int id)
        {
            Musician musician = await db.Musicians.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }

            db.Musicians.Remove(musician);
            await db.SaveChangesAsync();

            return Ok(musician);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MusicianExists(int id)
        {
            return db.Musicians.Count(e => e.Id == id) > 0;
        }
    }
}