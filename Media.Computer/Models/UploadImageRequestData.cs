namespace Media.Computer.Models
{
    public class UploadImageRequestData
    {

        public string base64Image { get; set; }
        public string sign { get; set; } // MD5( base64Image + secretKey)
    }
}
