using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000014 RID: 20
	internal class ResourceTrackerDiagnosticsData
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000024C2 File Offset: 0x000006C2
		public TimeSpan GetResourceMeterCallDuration(ResourceIdentifier resource)
		{
			if (this.resourceMeterCallDurations.ContainsKey(resource))
			{
				return this.resourceMeterCallDurations[resource];
			}
			return TimeSpan.Zero;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000024E4 File Offset: 0x000006E4
		public void SetResourceMeterCallDuration(ResourceIdentifier resource, TimeSpan callDuration)
		{
			this.resourceMeterCallDurations[resource] = callDuration;
		}

		// Token: 0x04000018 RID: 24
		private readonly Dictionary<ResourceIdentifier, TimeSpan> resourceMeterCallDurations = new Dictionary<ResourceIdentifier, TimeSpan>();
	}
}
