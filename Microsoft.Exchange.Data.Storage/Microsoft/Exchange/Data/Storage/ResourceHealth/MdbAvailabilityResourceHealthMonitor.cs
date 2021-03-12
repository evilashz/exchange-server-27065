using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B27 RID: 2855
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MdbAvailabilityResourceHealthMonitor : PingerDependentHealthMonitor
	{
		// Token: 0x0600677A RID: 26490 RVA: 0x001B5674 File Offset: 0x001B3874
		internal MdbAvailabilityResourceHealthMonitor(MdbAvailabilityResourceHealthMonitorKey key) : base(key, key.DatabaseGuid)
		{
			this.dataAvailabilityHealthAverage = new FixedTimeAverage(5000, 5, Environment.TickCount);
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x001B5699 File Offset: 0x001B3899
		public override ResourceHealthMonitorWrapper CreateWrapper()
		{
			return new DatabaseAvailabilityProviderWrapper(this);
		}

		// Token: 0x0600677C RID: 26492 RVA: 0x001B56A1 File Offset: 0x001B38A1
		public void Update(uint dataAvailabilityHealth)
		{
			base.ReceivedUpdate();
			this.LastUpdateUtc = TimeProvider.UtcNow;
			this.dataAvailabilityHealthAverage.Add((dataAvailabilityHealth < 2147483647U) ? dataAvailabilityHealth : 2147483647U);
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x001B56CF File Offset: 0x001B38CF
		protected override int InternalMetricValue
		{
			get
			{
				if (!this.dataAvailabilityHealthAverage.IsEmpty)
				{
					return (int)this.dataAvailabilityHealthAverage.GetValue();
				}
				return -1;
			}
		}

		// Token: 0x04003AA3 RID: 15011
		private const int BucketTimeInMsec = 5000;

		// Token: 0x04003AA4 RID: 15012
		private const int NumberOfBucketsForAveraging = 5;

		// Token: 0x04003AA5 RID: 15013
		private FixedTimeAverage dataAvailabilityHealthAverage;
	}
}
