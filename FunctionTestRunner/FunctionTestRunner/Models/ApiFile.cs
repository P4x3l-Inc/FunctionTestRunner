namespace FunctionTestRunner.Models
{
    public class ApiFile
    {
        public ApiFile(byte[] content, string contentType, string fieldName, string fileName)
        {
            Content = content;
            ContentType = contentType;
            FieldName = fieldName;
            FileName = fileName;
        }

        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FieldName { get; set; }
        public string FileName { get; set; }
    }
}
