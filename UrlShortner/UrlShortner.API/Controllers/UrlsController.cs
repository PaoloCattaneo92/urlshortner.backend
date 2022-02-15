using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaoloCattaneo.UrlShortner.Core.BL;
using PaoloCattaneo.UrlShortner.Core.DAL;
using PaoloCattaneo.UrlShortner.Core.Model;
using System;

namespace PaoloCattaneo.UrlShortner.API.Controllers
{
    [Route("api/urls")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlRepository repository;
        private readonly Shortner shortner; 

        public UrlsController(IUrlRepository repository)
        {
            this.repository = repository;

            //TODO opzioni da database?
            shortner = new Shortner(
                new ShortnerOptionsBuilder()
                    .WithAlphabet("ABCDEFGHIJKLMNOPRQSTUVWXYZ0123456789")
                .Build()
                );
        }

        /// <summary>
        /// Get the a shortned URL corresponding to the given key.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/urls/BR
        ///     
        /// Sample response:
        /// 
        ///     {
        ///         "id": 52,
        ///         "key": "BR",
        ///         "url": "https://www.youtube.com/watch?v=tCs48OFv7xA",
        ///         "creationTime": "2022-02-15T14:08:21
        ///         "expirationTime": "2022-08-14T14:08:21"
        ///     }
        /// 
        /// </remarks>
        /// <param name="key">Key of the URL</param>
        /// <response code="200">Returns the requested URL shortened object</response>
        /// <response code="204">URL with the given key was not found</response>
        /// <response code="500">An error occured during the operation</response>
        [HttpGet("{key}")]
        [ProducesResponseType(typeof(UrlShort), 200)]
        [Produces("application/json")]
        public ActionResult Get(string key)
        {
            try
            {
                var id = shortner.Decode(key);
                var shortUrl = repository.Get(id);
                if(shortUrl != null)
                {
                    return Ok(shortUrl);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error in GET url with key \"{key}\"");
            }
        }

        /// <summary>
        /// Encode a new URL into the database.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/urls?url=https://www.youtube.com/watch?v=tCs48OFv7xA
        ///     
        /// Sample response:
        /// 
        ///     {
        ///         "id": 52,
        ///         "key": "BR",
        ///         "url": "https://www.youtube.com/watch?v=tCs48OFv7xA",
        ///         "creationTime": "2022-02-15T14:08:21
        ///         "expirationTime": "2022-08-14T14:08:21"
        ///     }
        /// 
        /// </remarks>
        /// <param name="url">URL that must be encoded</param>
        /// <response code="200">Returns the newly created URL shortened object</response>
        /// <response code="500">An error occured during the operation</response>
        [HttpPost]
        [ProducesResponseType(typeof(UrlShort), 200)]
        [Produces("application/json")]
        public ActionResult Insert(string url)
        {
            try
            {
                // TODO meglio rendere transazione safe
                var shrt = repository.Insert(url);
                shrt.Key = shortner.Encode(shrt.Id);
                repository.Update(shrt.Id, shrt.Key);
                return Ok(shrt);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error in encoding url \"{url}\"");
            }
        }

        /// <summary>
        /// Delete an URL from the database.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/urls/B
        /// 
        /// </remarks>
        /// <param name="key">Key of the URL that must be deleted</param>
        /// <response code="200">Returns the newly created URL shortened object</response>
        /// <response code="500">An error occured during the operation</response>
        [HttpDelete("{key}")]
        public ActionResult Delete(string key)
        {
            try
            {
                var id = shortner.Decode(key);
                repository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleteing url with key \"{key}\"");
            }
        }
    }
}
