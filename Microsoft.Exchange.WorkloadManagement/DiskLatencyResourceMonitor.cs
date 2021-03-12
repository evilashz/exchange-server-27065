using System;
using System.ComponentModel;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000024 RID: 36
	internal class DiskLatencyResourceMonitor : CacheableResourceHealthMonitor, IResourceHealthPoller
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00005C6C File Offset: 0x00003E6C
		public DiskLatencyResourceMonitor(DiskLatencyResourceKey key) : base(key)
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005C7C File Offset: 0x00003E7C
		public TimeSpan Interval
		{
			get
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
				return snapshot.WorkloadManagement.DiskLatencySettings.ResourceHealthPollerInterval;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005CAC File Offset: 0x00003EAC
		public bool IsActive
		{
			get
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
				return snapshot.WorkloadManagement.DiskLatency.Enabled;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005CD9 File Offset: 0x00003ED9
		protected override int InternalMetricValue
		{
			get
			{
				return this.metricValue;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005CE4 File Offset: 0x00003EE4
		public void Execute()
		{
			this.UpdateConfiguration();
			string databaseVolumeName = ((DiskLatencyResourceKey)base.Key).DatabaseVolumeName;
			try
			{
				if (this.lastDiskPerf == default(DiskPerformanceStructure))
				{
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[DiskLatencyResourceMonitor.Execute] MDB: {0}, Volume: {1}. First iteration, capture this.lastDiskPerf only.", base.Key.Id, databaseVolumeName);
					this.lastDiskPerf = DiskIoControl.GetDiskPerformance(databaseVolumeName);
					this.metricValue = -1;
				}
				else
				{
					int diskReadLatency = DiskIoControl.GetDiskReadLatency(databaseVolumeName, ref this.lastDiskPerf, out this.lastUpdatedTime);
					this.diskReadLatencyAverage.Add(Environment.TickCount, (uint)diskReadLatency);
					this.metricValue = (int)Math.Round((double)this.diskReadLatencyAverage.GetValue());
					if (this.lastUpdatedTime > this.LastUpdateUtc + TimeSpan.FromMilliseconds((double)(this.numberOfBuckets * this.windowBucketLength)))
					{
						this.LastUpdateUtc = TimeProvider.UtcNow;
					}
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug((long)this.GetHashCode(), "[DiskLatencyResourceMonitor.Execute] MDB: {0}, Volume: {1}. Disk read latency value is: {2}. Metric value is: {3}", new object[]
					{
						base.Key.Id,
						databaseVolumeName,
						diskReadLatency,
						this.metricValue
					});
				}
			}
			catch (Win32Exception arg)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<string, string, Win32Exception>((long)this.GetHashCode(), "[DiskLatencyResourceMonitor.Execute] MDB: {0}, Volume: {1}. Unable to read disk peformance data. Error: {2}", base.Key.Id, databaseVolumeName, arg);
				this.metricValue = -1;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005E64 File Offset: 0x00004064
		private static ushort ConvertIntSettingToUshort(int settingValue, ushort defaultValue)
		{
			if (0 < settingValue && settingValue < 65535)
			{
				return (ushort)settingValue;
			}
			return defaultValue;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005E78 File Offset: 0x00004078
		private void UpdateConfiguration()
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			IDiskLatencyMonitorSettings diskLatencySettings = snapshot.WorkloadManagement.DiskLatencySettings;
			ushort num = DiskLatencyResourceMonitor.ConvertIntSettingToUshort(diskLatencySettings.FixedTimeAverageWindowBucket.Milliseconds, 1000);
			ushort num2 = DiskLatencyResourceMonitor.ConvertIntSettingToUshort(diskLatencySettings.FixedTimeAverageNumberOfBuckets, 10);
			if (this.windowBucketLength != num || this.numberOfBuckets != num2)
			{
				this.windowBucketLength = num;
				this.numberOfBuckets = num2;
				this.diskReadLatencyAverage = new FixedTimeAverage(this.windowBucketLength, this.numberOfBuckets, Environment.TickCount);
			}
		}

		// Token: 0x040000A0 RID: 160
		private const ushort DefaultWindowBucketLength = 1000;

		// Token: 0x040000A1 RID: 161
		private const ushort DefaultNumberOfBuckets = 10;

		// Token: 0x040000A2 RID: 162
		private ushort windowBucketLength;

		// Token: 0x040000A3 RID: 163
		private ushort numberOfBuckets;

		// Token: 0x040000A4 RID: 164
		private FixedTimeAverage diskReadLatencyAverage;

		// Token: 0x040000A5 RID: 165
		private DateTime lastUpdatedTime;

		// Token: 0x040000A6 RID: 166
		private DiskPerformanceStructure lastDiskPerf;

		// Token: 0x040000A7 RID: 167
		private int metricValue = -1;
	}
}
