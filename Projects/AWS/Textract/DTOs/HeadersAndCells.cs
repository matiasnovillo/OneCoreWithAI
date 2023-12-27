namespace AWS.Textract.DTOs
{
    public class HeadersAndCells
    {
        public List<string> lstHeader { get; set; }
        public List<string> lstCell { get; set; }

        public HeadersAndCells(List<string> lstHeader, List<string> lstCell)
        {
            this.lstHeader = lstHeader;
            this.lstCell = lstCell;
        }

    }
}
