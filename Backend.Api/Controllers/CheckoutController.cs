using Backend.Api.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string s_wasmClientURL = string.Empty;

        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CheckoutOrder([FromBody] GameKey key, [FromServices] IServiceProvider sp)
        {
            var referer = Request.Headers.Referer;
            s_wasmClientURL = referer[index: 0];


            var server = sp.GetRequiredService<IServer>();

            var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

            string? thisApiUrl = null;

            if (serverAddressesFeature is not null)
            {
                thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
            }

            if (thisApiUrl is not null)
            {
                var sessionId = await CheckOut(key, thisApiUrl);
                var pubKey = _configuration[key: "Stripe: PubKey"];

                var checkourOrderResponse = new CheckoutOrderResponse()
                {
                    SessionId = sessionId,
                    PubKey = pubKey,
                };
                return Ok(checkourOrderResponse);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [NonAction]

        public async Task<string> CheckOut(GameKey key, string thisApiUrl)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{thisApiUrl}/checkout/success?sessionId=" + "{CHECKOUT_SESSION_ID}",
                CancelUrl = s_wasmClientURL + "failed", // Checkout cancelled.
                PaymentMethodTypes = new List<string>
                {
                    "card"

                },// Only card available in test mode?


                LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {

                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = key.Price, // Price is in USD cents.
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                 Name = key.Key,
                            }
                        },
                          Quantity = 1,
                    },

                },
                Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
            };
            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Id;
        }

        [HttpGet("[action]")]

        public ActionResult CheckoutSucces (string sessionId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId);

            // Here you can save order and customer details to your database.
            var total = session.AmountTotal.Value;
            var customerEmail = session.CustomerDetails.Email;

            return Redirect(s_wasmClientURL + "success");
        }
    }
}

