using System;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000055 RID: 85
	internal class ShadowRedundancyResourceLevelObserver : ResourceLevelObserver
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000B067 File Offset: 0x00009267
		public ShadowRedundancyResourceLevelObserver(ShadowRedundancyComponent shadowRedundancyComponent) : base("ShadowRedundancy", shadowRedundancyComponent, null)
		{
		}

		// Token: 0x04000162 RID: 354
		internal const string ResourceObserverName = "ShadowRedundancy";
	}
}
