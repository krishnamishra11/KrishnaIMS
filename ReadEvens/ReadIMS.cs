using IMSRepository.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ReadEvens
{
    public static class ReadIMS
    {

        [FunctionName("ReadIMS")]
        public static void Run([ServiceBusTrigger("purchaseordertopic", "PurchaseOrderSubscribe", Connection = "ServiceBus")] string mySbMsg, ILogger log)
        {
            try
            {


                PurchaseOrder purchageOrder = System.Text.Json.JsonSerializer.Deserialize<PurchaseOrder>(mySbMsg);

                log.LogInformation($"function processed message: {mySbMsg}");

                if(purchageOrder.ShipmentMode.ToLower().Contains("air"))
                    Execute(purchageOrder).Wait();
            }
            catch (Exception ex )
            {
                log.LogError(ex, $"function processed message: {mySbMsg}");
            }
        }


        static async Task Execute(PurchaseOrder purchaseOrder)
        {
            var client = new SendGridClient("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");
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("krishnamishra11@hotmail.com", "Purchage order by air Azure"));

            var recipients = new List<EmailAddress>
                {
                    new EmailAddress("krishna.mishra2@cognizant.com"),
                    new EmailAddress("krishnamishra11@rediffmail.com")
                };
            msg.AddTos(recipients);

            msg.SetSubject("By Air Purchage order created");

            msg.AddContent(MimeType.Text, "This is just a simple azure test message!");
            msg.AddContent(MimeType.Html, "<p>Purchage order by air created with purchage order id!"+purchaseOrder.Id+ "</p>");
             await client.SendEmailAsync(msg);

        }
    }
}