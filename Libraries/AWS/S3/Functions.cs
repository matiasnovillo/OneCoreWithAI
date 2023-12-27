using Amazon.S3;
using Amazon;
using Amazon.S3.Transfer;
using Amazon.S3.Model.Internal.MarshallTransformations;

namespace AWS.S3
{
    public class Functions
    {
        /// <summary>
        /// Nombre de la instancia de base de datos AWS S3
        /// </summary>
        public string BucketName { get; }
        readonly IAmazonS3 RegionEndPoint = new AmazonS3Client(
            RegionEndpoint.USEast2);

        public Functions(string bucketName, IAmazonS3 regionEndPoint)
        {
            BucketName = bucketName;
            RegionEndPoint = regionEndPoint;
        }

        /// <summary>
        /// Esta función es la encargada de guardar los archivos a analizar
        /// en AWS S3
        /// </summary>
        /// <param name="filePath">Archivo almacenado en el servidor local</param>
        public bool SaveFileInAWSS3(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("El nombre del archivo no puede ser nulo o estar vacío.");
            }

            TransferUtility TransferUtility = new(RegionEndPoint);
            TransferUtilityUploadRequest TransferUtilityUploadRequest = new()
            {
                BucketName = BucketName,
                Key = filePath
            };

            try
            {
                MemoryStream MemoryStream = new();
                Stream Stream = File.OpenRead(filePath);

                Stream.CopyTo(MemoryStream);
                MemoryStream.Seek(0, SeekOrigin.Begin);
                byte[] buf = new byte[MemoryStream.Length];
                MemoryStream.Read(buf, 0, buf.Length);

                TransferUtilityUploadRequest.InputStream = MemoryStream;
                TransferUtility.Upload(TransferUtilityUploadRequest);

                TransferUtilityUploadRequest.InputStream.Close();

                return true;
            }
            catch (Exception)
            {
                throw;
            } 
        } 
    }
}
