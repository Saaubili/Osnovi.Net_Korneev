namespace Application
{

    public class ClientReportRow
    {
        public long ClientId { get; set; }
        public float OrdersSum { get; set; }
        public string FavoriteItemName { get; set; } = "";
    }
}