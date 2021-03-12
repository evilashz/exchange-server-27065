using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000AE RID: 174
	internal class InternalExchangeServer
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x00018F91 File Offset: 0x00017191
		public InternalExchangeServer(Server s)
		{
			this.server = s;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00018FA0 File Offset: 0x000171A0
		public string Fqdn
		{
			get
			{
				return this.server.Fqdn;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00018FAD File Offset: 0x000171AD
		public ADObjectId Id
		{
			get
			{
				return this.server.Id;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00018FBA File Offset: 0x000171BA
		public ADObjectId ServerSite
		{
			get
			{
				return this.server.ServerSite;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00018FC7 File Offset: 0x000171C7
		public ServerStatus Status
		{
			get
			{
				return this.server.Status;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00018FD4 File Offset: 0x000171D4
		public Server Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00018FDC File Offset: 0x000171DC
		internal string LegacyDN
		{
			get
			{
				return this.server.ExchangeLegacyDN;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00018FE9 File Offset: 0x000171E9
		internal string MachineName
		{
			get
			{
				return this.server.Name;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00018FF8 File Offset: 0x000171F8
		public override bool Equals(object obj)
		{
			InternalExchangeServer internalExchangeServer = obj as InternalExchangeServer;
			return obj != null && this.Id.Equals(internalExchangeServer.Id);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00019022 File Offset: 0x00017222
		public override int GetHashCode()
		{
			return this.server.Id.GetHashCode();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00019034 File Offset: 0x00017234
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				this.server.Fqdn,
				this.Status.ToString()
			});
		}

		// Token: 0x04000391 RID: 913
		private Server server;
	}
}
