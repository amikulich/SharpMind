using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using SharpMind.AppServices;

namespace SharpMind.Api.Controllers
{
    [BotAuthentication]
    public class BotActivityController : ApiController
    {
        private readonly IActivityResolverFacade _activityResolverFacade;

        public BotActivityController(IActivityResolverFacade activityResolver)
        {
            _activityResolverFacade = activityResolver;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity == null)
            {
                BadRequest();
            }

            await _activityResolverFacade.ResolveActivityAsync(activity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
