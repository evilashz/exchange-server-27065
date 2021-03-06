using System;
using System.Net;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200020F RID: 527
	public class BasicSmtpClientFactory : IMinimalSmtpClientFactory
	{
		// Token: 0x0600100E RID: 4110 RVA: 0x0002BC20 File Offset: 0x00029E20
		public IMinimalSmtpClient CreateSmtpClient(string host, SmtpProbeWorkDefinition workDefinition, DelTraceDebug traceDebug)
		{
			SmtpProbeWorkDefinition.SendMailDefinition sendMail = workDefinition.SendMail;
			IMinimalSmtpClient minimalSmtpClient2;
			if (workDefinition.ClientCertificate == null)
			{
				IMinimalSmtpClient minimalSmtpClient = new SmtpClientWrapper(host);
				minimalSmtpClient2 = minimalSmtpClient;
			}
			else
			{
				minimalSmtpClient2 = new RawSmtpClientWrapper(host, workDefinition, traceDebug);
			}
			IMinimalSmtpClient minimalSmtpClient3 = minimalSmtpClient2;
			minimalSmtpClient3.Port = sendMail.Port;
			minimalSmtpClient3.EnableSsl = sendMail.EnableSsl;
			minimalSmtpClient3.UseDefaultCredentials = (sendMail.Anonymous || string.IsNullOrEmpty(sendMail.SenderPassword));
			if (!minimalSmtpClient3.UseDefaultCredentials)
			{
				minimalSmtpClient3.Credentials = new NetworkCredential(sendMail.SenderUsername, sendMail.SenderPassword);
			}
			if (sendMail.Timeout > 0)
			{
				minimalSmtpClient3.Timeout = sendMail.Timeout * 1000;
			}
			return minimalSmtpClient3;
		}
	}
}
