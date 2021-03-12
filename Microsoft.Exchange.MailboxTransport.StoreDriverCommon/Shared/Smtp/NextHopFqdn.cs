using System;
using System.Net;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MailboxTransport.Shared.Smtp
{
	// Token: 0x02000025 RID: 37
	internal class NextHopFqdn : INextHopServer
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x0000704E File Offset: 0x0000524E
		public NextHopFqdn(string fqdn, bool isFrontendAndHubColocatedServer)
		{
			this.fqdn = fqdn;
			this.isFrontendAndHubColocatedServer = isFrontendAndHubColocatedServer;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00007064 File Offset: 0x00005264
		bool INextHopServer.IsIPAddress
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00007067 File Offset: 0x00005267
		IPAddress INextHopServer.Address
		{
			get
			{
				throw new InvalidOperationException("INextHopServer.Address must not be requested from NextHopFqdn objects");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00007073 File Offset: 0x00005273
		string INextHopServer.Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000707B File Offset: 0x0000527B
		bool INextHopServer.IsFrontendAndHubColocatedServer
		{
			get
			{
				return this.isFrontendAndHubColocatedServer;
			}
		}

		// Token: 0x04000073 RID: 115
		private readonly string fqdn;

		// Token: 0x04000074 RID: 116
		private readonly bool isFrontendAndHubColocatedServer;
	}
}
