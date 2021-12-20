using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MusicApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicApi", Version = "v1" });
            });
            services.AddDbContext<ApiDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicApi v1"));
            }
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
/* using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Helpers;
using MusicApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {

        private ApiDbContext _dbContext;
        public SongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Songs.ToListAsync());
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _dbContext.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound("No record found against this id");
            }
            return Ok(song);
        }

        //api/songs/test/1
        [HttpGet("[Action]/{id}")]
        public int Test(int id)
        {
            return id;
        }

        // POST api/<SongsController>
        /* [HttpPost]
         public async Task<IActionResult> Post([FromBody] Song song)
         {
            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
             return StatusCode(StatusCodes.Status201Created);
         }


[HttpPost]
public async Task<IActionResult> Post([FromForm] Song song)
{
    var imageUrl = await FileHelper.UploadImage(song.Image);
    song.ImageUrl = imageUrl;
    await _dbContext.Songs.AddAsync(song);
    await _dbContext.SaveChangesAsync();
    return StatusCode(StatusCodes.Status201Created);
}
// PUT api/<SongsController>/5
[HttpPut("{id}")]
public async Task<IActionResult> Put(int id, [FromBody] Song songObj)
{
    var song = await _dbContext.Songs.FindAsync(id);
    if (song == null)
    {
        return NotFound("No record found against this id");
    }
    else
    {
        song.Title = songObj.Title;
        song.Language = songObj.Language;
        song.Duration = songObj.Duration;
        await _dbContext.SaveChangesAsync();
        return Ok("Record updated successfully");
    }
}

// DELETE api/<SongsController>/5
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var song = await _dbContext.Songs.FindAsync(id);
    if (song == null)
    {
        return NotFound("No record found against this id");
    }
    _dbContext.Songs.Remove(song);
    await _dbContext.SaveChangesAsync();
    return Ok("Record Deleted");
}
    }
}*/