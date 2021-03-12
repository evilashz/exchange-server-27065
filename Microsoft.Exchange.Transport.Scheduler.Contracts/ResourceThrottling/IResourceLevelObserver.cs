using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x0200000A RID: 10
	internal interface IResourceLevelObserver
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23
		string Name { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24
		bool Paused { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25
		string SubStatus { get; }

		// Token: 0x0600001A RID: 26
		void HandleResourceChange(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses);
	}
}
