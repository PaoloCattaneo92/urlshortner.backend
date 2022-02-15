using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaoloCattaneo.UrlShortner.Core.BL;
using PaoloCattaneo.UrlShortner.Core.DAL;
using System;

namespace PaoloCattaneo.UrlShortner.API.Controllers
{
    [Route("")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly IUrlRepository repository;
        private readonly Shortner shortner;

        public RedirectController(IUrlRepository repository)
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
        /// Redirect to the URL corrisponding to the key.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /B
        ///     
        /// </remarks>
        /// <param name="key">Key of the URL destination</param>
        /// <response code="302">Redirect to the decoded URL</response>
        /// <response code="204">Key was not found</response>
        /// <response code="410">Key was found but expired</response>
        /// <response code="500">An error occured during the operation</response>
        [HttpGet("{key}")]
        public ActionResult FindAndRedirect(string key)
        {
            try
            {
                var id = shortner.Decode(key);
                var shortUrl = repository.Get(id);

                if(shortUrl == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                if(shortUrl.ExpirationTime < DateTime.UtcNow)
                {
                    return StatusCode(StatusCodes.Status410Gone, $"Shortened url with \"{key}\" expired at {shortUrl.ExpirationTime}");
                }

                return Redirect(shortUrl.Url);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error in GET url with key \"{key}\"");
            }
        }
    }
}
