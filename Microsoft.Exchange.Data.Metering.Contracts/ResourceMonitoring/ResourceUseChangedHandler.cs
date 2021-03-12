using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000016 RID: 22
	// (Invoke) Token: 0x0600007C RID: 124
	internal delegate Task ResourceUseChangedHandler(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResources, IEnumerable<ResourceUse> rawResources);
}
