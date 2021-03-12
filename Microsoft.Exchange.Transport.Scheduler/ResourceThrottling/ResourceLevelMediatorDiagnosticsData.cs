using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000004 RID: 4
	internal class ResourceLevelMediatorDiagnosticsData
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000254B File Offset: 0x0000074B
		public TimeSpan GetResourceObserverCallDuration(string resource)
		{
			if (this.resourceObserverCallDurations.ContainsKey(resource))
			{
				return this.resourceObserverCallDurations[resource];
			}
			return TimeSpan.Zero;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000256D File Offset: 0x0000076D
		public void SetResourceObserverCallDuration(string resource, TimeSpan callDuration)
		{
			this.resourceObserverCallDurations[resource] = callDuration;
		}

		// Token: 0x0400000E RID: 14
		private readonly Dictionary<string, TimeSpan> resourceObserverCallDurations = new Dictionary<string, TimeSpan>();
	}
}
