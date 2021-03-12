using System;
using System.Collections.Generic;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x0200000C RID: 12
	internal class ServerLocatorReturn
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00003024 File Offset: 0x00001224
		internal ServerLocatorReturn()
		{
			this.ServerFqdn = string.Empty;
			this.ServerVersion = null;
			this.SuccessKey = null;
			this.RoutingEntries = null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000305F File Offset: 0x0000125F
		internal ServerLocatorReturn(string serverFqdn, int? serverVersion, IRoutingKey successKey, IList<IRoutingEntry> routingEntries)
		{
			this.ServerFqdn = serverFqdn;
			this.ServerVersion = serverVersion;
			this.SuccessKey = successKey;
			this.RoutingEntries = routingEntries;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003084 File Offset: 0x00001284
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000308C File Offset: 0x0000128C
		public string ServerFqdn { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003095 File Offset: 0x00001295
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000309D File Offset: 0x0000129D
		public int? ServerVersion { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000030A6 File Offset: 0x000012A6
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000030AE File Offset: 0x000012AE
		public IRoutingKey SuccessKey { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000030B7 File Offset: 0x000012B7
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000030BF File Offset: 0x000012BF
		public IList<IRoutingEntry> RoutingEntries { get; set; }
	}
}
