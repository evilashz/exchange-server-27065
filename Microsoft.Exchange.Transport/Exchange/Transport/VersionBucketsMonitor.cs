using System;
using System.Threading;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000076 RID: 118
	internal sealed class VersionBucketsMonitor : ResourceMonitor
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0000F84D File Offset: 0x0000DA4D
		public VersionBucketsMonitor(DataSource dataSource, ResourceManagerConfiguration.ResourceMonitorConfiguration configuration) : base(string.Empty, configuration)
		{
			this.dataSource = dataSource;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000F862 File Offset: 0x0000DA62
		public override void DoCleanup()
		{
			if (!this.dataSource.CleanupRequestInProgress && this.ResourceUses > ResourceUses.Normal)
			{
				this.dataSource.CleanupRequestInProgress = true;
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.dataSource.OnDataCleanup));
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000F89D File Offset: 0x0000DA9D
		public override string ToString(ResourceUses resourceUses, int currentPressure)
		{
			return Strings.VersionBucketUses(currentPressure, ResourceManager.MapToLocalizedString(resourceUses), base.LowPressureLimit, base.MediumPressureLimit, base.HighPressureLimit);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000F8C2 File Offset: 0x0000DAC2
		protected override bool GetCurrentReading(out int currentReading)
		{
			currentReading = (int)this.dataSource.GetCurrentVersionBuckets();
			return true;
		}

		// Token: 0x040001EB RID: 491
		private DataSource dataSource;
	}
}
