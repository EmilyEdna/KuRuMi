using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuRuMi.Mio.AppService.Common
{
    /// <summary>
    /// 枚举状态
    /// </summary>
    public enum StatusCodeEnum
    {
        [TextAttributeExtension("请求(或处理)成功")]
        Success = 200, //请求(或处理)成功

        [TextAttributeExtension("内部请求出错")]
        Error = 500, //内部请求出错

        [TextAttributeExtension("未授权标识")]
        Unauthorized = 401,//未授权标识

        [TextAttributeExtension("请求参数不完整或不正确")]
        ParameterError = 400,//请求参数不完整或不正确

        [TextAttributeExtension("请求TOKEN失效")]
        TokenInvalid = 403,//请求TOKEN失效

        [TextAttributeExtension("HTTP请求类型不合法")]
        HttpMehtodError = 405,//HTTP请求类型不合法

        [TextAttributeExtension("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,//HTTP请求不合法

        [TextAttributeExtension("该URL已经失效")]
        URLExpireError = 407,//HTTP请求不合法
    }
}