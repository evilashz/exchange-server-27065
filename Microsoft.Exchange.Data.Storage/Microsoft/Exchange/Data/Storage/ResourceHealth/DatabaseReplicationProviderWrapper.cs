using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B2C RID: 2860
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseReplicationProviderWrapper : ResourceHealthMonitorWrapper, IDatabaseReplicationProvider, IResourceLoadMonitor
	{
		// Token: 0x0600678A RID: 26506 RVA: 0x001B5837 File Offset: 0x001B3A37
		public DatabaseReplicationProviderWrapper(MdbReplicationResourceHealthMonitor provider) : base(provider)
		{
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x001B5840 File Offset: 0x001B3A40
		public void Update(uint databaseReplicationHealth)
		{
			base.CheckExpired();
			MdbReplicationResourceHealthMonitor wrappedMonitor = base.GetWrappedMonitor<MdbReplicationResourceHealthMonitor>();
			wrappedMonitor.Update(databaseReplicationHealth);
		}
	}
}
