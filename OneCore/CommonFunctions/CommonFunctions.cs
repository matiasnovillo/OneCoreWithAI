using Microsoft.AspNetCore.Components.Forms;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace OneCore.CommonFunctions
{
    public class CommonFunctions
    {
        
        public long MaxFileSize { get; } = 1024L * 1024L; //3MB max.

        public async Task<bool> SaveFileInLocalServer(IBrowserFile file)
        {
            try
            {
                string path = Path.Combine(
                    Environment.CurrentDirectory,
                    "wwwroot",
                    "Uploads",
                    file.Name);

                await using FileStream FileStream = new(path, FileMode.Create);
                await file.OpenReadStream(MaxFileSize).CopyToAsync(FileStream);

                FileStream.Close();
            }
            catch (Exception) { throw; }
            
            return true;
        }

        public string EncodeString(string stringToEncode)
        {
            try
            {
                UnicodeEncoding Encoder = new();

                string EncodedString = Convert
                    .ToBase64String(SHA512
                    .HashData(Encoder
                    .GetBytes(stringToEncode)));

                return EncodedString;
            }
            catch (Exception) { throw; }
        }

        public string CreateHTMLTables(AWS.Textract.DTOs.HeadersAndCells headersAndCells)
        {
            string TablesToRender = $@"
<table class=""table table-striped table-hover table-bordered table-responsive"">
    <thead>
        <tr>";
            foreach (string header in headersAndCells.lstHeader)
            {
                TablesToRender += $@"
            <th scope=""col"">{header}</th>";
            }
            TablesToRender += $@"
        </tr>
    </thead>
    <tbody>
        
        <tr>";
            foreach (string cell in headersAndCells.lstCell)
            {
                TablesToRender += $@"
            <td>{cell}</td>";
            }
            TablesToRender += $@"
        </tr>
    </tbody>
</table>";

            return TablesToRender;
        }
    
        public string SummarizeTextWithPython(
            string textToAnalyze,
            string pythonExecutablePath,
            string pythonScriptToExecutePath)
        {
            try
            {
                ProcessStartInfo startInfo = new()
                {
                    FileName = pythonExecutablePath,
                    Arguments = $"\"{pythonScriptToExecutePath}\" \"{textToAnalyze}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using Process proceso = new();
                proceso.StartInfo = startInfo;
                proceso.Start();

                string Result = proceso.StandardOutput.ReadToEnd();

                proceso.WaitForExit();

                return Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DescribeTextWithPython(
            string textToAnalyze,
            string pythonExecutablePath,
            string pythonScriptToExecutePath)
        {
            try
            {
                ProcessStartInfo startInfo = new()
                {
                    FileName = pythonExecutablePath,
                    Arguments = $"\"{pythonScriptToExecutePath}\" \"{textToAnalyze}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using Process proceso = new();
                proceso.StartInfo = startInfo;
                proceso.Start();

                string Result = proceso.StandardOutput.ReadToEnd();

                proceso.WaitForExit();

                return Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
