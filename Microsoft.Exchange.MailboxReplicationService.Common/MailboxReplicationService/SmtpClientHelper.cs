using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.ExSmtpClient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000261 RID: 609
	internal sealed class SmtpClientHelper
	{
		// Token: 0x06001EEA RID: 7914 RVA: 0x000400B4 File Offset: 0x0003E2B4
		public static void Submit(SendAction sendAction, string host, int port, NetworkCredential credentials)
		{
			if (string.IsNullOrEmpty(host) || port == 0)
			{
				throw new SmtpServerInfoMissingException();
			}
			using (SmtpTalk smtpTalk = new SmtpTalk(SmtpClientHelper.DebugOutput))
			{
				smtpTalk.Connect(host, port);
				smtpTalk.Ehlo();
				smtpTalk.StartTls(false);
				smtpTalk.Ehlo();
				smtpTalk.Authenticate(credentials, SmtpSspiMechanism.Login);
				smtpTalk.MailFrom("<" + credentials.UserName + ">", null);
				foreach (string str in sendAction.Recipients)
				{
					smtpTalk.RcptTo("<" + str + ">", null);
				}
				byte[] data = sendAction.Data;
				using (MemoryStream memoryStream = new MemoryStream(data.Length))
				{
					memoryStream.Write(data, 0, data.Length);
					memoryStream.Position = 0L;
					smtpTalk.Chunking(memoryStream);
				}
				smtpTalk.Quit();
			}
		}

		// Token: 0x04000C7A RID: 3194
		private static readonly SmtpClientHelper.SmtpClientDebugOutput DebugOutput = new SmtpClientHelper.SmtpClientDebugOutput();

		// Token: 0x02000262 RID: 610
		private class SmtpClientDebugOutput : ISmtpClientDebugOutput
		{
			// Token: 0x06001EED RID: 7917 RVA: 0x000401D8 File Offset: 0x0003E3D8
			void ISmtpClientDebugOutput.Output(Trace tracer, object context, string message, params object[] args)
			{
				if (!string.IsNullOrEmpty(message))
				{
					MrsTracer.Common.Debug(message, args);
				}
			}
		}
	}
}
