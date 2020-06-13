using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMvcClient.Models
{
    public class ConfigModel
    {
        public string Authority { get; set; }
        public string ClentId { get; set; }
        public string Secret { get; set; }
        public string ResponseType { get; set; }
        public bool SaveTokens { get; set; }
    }
}
