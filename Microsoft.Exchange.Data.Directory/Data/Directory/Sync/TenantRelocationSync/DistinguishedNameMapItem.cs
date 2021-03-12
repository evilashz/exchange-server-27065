using System;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007FF RID: 2047
	internal class DistinguishedNameMapItem
	{
		// Token: 0x170023CE RID: 9166
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x001601BE File Offset: 0x0015E3BE
		// (set) Token: 0x06006501 RID: 25857 RVA: 0x001601C6 File Offset: 0x0015E3C6
		public ADObjectId SourceDN { get; private set; }

		// Token: 0x170023CF RID: 9167
		// (get) Token: 0x06006502 RID: 25858 RVA: 0x001601CF File Offset: 0x0015E3CF
		// (set) Token: 0x06006503 RID: 25859 RVA: 0x001601D7 File Offset: 0x0015E3D7
		public ADObjectId TargetDN { get; private set; }

		// Token: 0x170023D0 RID: 9168
		// (get) Token: 0x06006504 RID: 25860 RVA: 0x001601E0 File Offset: 0x0015E3E0
		// (set) Token: 0x06006505 RID: 25861 RVA: 0x001601E8 File Offset: 0x0015E3E8
		public Guid CorrelationId { get; private set; }

		// Token: 0x06006506 RID: 25862 RVA: 0x001601F1 File Offset: 0x0015E3F1
		public DistinguishedNameMapItem(ADObjectId source, ADObjectId target, Guid correlationId)
		{
			this.SourceDN = source;
			this.TargetDN = target;
			this.CorrelationId = correlationId;
		}
	}
}
