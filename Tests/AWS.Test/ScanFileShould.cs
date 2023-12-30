using Amazon;
using Amazon.S3;
using AWS.Textract;

namespace AWS.Test
{
    public class ScanFileShould
    {
        readonly string BucketName = "[BucketName]";

        readonly AmazonS3Client AmazonS3Client = new(RegionEndpoint.USEast2);

        readonly string ObjectName = "Textract-Example-Bill.pdf";

        #region ScanFileForLines
        [Fact]
        public void ThrowWhenObjectNameIsNullForLines()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            Assert.ThrowsAnyAsync<Exception>(() => 
            { return Functions.ScanFileForLines(null); });
        }

        [Fact]
        public void ThrowWhenObjectNameNameIsEmptyForLines()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            Assert.ThrowsAnyAsync<Exception>(() => 
            { return Functions.ScanFileForLines(""); });
        }

        [Fact]
        public async void AddObjectNameForLines()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            string LinesResult = await Functions
                .ScanFileForLines(ObjectName);

            Assert.NotNull(LinesResult);
            Assert.NotEmpty(LinesResult);
        }
        #endregion

        #region ScanFileForTables
        [Fact]
        public void ThrowWhenObjectNameIsNullForTables()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            Assert.ThrowsAnyAsync<Exception>(() => 
            { return Functions.ScanFileForTables(null); });
        }

        [Fact]
        public void ThrowWhenObjectNameNameIsEmptyForTables()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            Assert.ThrowsAnyAsync<Exception>(() => 
            { return Functions.ScanFileForTables(""); });
        }

        [Fact]
        public async void AddObjectNameForTables()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            Textract.DTOs.HeadersAndCells HeadersAndCells = await Functions
                .ScanFileForTables(ObjectName);

            Assert.NotNull(HeadersAndCells);
        }
        #endregion

        #region ScanFileForForms
        [Fact]
        public void ThrowWhenObjectNameIsNullForForms()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            Assert.ThrowsAnyAsync<Exception>(() =>
            { return Functions.ScanFileForForms(null); });
        }

        [Fact]
        public void ThrowWhenObjectNameNameIsEmptyForForms()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            Assert.ThrowsAnyAsync<Exception>(() =>
            { return Functions.ScanFileForForms(""); });
        }

        [Fact]
        public async void AddObjectNameForForms()
        {
            Functions Functions = new(BucketName, AmazonS3Client);

            List<KeyValuePair<string, string>> lstResult = await Functions
                .ScanFileForForms(ObjectName);

            Assert.NotNull(lstResult);
            Assert.NotEmpty(lstResult);
        }
        #endregion
    }
}
