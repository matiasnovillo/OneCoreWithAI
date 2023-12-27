using Amazon;
using Amazon.S3;
using AWS.S3;

namespace AWS.Test
{
    public class SaveFileInAWSS3Should
    {
        readonly string BucketName = "matiasnovillos3";

        readonly AmazonS3Client AmazonS3Client = new(RegionEndpoint.USEast2);

        readonly string FilePath = Path.Combine(
                Environment.CurrentDirectory,
                "Textract-Example-Bill.pdf");

        [Fact]
        public void ThrowWhenFileNameIsNull()
        {
            Functions Functions = new(BucketName, AmazonS3Client);


            Assert.ThrowsAny<Exception>(() => { Functions.SaveFileInAWSS3(null); });
        }

        [Fact]
        public void ThrowWhenFileNameIsEmpty()
        {
            Functions Functions = new(BucketName, AmazonS3Client);


            Assert.ThrowsAny<Exception>(() => { Functions.SaveFileInAWSS3(""); });
        }

        [Fact]
        public void AddFilePath()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            bool isSuccess = Functions.SaveFileInAWSS3(FilePath);

            Assert.True(isSuccess);
        }
    }
}