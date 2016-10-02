using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jsonize_Web.Models
{
    public class APIModel
    {
        [RegularExpression(@"^(http|https)://")]
        public string url { get; set; }

        public string body { get; set; }
    }
}
