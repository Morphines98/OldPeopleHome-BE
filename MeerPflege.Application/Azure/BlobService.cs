using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace MeerPflege.Application.Azure
{
  public class BlobService
  {
    private const string blobServiceEndpoint = "https://oldpeopleshome.blob.core.windows.net";
    private const string blobContainerName = "files";
    private const string storageAccountName = "oldpeopleshome";
    private const string storageAccountKey = "iwAsvGJrvn2XKcOPk9la1LovsHFv35sKLDQ+vE9uhxZFKiXupxN1mX+x21e7XqRQ6pDuyUBDx6g3+AStYwwiKA==";

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