using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement.EventLogs;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000032 RID: 50
	internal class LogEventIfSlotBlocked : LogEventIf
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00007F75 File Offset: 0x00006175
		public LogEventIfSlotBlocked(IResourceLoadMonitor monitor, ushort numberOfBuckets) : base(LogEventIfSlotBlocked.OneMinute, numberOfBuckets, numberOfBuckets / 2)
		{
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor", "Monitor cannot be null.");
			}
			this.monitor = monitor;
			this.resourceKey = monitor.Key;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007FB0 File Offset: 0x000061B0
		protected override void InternalLogEvent()
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			IResourceSettings @object = snapshot.WorkloadManagement.GetObject<IResourceSettings>(this.resourceKey.MetricType, new object[0]);
			if (@object.Enabled)
			{
				WorkloadManagerEventLogger.LogEvent(WorkloadManagementEventLogConstants.Tuple_StaleResourceMonitor, this.resourceKey.ToString(), new object[]
				{
					this.resourceKey,
					this.monitor.LastUpdateUtc,
					DateTime.UtcNow - this.monitor.LastUpdateUtc
				});
			}
		}

		// Token: 0x040000EA RID: 234
		private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1.0);

		// Token: 0x040000EB RID: 235
		private IResourceLoadMonitor monitor;

		// Token: 0x040000EC RID: 236
		private ResourceKey resourceKey;
	}
}
