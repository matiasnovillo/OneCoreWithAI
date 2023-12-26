
using Microsoft.AspNetCore.Components.Forms;
using OneCore.DTOs;

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
            catch (Exception)
            {
                throw;
            }
            
            return true;
        }

        public string CreateHTMLTables(BillDTO billDTO)
        {
            string TablesToRender = $@"
<table class=""table table-striped table-hover table-bordered table-responsive"">
    <thead>
        <tr>";
            foreach (string header in billDTO.lstHeader)
            {
                TablesToRender += $@"
            <th scope=""col"">{header}</th>";
            }
            TablesToRender += $@"
        </tr>
    </thead>
    <tbody>
        
        <tr>";
            foreach (string cell in billDTO.lstCell)
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
    }
}
