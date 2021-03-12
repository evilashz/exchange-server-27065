using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000284 RID: 644
	internal class WlmHealthSLA
	{
		// Token: 0x06001FBE RID: 8126 RVA: 0x00043681 File Offset: 0x00041881
		public WlmHealthSLA() : this(TimeSpan.FromMinutes(30.0))
		{
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00043698 File Offset: 0x00041898
		public WlmHealthSLA(TimeSpan windowWidth)
		{
			this.avg5min = new WlmHealthSLA.Bucketizer(TimeSpan.FromMinutes(5.0));
			this.avg1hour = new WlmHealthSLA.Bucketizer(TimeSpan.FromHours(1.0));
			this.avg1day = new WlmHealthSLA.Bucketizer(TimeSpan.FromDays(1.0));
			this.CustomTimeInterval = windowWidth;
			this.avgCustom = new WlmHealthSLA.Bucketizer(this.CustomTimeInterval);
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0004370E File Offset: 0x0004190E
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x00043716 File Offset: 0x00041916
		public TimeSpan CustomTimeInterval { get; private set; }

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0004371F File Offset: 0x0004191F
		public void AddSample(ResourceLoadState healthState, int currentCapacity)
		{
			this.avg5min.AddSample(healthState, currentCapacity);
			this.avg1hour.AddSample(healthState, currentCapacity);
			this.avg1day.AddSample(healthState, currentCapacity);
			this.avgCustom.AddSample(healthState, currentCapacity);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00043758 File Offset: 0x00041958
		public WlmHealthStatistics GetStats()
		{
			return new WlmHealthStatistics
			{
				Avg5Min = this.avg5min.GetStats(),
				Avg1Hour = this.avg1hour.GetStats(),
				Avg1Day = this.avg1day.GetStats()
			};
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0004379F File Offset: 0x0004199F
		public WlmHealthCounters GetCustomHealthCounters()
		{
			return this.avgCustom.GetHealthCounters();
		}

		// Token: 0x04000CCE RID: 3278
		private const ushort OneMinuteInMilliseconds = 60000;

		// Token: 0x04000CCF RID: 3279
		private WlmHealthSLA.Bucketizer avg5min;

		// Token: 0x04000CD0 RID: 3280
		private WlmHealthSLA.Bucketizer avg1hour;

		// Token: 0x04000CD1 RID: 3281
		private WlmHealthSLA.Bucketizer avg1day;

		// Token: 0x04000CD2 RID: 3282
		private WlmHealthSLA.Bucketizer avgCustom;

		// Token: 0x02000285 RID: 645
		private class Bucketizer
		{
			// Token: 0x06001FC5 RID: 8133 RVA: 0x000437AC File Offset: 0x000419AC
			public Bucketizer(TimeSpan windowWidth)
			{
				ushort numberOfBuckets = (ushort)windowWidth.TotalMinutes;
				this.healthValues = new Dictionary<ResourceLoadState, FixedTimeSum>();
				foreach (object obj in Enum.GetValues(typeof(ResourceLoadState)))
				{
					ResourceLoadState key = (ResourceLoadState)obj;
					this.healthValues[key] = new FixedTimeSum(60000, numberOfBuckets);
				}
				this.averageCapacity = new FixedTimeAverage(60000, numberOfBuckets, Environment.TickCount);
			}

			// Token: 0x06001FC6 RID: 8134 RVA: 0x00043850 File Offset: 0x00041A50
			public void AddSample(ResourceLoadState healthState, int currentCapacity)
			{
				this.healthValues[healthState].Add(1U);
				this.averageCapacity.Add((uint)currentCapacity);
			}

			// Token: 0x06001FC7 RID: 8135 RVA: 0x00043870 File Offset: 0x00041A70
			public WlmHealthStatistics.HealthAverages GetStats()
			{
				WlmHealthStatistics.HealthAverages healthAverages = new WlmHealthStatistics.HealthAverages();
				uint value = this.healthValues[ResourceLoadState.Underloaded].GetValue();
				uint value2 = this.healthValues[ResourceLoadState.Full].GetValue();
				uint value3 = this.healthValues[ResourceLoadState.Overloaded].GetValue();
				uint value4 = this.healthValues[ResourceLoadState.Critical].GetValue();
				uint value5 = this.healthValues[ResourceLoadState.Unknown].GetValue();
				uint total = value + value2 + value3 + value4 + value5;
				healthAverages.Full = WlmHealthSLA.Bucketizer.GetPercentage(value2, total);
				healthAverages.Overloaded = WlmHealthSLA.Bucketizer.GetPercentage(value3, total);
				healthAverages.Critical = WlmHealthSLA.Bucketizer.GetPercentage(value4, total);
				healthAverages.Unknown = WlmHealthSLA.Bucketizer.GetPercentage(value5, total);
				healthAverages.Underloaded = 100 - healthAverages.Full - healthAverages.Overloaded - healthAverages.Critical - healthAverages.Unknown;
				healthAverages.AverageCapacity = this.averageCapacity.GetValue();
				return healthAverages;
			}

			// Token: 0x06001FC8 RID: 8136 RVA: 0x0004395C File Offset: 0x00041B5C
			public WlmHealthCounters GetHealthCounters()
			{
				return new WlmHealthCounters
				{
					UnderloadedCounter = this.healthValues[ResourceLoadState.Underloaded].GetValue(),
					FullCounter = this.healthValues[ResourceLoadState.Full].GetValue(),
					OverloadedCounter = this.healthValues[ResourceLoadState.Overloaded].GetValue(),
					CriticalCounter = this.healthValues[ResourceLoadState.Critical].GetValue(),
					UnknownCounter = this.healthValues[ResourceLoadState.Unknown].GetValue()
				};
			}

			// Token: 0x06001FC9 RID: 8137 RVA: 0x000439E3 File Offset: 0x00041BE3
			private static int GetPercentage(uint value, uint total)
			{
				if (total == 0U)
				{
					return 0;
				}
				return (int)(value * 100U / total);
			}

			// Token: 0x04000CD4 RID: 3284
			private Dictionary<ResourceLoadState, FixedTimeSum> healthValues;

			// Token: 0x04000CD5 RID: 3285
			private FixedTimeAverage averageCapacity;
		}
	}
}
