using System;
using Microsoft.Exchange.EDiscovery.Export;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000015 RID: 21
	internal class SourceMailbox : ISource
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00008AF8 File Offset: 0x00006CF8
		public SourceMailbox(string id, string name, string legacyDN, Uri serviceEndpoint, string queryFilter)
		{
			Util.ThrowIfNullOrEmpty(id, "id");
			Util.ThrowIfNullOrEmpty(name, "name");
			Util.ThrowIfNullOrEmpty(legacyDN, "legacyDN");
			Util.ThrowIfNull(serviceEndpoint, "serviceEndpoint");
			Util.ThrowIfNullOrEmpty(queryFilter, "queryFilter");
			this.Id = id;
			this.Name = name;
			this.LegacyExchangeDN = legacyDN;
			this.ServiceEndpoint = serviceEndpoint;
			this.SourceFilter = queryFilter;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00008B69 File Offset: 0x00006D69
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00008B71 File Offset: 0x00006D71
		public string Id { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00008B7A File Offset: 0x00006D7A
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00008B82 File Offset: 0x00006D82
		public string Name { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00008B8B File Offset: 0x00006D8B
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00008B93 File Offset: 0x00006D93
		public Uri ServiceEndpoint { get; internal set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00008B9C File Offset: 0x00006D9C
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00008BA4 File Offset: 0x00006DA4
		public string LegacyExchangeDN { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00008BAD File Offset: 0x00006DAD
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00008BB5 File Offset: 0x00006DB5
		public string SourceFilter { get; private set; }
	}
}
