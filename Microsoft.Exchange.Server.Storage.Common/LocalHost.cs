using System;
using System.Net;
using Microsoft.Exchange.TextProcessing.Boomerang;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200004C RID: 76
	public static class LocalHost
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000CE94 File Offset: 0x0000B094
		public static string Fqdn
		{
			get
			{
				if (LocalHost.fqdn == null)
				{
					string hostName = Dns.GetHostName();
					IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
					LocalHost.fqdn = hostEntry.HostName;
				}
				return LocalHost.fqdn;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000CEC5 File Offset: 0x0000B0C5
		public static string GenerateInternetMessageId(string smtpSenderAddress, Guid organizationId)
		{
			return BoomerangProvider.Instance.GenerateBoomerangMessageId(smtpSenderAddress, LocalHost.Fqdn, organizationId);
		}

		// Token: 0x040004E1 RID: 1249
		private static string fqdn;
	}
}
