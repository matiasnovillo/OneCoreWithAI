namespace OneCore.CommonFunctions.DTOs
{
    public class BillDTO
    {
        public List<string> lstHeader { get; set; }
        public List<string> lstCell { get; set; }

        public BillDTO(List<string> lstHeader, List<string> lstCell)
        {
            this.lstHeader = lstHeader;
            this.lstCell = lstCell;
        }

    }
}
