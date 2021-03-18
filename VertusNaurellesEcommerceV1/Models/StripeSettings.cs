using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VertusNaurellesEcommerceV1.Models
{
    public class StripeSettings
    {
        //crypto
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
    }
}
