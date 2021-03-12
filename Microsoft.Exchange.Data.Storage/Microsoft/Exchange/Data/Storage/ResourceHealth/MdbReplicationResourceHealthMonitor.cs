using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B2B RID: 2859
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MdbReplicationResourceHealthMonitor : PingerDependentHealthMonitor
	{
		// Token: 0x06006786 RID: 26502 RVA: 0x001B57BF File Offset: 0x001B39BF
		internal MdbReplicationResourceHealthMonitor(MdbReplicationResourceHealthMonitorKey key) : base(key, key.DatabaseGuid)
		{
			this.dataProtectionHealthAverage = new FixedTimeAverage(5000, 6, Environment.TickCount);
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x001B57E4 File Offset: 0x001B39E4
		public override ResourceHealthMonitorWrapper CreateWrapper()
		{
			return new DatabaseReplicationProviderWrapper(this);
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x001B57EC File Offset: 0x001B39EC
		public void Update(uint dataProtectionHealth)
		{
			base.ReceivedUpdate();
			this.LastUpdateUtc = TimeProvider.UtcNow;
			this.dataProtectionHealthAverage.Add((dataProtectionHealth < 2147483647U) ? dataProtectionHealth : 2147483647U);
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06006789 RID: 26505 RVA: 0x001B581A File Offset: 0x001B3A1A
		protected override int InternalMetricValue
		{
			get
			{
				if (!this.dataProtectionHealthAverage.IsEmpty)
				{
					return (int)this.dataProtectionHealthAverage.GetValue();
				}
				return -1;
			}
		}

		// Token: 0x04003AA7 RID: 15015
		private const int BucketTimeInMsec = 5000;

		// Token: 0x04003AA8 RID: 15016
		private const int NumberOfBucketsForAveraging = 6;

		// Token: 0x04003AA9 RID: 15017
		private FixedTimeAverage dataProtectionHealthAverage;
	}
}
