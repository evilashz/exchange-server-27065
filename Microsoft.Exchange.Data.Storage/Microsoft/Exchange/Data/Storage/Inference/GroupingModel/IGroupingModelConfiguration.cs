using System;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F5F RID: 3935
	public interface IGroupingModelConfiguration
	{
		// Token: 0x170023B2 RID: 9138
		// (get) Token: 0x060086BD RID: 34493
		int CurrentVersion { get; }

		// Token: 0x170023B3 RID: 9139
		// (get) Token: 0x060086BE RID: 34494
		int MinimumSupportedVersion { get; }
	}
}
