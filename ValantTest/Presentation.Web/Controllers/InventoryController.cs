namespace Presentation.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ValantTest.Application.DTO;
    using ValantTest.Client.Sdk.Services;

    public class InventoryController : Controller
    {
        private readonly InventoryClient inventoryClient;

        public InventoryController()
        {
            this.inventoryClient = new InventoryClient();
        }

        [HttpPost]
        public async Task<JsonResult> CreateItem(string label, string expirationDate, string description)
        {
            var itemToCreate = new ItemDto { Description = description, ExpirationDate = DateTime.Parse(expirationDate), Label = label };
            var result = await this.inventoryClient.AddItem(itemToCreate);
            var jsonResult = this.Json(result);
            return jsonResult;
        }

        public async Task<JsonResult> TakeItemByLabel(string label)
        {
            var result = await this.inventoryClient.GetByItemLablel(label);
            if (result == null)
            {
                var jsonResult = this.Json("Not Found", JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            else
            {
                var jsonResult = this.Json(result, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
        }
    }
}