using System.Collections.Generic;

namespace _175_R
{
    public class ResponseDatas
    {
        public List<ResponseData> responseDatas { get; set; }
    }

    public class ResponseData
    {
        public string symbol { get; set; }
        public string price { get; set; }
    }
}
