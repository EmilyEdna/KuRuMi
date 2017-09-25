using KuRuMi.Mio.DoMain.Infrastructure.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.Common
{
    /// <summary>
    /// 邮箱拓展
    /// </summary>
    public class MailExtension
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="address"></param>
        /// <param name="body"></param>
        public static void SendEmail(string address,string body)
        {
            MailMessage mm = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                //发件人地址
                mm.From=new MailAddress("847432003@qq.com");
                //收件人地址
                mm.To.Add(address);
                //邮件主题
                mm.Subject = "账户信息";
                //邮件标题编码
                mm.SubjectEncoding = Encoding.UTF8;
                //发送邮件的内容
                mm.Body = body;
                //邮件内容编码
                mm.BodyEncoding = Encoding.UTF8;
                //是否是HTML邮件
                mm.IsBodyHtml = true;
                //邮件优先级
                mm.Priority = MailPriority.High;
                //创建一个邮件服务器类
                client.Host = "smtp.qq.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("847432003@qq.com", "mmjqsptyiijcbfaj");
                client.SendMailAsync(mm);
            }
            catch (Exception ex)
            {
                UnitExtension.Log(ex.Message);
            }
            finally
            {
                mm.Dispose();
                client.Dispose();
            }
            
        }
    }
}
