namespace _175_R
{
    public class DataClass
    {
        public string name { get; set; }
        public double price_now { get; set; }
        public double price_last_5min { get; set; }
        public double price_last_10min { get; set; }
        public double price_last_15min { get; set; }
        public double price_last_20min { get; set; }
        public int price_up_count { get; set; }
        public int is_price_up { get; set; }
    }

    public class TableDataClass
    {
        public string column_brand { get; set; }
        public string column_price { get; set; }
        public string column_continue { get; set; }
        public string column_up_ratio { get; set; }
    }

}
