using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jsonize_Web.Models
{
    public class ApiModel
    {
        [RegularExpression(@"^(http|https)://")]
        public string Url { get; set; }

        public string Body { get; set; }

        public string Format { get; set; }

        public string EmptyTextNodeHandling { get; set; }

        public string NullValueHandling { get; set; }

        public string TextTrimHandling { get; set; }

        public string ClassAttributeHandling { get; set; }
    }
}
