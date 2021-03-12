using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000026 RID: 38
	internal class ProcessorResourceLoadMonitor : CacheableResourceHealthMonitor, IResourceHealthPoller
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00005F2C File Offset: 0x0000412C
		internal ProcessorResourceLoadMonitor(ProcessorResourceKey key) : base(key)
		{
			this.cpuAverage = new FixedTimeAverage(1000, (ushort)ProcessorResourceLoadMonitorConfiguration.CPUAverageTimeWindow, Environment.TickCount);
			if (CPUUsage.GetCurrentCPU(ref this.lastServerCPUUsage))
			{
				this.lastUpdatedTime = DateTime.UtcNow;
				return;
			}
			this.lastServerCPUUsage = 0L;
			this.lastUpdatedTime = DateTime.MinValue;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005F8E File Offset: 0x0000418E
		public bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005F91 File Offset: 0x00004191
		public virtual TimeSpan Interval
		{
			get
			{
				return ProcessorResourceLoadMonitorConfiguration.RefreshInterval;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005F98 File Offset: 0x00004198
		protected override int InternalMetricValue
		{
			get
			{
				return this.metricValue;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005FA0 File Offset: 0x000041A0
		public override ResourceLoad GetResourceLoad(WorkloadType type, bool raw = false, object optionalData = null)
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			IWorkloadSettings @object = snapshot.WorkloadManagement.GetObject<IWorkloadSettings>(type, new object[0]);
			if (!@object.Enabled)
			{
				return ResourceLoad.Critical;
			}
			if (!@object.EnabledDuringBlackout)
			{
				IBlackoutSettings blackout = snapshot.WorkloadManagement.Blackout;
				bool flag = blackout.StartTime != blackout.EndTime;
				if (flag)
				{
					DateTime utcNow = DateTime.UtcNow;
					DateTime t = utcNow.Date + blackout.StartTime;
					DateTime t2 = utcNow.Date + blackout.EndTime;
					if (t >= t2)
					{
						t2 = t2.AddDays(1.0);
					}
					if (t < utcNow && utcNow < t2)
					{
						return ResourceLoad.Critical;
					}
				}
			}
			return base.GetResourceLoad(type, raw, optionalData);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006085 File Offset: 0x00004285
		public virtual void Execute()
		{
			this.Update();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000608D File Offset: 0x0000428D
		public override bool ShouldRemoveResourceFromCache()
		{
			return false;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006090 File Offset: 0x00004290
		private void Update()
		{
			if (!ProcessorResourceLoadMonitorConfiguration.Enabled)
			{
				this.metricValue = -1;
				return;
			}
			float num;
			if (CPUUsage.CalculateCPUUsagePercentage(ref this.lastUpdatedTime, ref this.lastServerCPUUsage, out num))
			{
				this.lastServerCPUUsagePercentage = (uint)Math.Round((double)num);
				this.cpuAverage.Add(Environment.TickCount, this.lastServerCPUUsagePercentage);
				this.metricValue = (int)Math.Round((double)this.cpuAverage.GetValue());
				if (this.lastUpdatedTime > this.LastUpdateUtc + TimeSpan.FromSeconds((double)ProcessorResourceLoadMonitorConfiguration.CPUAverageTimeWindow))
				{
					this.LastUpdateUtc = TimeProvider.UtcNow;
				}
			}
			else
			{
				this.metricValue = -1;
			}
			if (ProcessorResourceLoadMonitorConfiguration.OverrideMetricValue != null)
			{
				this.metricValue = ProcessorResourceLoadMonitorConfiguration.OverrideMetricValue.Value;
			}
		}

		// Token: 0x040000A9 RID: 169
		private const int MillisecondsInOneSecond = 1000;

		// Token: 0x040000AA RID: 170
		private int metricValue = -1;

		// Token: 0x040000AB RID: 171
		private DateTime lastUpdatedTime;

		// Token: 0x040000AC RID: 172
		private long lastServerCPUUsage;

		// Token: 0x040000AD RID: 173
		private uint lastServerCPUUsagePercentage;

		// Token: 0x040000AE RID: 174
		private FixedTimeAverage cpuAverage;
	}
}
