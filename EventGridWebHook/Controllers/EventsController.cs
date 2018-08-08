using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EventGridWebHook.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody]List<GridEvent<object>> events)
        {
            if (events == null)
            {
                return Ok();
            }
            foreach (var event1 in events)
            {
                if (event1.EventType == "Microsoft.EventGrid.SubscriptionValidationEvent")
                {
                    var eventData = JsonConvert.DeserializeObject<SubscriptionValidationEventData>(event1.Data.ToString());
                    var responseData = new SubscriptionValidationResponse();
                    responseData.ValidationResponse = eventData.ValidationCode;
                    return Ok(responseData);
                }
            }

            return Ok();
        }
    }
}