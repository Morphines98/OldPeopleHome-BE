using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace MeerPflege.Application.Azure
{
  public class BlobService
  {
    private const string blobServiceEndpoint = "https://oldpeoplehome.blob.core.windows.net";
    private const string blobContainerName = "files";
    private const string storageAccountName = "oldpeoplehome";
    private const string storageAccountKey = "eQyW4Onk4BDlAMb/ak2/ySwUTwz3LNwcSWeEloLUb/PXy4N3FEYLQHoXTBTYyCw1lSaE2L8TrGlY+ASts/SUrg==";

    private BlobServiceClient _serviceClient;
    private BlobContainerClient _container;

    public BlobService()
    {
      _serviceClient = new BlobServiceClient(new Uri(blobServiceEndpoint), new StorageSharedKeyCredential(storageAccountName, storageAccountKey));

      _container = _serviceClient.GetBlobContainerClient(blobContainerName);
      if (!_container.Exists())
      {
        _container.CreateAsync().Wait();
      }

    }

    public async Task<DTOs.File> UploadFileAsync(IFormFile file)
    {
      var blobClient = _container.GetBlobClient(file.FileName);

      using (var stream = file.OpenReadStream())
      {
        await blobClient.UploadAsync(stream, true);
      }

      return new DTOs.File() { Url = blobClient.Uri.ToString(), Name = file.FileName };
    }
  }
}