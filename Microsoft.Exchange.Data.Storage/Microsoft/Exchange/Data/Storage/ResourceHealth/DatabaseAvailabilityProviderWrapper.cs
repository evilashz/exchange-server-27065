using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B28 RID: 2856
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseAvailabilityProviderWrapper : ResourceHealthMonitorWrapper, IDatabaseAvailabilityProvider, IResourceLoadMonitor
	{
		// Token: 0x0600677E RID: 26494 RVA: 0x001B56EC File Offset: 0x001B38EC
		public DatabaseAvailabilityProviderWrapper(MdbAvailabilityResourceHealthMonitor provider) : base(provider)
		{
		}

		// Token: 0x0600677F RID: 26495 RVA: 0x001B56F8 File Offset: 0x001B38F8
		public void Update(uint databaseAvailabilityHealth)
		{
			base.CheckExpired();
			MdbAvailabilityResourceHealthMonitor wrappedMonitor = base.GetWrappedMonitor<MdbAvailabilityResourceHealthMonitor>();
			wrappedMonitor.Update(databaseAvailabilityHealth);
		}
	}
}
