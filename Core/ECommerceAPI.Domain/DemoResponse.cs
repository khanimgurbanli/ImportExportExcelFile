using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain
{
    public class DemoResponse<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public static DemoResponse<T> GetResult(int code, string msg, T data = default(T))
        {
            return new DemoResponse<T>
            {
                Code = code,
                Msg = msg,
                Data = data
            };
        }
    }
}
