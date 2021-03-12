using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009FA RID: 2554
	internal interface IDatabaseAvailabilityProvider : IResourceLoadMonitor
	{
		// Token: 0x06007663 RID: 30307
		void Update(uint databaseAvailabilityHealth);
	}
}
