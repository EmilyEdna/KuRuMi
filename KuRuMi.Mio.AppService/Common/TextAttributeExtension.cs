using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuRuMi.Mio.AppService.Common
{
    /// <summary>
    /// 自定义特性
    /// </summary>
    public class TextAttributeExtension : Attribute
    {
        public TextAttributeExtension(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}