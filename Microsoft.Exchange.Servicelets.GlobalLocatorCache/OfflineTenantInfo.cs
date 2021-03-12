using System;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000019 RID: 25
	internal class OfflineTenantInfo
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003FB3 File Offset: 0x000021B3
		public OfflineTenantInfo(Guid tenantId, int partnerId, int minorPartnerId, int resourceForestId, int accountForestId)
		{
			this.TenantId = tenantId;
			this.PartnerId = partnerId;
			this.MinorPartnerId = minorPartnerId;
			this.ResourceForestId = resourceForestId;
			this.AccountForestId = accountForestId;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003FE0 File Offset: 0x000021E0
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003FE8 File Offset: 0x000021E8
		public Guid TenantId { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003FF1 File Offset: 0x000021F1
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003FF9 File Offset: 0x000021F9
		public int PartnerId { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004002 File Offset: 0x00002202
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000400A File Offset: 0x0000220A
		public int MinorPartnerId { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004013 File Offset: 0x00002213
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000401B File Offset: 0x0000221B
		public int ResourceForestId { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004024 File Offset: 0x00002224
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000402C File Offset: 0x0000222C
		public int AccountForestId { get; private set; }
	}
}
