using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x0200003F RID: 63
	internal class ResourceHelper
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00006E94 File Offset: 0x00005094
		public static UseLevel TryGetCurrentUseLevel(IEnumerable<ResourceUse> resourceUses, ResourceIdentifier resource, UseLevel defaultUseLevel = UseLevel.Low)
		{
			if (resourceUses != null)
			{
				ResourceUse resourceUse = ResourceHelper.TryGetResourceUse(resourceUses, resource);
				if (resourceUse != null)
				{
					return resourceUse.CurrentUseLevel;
				}
			}
			return defaultUseLevel;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006ED4 File Offset: 0x000050D4
		public static ResourceUse TryGetResourceUse(IEnumerable<ResourceUse> resourceUses, ResourceIdentifier resource)
		{
			if (resourceUses != null)
			{
				return resourceUses.SingleOrDefault((ResourceUse item) => item.Resource == resource);
			}
			return null;
		}
	}
}
