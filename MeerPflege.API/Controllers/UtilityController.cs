using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace MeerPflege.API.Controllers
{
  [Authorize]
  public class UtilityController : BaseApiController
  {
    private readonly MeerPflege.Application.Azure.BlobService _blobService;
    public UtilityController(MeerPflege.Application.Azure.BlobService blobService)
    {
      _blobService = blobService;
    }

    [HttpPost(Name = "Upload")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<MeerPflege.Application.DTOs.File>> Upload()
    {
      var files = this.Request.Form.Files;
      var file = files[0];
      var result = await _blobService.UploadFileAsync(file);
      return HandleResult(Application.Core.Result<MeerPflege.Application.DTOs.File>.Success(result));
    }
  }
}