using Computer.Common;
using Media.Computer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Media.Computer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private IConfiguration _config;
        public UploadFileController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("UploadProductImage")]
        public async Task<UploadImageResponseData> UploadProductImage(UploadImageRequestData requestData)
        {
            var returnData = new UploadImageResponseData();
            try
            {
                await Task.Yield();

                string imgPath = string.Empty;
                if (requestData == null
                    || string.IsNullOrEmpty(requestData.base64Image)
                    || string.IsNullOrEmpty(requestData.sign))
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Dữ liệu đầu vào không hợp lệ";
                    return returnData;
                }

                var secretKey = _config["Sercurity:secretKeyCall_API"] ?? "UxFkTt5siR5dibph8JdUIsixJ2mmhr";
                var verifySign = Security.MD5Hash(requestData.base64Image + "|" + secretKey);

                // xử lý che email , số điện thoại , link
                if (requestData.sign != verifySign)
                {

                    returnData.ResponseCode = -3;
                    returnData.Description = "Chữ ký không hợp lệ";
                    return returnData;
                }


                var path = "Product"; //Path

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                string imageName = Guid.NewGuid().ToString() + ".png";
                //set the image path
                imgPath = Path.Combine(path, imageName);
                if (requestData.base64Image.Contains("data:image"))
                {
                    requestData.base64Image = requestData.base64Image.Substring(requestData.base64Image.LastIndexOf(',') + 1);
                }
                byte[] imageBytes = Convert.FromBase64String(requestData.base64Image);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);

                // Update Database 
                returnData.ResponseCode = 1;
                returnData.Description = imageName;
                return returnData;
            }
            catch (Exception ex)
            {
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return returnData;
            }
        }
    }
}
