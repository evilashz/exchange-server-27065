using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020009F6 RID: 2550
	internal interface IResourceLoadMonitor
	{
		// Token: 0x17002A60 RID: 10848
		// (get) Token: 0x06007653 RID: 30291
		ResourceKey Key { get; }

		// Token: 0x17002A61 RID: 10849
		// (get) Token: 0x06007654 RID: 30292
		DateTime LastUpdateUtc { get; }

		// Token: 0x06007655 RID: 30293
		ResourceLoad GetResourceLoad(WorkloadType type, bool raw = false, object optionalData = null);

		// Token: 0x06007656 RID: 30294
		ResourceLoad GetResourceLoad(WorkloadClassification classification, bool raw = false, object optionalData = null);
	}
}
