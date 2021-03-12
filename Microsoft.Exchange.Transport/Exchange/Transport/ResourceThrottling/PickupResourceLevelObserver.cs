using System;
using Microsoft.Exchange.Transport.Pickup;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000034 RID: 52
	internal class PickupResourceLevelObserver : ResourceLevelObserver
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000513F File Offset: 0x0000333F
		public PickupResourceLevelObserver(PickupComponent pickupComponent) : base("Pickup", pickupComponent, null)
		{
		}

		// Token: 0x0400008A RID: 138
		internal const string ResourceObserverName = "Pickup";
	}
}
