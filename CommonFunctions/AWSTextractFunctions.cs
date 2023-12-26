using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Textract;
using OneCore.DTOs;

namespace OneCore.CommonFunctions
{
    public class AWSTextractFunctions
    {
        public string BucketName { get; } = "matiasnovillos3";
        IAmazonS3 RegionEndPoint = new AmazonS3Client(RegionEndpoint.USEast2);

        public void SaveFileInAWSS3(string fileName)
        {
            string path = Path.Combine(
                Environment.CurrentDirectory,
                "wwwroot",
                "Uploads",
                fileName);

            TransferUtility TransferUtility = new(RegionEndPoint);
            TransferUtilityUploadRequest TransferUtilityUploadRequest = new()
            {
                BucketName = BucketName,
                Key = fileName
            };

            try
            {
                MemoryStream MemoryStream = new();
                Stream Stream = File.OpenRead(path);

                Stream.CopyTo(MemoryStream);
                MemoryStream.Seek(0, SeekOrigin.Begin);
                byte[] buf = new byte[MemoryStream.Length];
                MemoryStream.Read(buf, 0, buf.Length);

                TransferUtilityUploadRequest.InputStream = MemoryStream;
                TransferUtility.Upload(TransferUtilityUploadRequest);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                TransferUtilityUploadRequest.InputStream.Close();
            }
        }

        public async Task<string> ScanFileForLines(string objectName)
        {
            string ScannedText = "";

            AmazonTextractClient AmazonTextractClient = new();

            var TextractResults = await AmazonTextractClient
                .DetectDocumentTextAsync(new Amazon.Textract.Model.DetectDocumentTextRequest()
                {
                    Document = new Amazon.Textract.Model.Document()
                    {
                        S3Object = new Amazon.Textract.Model.S3Object()
                        {
                            Bucket = BucketName,
                            Name = objectName
                        }
                    }
                });

            foreach (var block in TextractResults.Blocks)
            {
                if (block.BlockType == BlockType.LINE)
                {
                    ScannedText += $@"{block.Text}{Environment.NewLine}";
                }
            }

            return ScannedText;
        }

        public async Task<BillDTO> ScanFileForTables(string objectName)
        {
            AmazonTextractClient AmazonTextractClient = new();

            var TextractResults = await AmazonTextractClient
                .AnalyzeDocumentAsync(new Amazon.Textract.Model.AnalyzeDocumentRequest()
                {
                    Document = new Amazon.Textract.Model.Document()
                    {
                        S3Object = new Amazon.Textract.Model.S3Object()
                        {
                            Bucket = BucketName,
                            Name = objectName
                        }
                    },
                    FeatureTypes = ["TABLES"],
                });

            List<string> lstHeader = [];
            List<string> lstCell = [];

            foreach (var block in TextractResults.Blocks)
            {
                if (block.BlockType == BlockType.TABLE)
                {
                    //Access rows and cells of the table
                    foreach (var relationship in block.Relationships)
                    {
                        if (relationship.Type == "CHILD")
                        {
                            foreach (var childId in relationship.Ids)
                            {
                                //Access each cell of the table
                                foreach (var cellItem in TextractResults.Blocks)
                                {
                                    if (cellItem.Id == childId && cellItem.BlockType == BlockType.CELL)
                                    {

                                        foreach (var relationship1 in cellItem.Relationships)
                                        {
                                            if (relationship1.Type == "CHILD")
                                            {
                                                foreach (var childId1 in relationship1.Ids)
                                                {
                                                    //Access each block of the cell
                                                    foreach (var wordItem in TextractResults.Blocks)
                                                    {
                                                        if (wordItem.Id == childId1 && wordItem.BlockType == BlockType.WORD)
                                                        {
                                                            if (cellItem.EntityTypes.Count != 0)
                                                            {
                                                                if (cellItem.EntityTypes[0] == "COLUMN_HEADER")
                                                                {
                                                                    lstHeader.Add(wordItem.Text);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                lstCell.Add(wordItem.Text);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return new BillDTO(lstHeader, lstCell);
        }

        public async Task<List<KeyValuePair<string, string>>> ScanFileForForms(string objectName)
        {
            AmazonTextractClient AmazonTextractClient = new();

            var TextractResults = await AmazonTextractClient.
                AnalyzeDocumentAsync(new Amazon.Textract.Model.AnalyzeDocumentRequest()
                {
                    Document = new Amazon.Textract.Model.Document()
                    {
                        S3Object = new Amazon.Textract.Model.S3Object()
                        {
                            Bucket = BucketName,
                            Name = objectName
                        }
                    },
                    FeatureTypes = ["FORMS"]
                });

            List<KeyValuePair<string, string>> KeyValuePairResult = [];

            var KeyValueFromTextractResults = (from x in TextractResults.Blocks
                                               where x.BlockType == BlockType.KEY_VALUE_SET
                                               select x).ToArray();

            foreach (var keyValueFromTextractResults in KeyValueFromTextractResults)
            {
                string keyForKeyValuePair = "";
                string valueForKeyValuePair = "";

                var keyRawFromBlock = (from k in keyValueFromTextractResults.Relationships
                                       where k.Type == RelationshipType.CHILD
                                       select k).FirstOrDefault();

                var valueRawFromBlock = (from v in keyValueFromTextractResults.Relationships
                                         where v.Type == RelationshipType.VALUE
                                         select v).FirstOrDefault();

                if (keyRawFromBlock != null)
                {
                    foreach (string keyIdFromBlock in keyRawFromBlock.Ids)
                    {
                        var keyElementFromBlock = (from k in TextractResults.Blocks
                                                   where k.Id == keyIdFromBlock
                                                   select k).FirstOrDefault();

                        keyForKeyValuePair += $@"{keyElementFromBlock.Text} ";
                    }
                }

                if (valueRawFromBlock != null)
                {
                    var valueElement = (from x in TextractResults.Blocks
                                        where x.Id == valueRawFromBlock.Ids[0]
                                        select x).FirstOrDefault();

                    if (valueElement.Relationships.Count > 0)
                    {
                        foreach (var valueElementId in valueElement.Relationships[0].Ids)
                        {
                            var valueElementFromBlock = (from x in TextractResults.Blocks
                                                         where x.Id == valueElementId
                                                         select x).FirstOrDefault();
                            valueForKeyValuePair += $@"{valueElementFromBlock.Text} ";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(keyForKeyValuePair.ToString()) &&
                    !string.IsNullOrEmpty(valueForKeyValuePair.ToString()))
                {
                    KeyValuePairResult.Add(
                        new KeyValuePair<string, string>(
                        keyForKeyValuePair.ToString(),
                        valueForKeyValuePair.ToString()));
                }
            }

            return KeyValuePairResult;
        }

        public async Task<List<KeyValuePair<string, string>>> ScanFileForLinesAndWordsCustom(string objectName)
        {
            List<KeyValuePair<string, string>> ListKeyValuePairResult = [];

            AmazonTextractClient AmazonTextractClient = new();

            var TextractResults = await AmazonTextractClient
                .DetectDocumentTextAsync(new Amazon.Textract.Model.DetectDocumentTextRequest()
                {
                    Document = new Amazon.Textract.Model.Document()
                    {
                        S3Object = new Amazon.Textract.Model.S3Object()
                        {
                            Bucket = BucketName,
                            Name = objectName
                        }
                    }
                });

            if (TextractResults.Blocks[14].ToString() == "I.V A RESPONSABLE INSCRIPTO Agente de Retención R.G 18/97")
            {
                throw new Exception("El archivo subido y analizado no es la factura customizada");
            }

            ListKeyValuePairResult.Add(
                        new KeyValuePair<string, string>(
                            "ClientName",
                            TextractResults.Blocks[14].Text));

            ListKeyValuePairResult.Add(
                        new KeyValuePair<string, string>(
                            "ClientAddress",
                            TextractResults.Blocks[17].Text +
                            " - " +
                            TextractResults.Blocks[19].Text));

            ListKeyValuePairResult.Add(
                        new KeyValuePair<string, string>(
                            "ProviderName",
                            TextractResults.Blocks[1].Text));

            ListKeyValuePairResult.Add(
                        new KeyValuePair<string, string>(
                            "ProviderAddress",
                            TextractResults.Blocks[3].Text.
                            Replace("Domicilio Comercial: ", "")));

            ListKeyValuePairResult.Add(
                    new KeyValuePair<string, string>(
                        "BillingTotal",
                        TextractResults.Blocks[15].Text));

            ListKeyValuePairResult.Add(
                    new KeyValuePair<string, string>(
                        "BillingNumber",
                        TextractResults.Blocks[381].Text));

            return ListKeyValuePairResult;
        }
    }
}
