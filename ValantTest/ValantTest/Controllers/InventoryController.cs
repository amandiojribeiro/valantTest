namespace ValantTest.Presentation.Api.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Application.DTO;
    using Application.Services;

    [RoutePrefix("items")]
    public class InventoryController : ApiController
    {
        private readonly IInventoryService inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }
        
        /// <summary>
        /// Get a Item from inventory by label.
        /// </summary>
        /// <param name="label">The label of the item.</param>
        /// <returns>The Outfit.</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet, Route("{label}"), ResponseType(typeof(ItemDto))]
        public async Task<HttpResponseMessage> GetByLablel(string label)
        {
            var result = await this.inventoryService.TakeItemByLabel(label);
            if (result != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Creates a Item.
        /// </summary>
        /// <param name="request">Item to save</param>
        /// <remarks>
        /// Type<br/>
        /// 0 = Great<br/>
        /// 1 = Even greater<br/>
        /// 2 = The best of all <br/>
        /// </remarks>
        /// <response code="200">No content</response>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Item</returns>
        [HttpPost, Route(""), ResponseType(typeof(ItemDto))]
        public async Task<HttpResponseMessage> AddItem(ItemDto request)
        {
            var result = await this.inventoryService.SaveItem(request);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}