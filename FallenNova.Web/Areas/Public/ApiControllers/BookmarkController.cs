using FallenNova.Web.Areas.Public.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FallenNova.Web.Areas.Public.ApiControllers
{
    public class BookmarkController : ApiController
    {
        //
        // POST: /api/bookmark/

        public HttpResponseMessage Post(BookmarkModel bookmarkModel)
        {
            if (ModelState.IsValid)
            {
                // TODO: Implement the following updates and cache reflection.
                if (bookmarkModel.AddBookmark)
                {
                    // Add the bookmark. Persistence update and update UI menu cache.
                }
                else
                {
                    // Remove the bookmark. Persistence update and update UI menu cache.
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
