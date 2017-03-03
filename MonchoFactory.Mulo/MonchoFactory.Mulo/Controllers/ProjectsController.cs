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
using MonchoFactory.Mulo.WebApi.Models;

namespace MonchoFactory.Mulo.WebApi.Controllers
{
    public class ProjectsController : ApiController
    {
        private MuloContext db = new MuloContext();

        // GET: api/Projects
        public async Task<IHttpActionResult> GetProjects()
        {
            return Ok(await db.Projects.ToListAsync());
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        public async Task<IHttpActionResult> GetProject(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ProjectID)
            {
                return BadRequest();
            }

            project.Genres = project.Genres.Select(genre => db.Genres.Find(genre.Id)).ToList();
            project.Instruments = project.Instruments.Select(instrument => db.Instruments.Find(instrument.Id)).ToList();

            var original = db.Projects.Include("Genres").Include("Instruments").Single(x => x.ProjectID == project.ProjectID);

            original.Genres = project.Genres;
            original.Instruments = project.Instruments;
            original.Title = project.Title;
            original.FacebookUrl = project.FacebookUrl;
            original.SoundcloudUrl = project.SoundcloudUrl;
            original.LocationGooglePlaceId = project.LocationGooglePlaceId;
            original.LocationLatitude = project.LocationLatitude;
            original.LocationLongitude = project.LocationLongitude;
            original.LocationName = project.LocationName;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public async Task<IHttpActionResult> PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            project.Genres = project.Genres.Select(genre => db.Genres.Find(genre.Id)).ToList();

            project.Instruments = project.Instruments.Select(instrument => db.Instruments.Find(instrument.Id)).ToList();

            db.Projects.Add(project);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = project.ProjectID }, project);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Project))]
        public async Task<IHttpActionResult> DeleteProject(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ProjectID == id) > 0;
        }
    }
}