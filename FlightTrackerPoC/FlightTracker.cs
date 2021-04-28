using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FlightTrackerPoC
{
    public static class FlightTracker
    {
        [FunctionName("FlightTracker")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "flight/{id}")] HttpRequest req,
            string id,
            [Blob("%flightcontainername%", Connection = "StorageConnectionString")] CloudBlobContainer outputContainer,
            ILogger log)
        {
            log.LogInformation($"Processing flight {id}");

            await outputContainer.CreateIfNotExistsAsync();

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var blobName = id;

            var cloudBlockBlob = outputContainer.GetBlockBlobReference(blobName);
            await cloudBlockBlob.UploadTextAsync(requestBody);            

            return new OkObjectResult($"Tracking stored successfully with blob name: {id}");
        }
    }
}

