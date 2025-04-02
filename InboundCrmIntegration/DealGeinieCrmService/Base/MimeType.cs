using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DealGeinieCrmService.Base
{
    /// <summary>
    /// 标准MIME类型枚举（包含IANA注册的数值标识）
    /// 数值范围说明：
    ///   10000-19999: 文本类型
    ///   20000-29999: 图像类型
    ///   30000-39999: 音频类型
    ///   40000-49999: 视频类型
    ///   50000-59999: 应用类型
    ///   60000-69999: 消息类型
    ///   70000-79999: 多部分类型
    ///   80000-89999: 自定义扩展类型
    /// Willie Yao - 04/02/2025
    /// </summary>
    public enum MimeType : int
    {
        // ========== 文本类型 (10000-19999) ==========
        /// <summary>
        /// 纯文本 (IANA标准)
        /// </summary>
        [Description("text/plain")]
        PlainText = 10001,

        /// <summary>
        /// HTML文档 (IANA标准)
        /// </summary>
        [Description("text/html")]
        Html = 10002,

        /// <summary>
        /// CSS样式表 (IANA标准)
        /// </summary>
        [Description("text/css")]
        Css = 10003,

        /// <summary>
        /// CSV数据 (IANA标准)
        /// </summary>
        [Description("text/csv")]
        Csv = 10004,

        /// <summary>
        /// JavaScript代码 (IANA标准)
        /// </summary>
        [Description("text/javascript")]
        JavaScript = 10005,

        // ========== 图像类型 (20000-29999) ==========
        /// <summary>
        /// JPEG图像 (IANA标准)
        /// </summary>
        [Description("image/jpeg")]
        Jpeg = 20001,

        /// <summary>
        /// PNG图像 (IANA标准)
        /// </summary>
        [Description("image/png")]
        Png = 20002,

        /// <summary>
        /// GIF图像 (IANA标准)
        /// </summary>
        [Description("image/gif")]
        Gif = 20003,

        /// <summary>
        /// SVG矢量图 (IANA标准)
        /// </summary>
        [Description("image/svg+xml")]
        Svg = 20004,

        // ========== 应用类型 (50000-59999) ==========
        /// <summary>
        /// PDF文档 (IANA标准)
        /// </summary>
        [Description("application/pdf")]
        Pdf = 50001,

        /// <summary>
        /// Word文档 (Microsoft标准)
        /// </summary>
        [Description("application/msword")]
        Doc = 50002,

        /// <summary>
        /// Word OpenXML文档 (ECMA标准)
        /// </summary>
        [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        Docx = 50003,

        /// <summary>
        /// JSON数据 (IANA标准)
        /// </summary>
        [Description("application/json")]
        Json = 50004,

        /// <summary>
        /// 二进制流 (IANA标准)
        /// </summary>
        [Description("application/octet-stream")]
        Binary = 59999,

        // ========== 自定义类型 (80000-89999) ==========
        /// <summary>
        /// 企业自定义文档类型
        /// </summary>
        [Description("application/vnd.company.custom+xml")]
        CompanyCustomDocument = 80001,

        /// <summary>
        /// 加密数据包
        /// </summary>
        [Description("application/encrypted-pkg")]
        EncryptedPackage = 80002
    }

    public static class MimeTypeHelper
    {       
        /// <summary>
        /// 获取枚举值的标准MIME类型字符串
        /// Willie Yao - 04/02/2025
        /// </summary>
        public static string GetMimeString(MimeType mimeType)
        {
            switch (mimeType)
            {
                case MimeType.PlainText: return "text/plain";
                case MimeType.Html: return "text/html";
                case MimeType.Css: return "text/css";
                case MimeType.Png: return "image/png";
                case MimeType.Gif: return "image/gif";
                case MimeType.Jpeg: return "image/jpeg";
                case MimeType.Binary: return "application/octet-stream";
                case MimeType.Svg: return "image/svg+xml";
                case MimeType.Pdf: return "application/pdf";
                case MimeType.Doc: return "application/msword";
                case MimeType.CompanyCustomDocument: return "application/vnd.company.custom+xml";
                case MimeType.Docx: return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case MimeType.Json: return "application/json";
                case MimeType.JavaScript: return "text/javascript";
                case MimeType.EncryptedPackage: return "application/encrypted-pkg";
            }

            return "application/octet-stream";
        }

        /// <summary>
        /// 根据数值获取枚举实例
        /// </summary>
        public static MimeType FromValue(int value)
        {
            return Enum.IsDefined(typeof(MimeType), value)
                ? (MimeType)value
                : throw new ArgumentException($"Invalid MimeType value: {value}");
        }

        /// <summary>
        /// 获取IANA注册状态
        /// </summary>
        public static bool IsIanaRegistered(this MimeType mimeType)
        {
            return (int)mimeType < 80000; // 80000以上是自定义类型
        }
    }
}