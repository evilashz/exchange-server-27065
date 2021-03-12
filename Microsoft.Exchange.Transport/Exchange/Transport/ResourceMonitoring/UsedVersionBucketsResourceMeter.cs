using System;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x0200011E RID: 286
	internal sealed class UsedVersionBucketsResourceMeter : ResourceMeter
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x0002FA11 File Offset: 0x0002DC11
		public UsedVersionBucketsResourceMeter(IMeterableJetDataSource meterableDataSourcee, PressureTransitions pressureTransitions) : base("UsedVersionBuckets", UsedVersionBucketsResourceMeter.GetDatabasePath(meterableDataSourcee), pressureTransitions)
		{
			this.meterableDataSource = meterableDataSourcee;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002FA2C File Offset: 0x0002DC2C
		protected override long GetCurrentPressure()
		{
			return this.meterableDataSource.GetCurrentVersionBuckets();
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002FA39 File Offset: 0x0002DC39
		private static string GetDatabasePath(IMeterableJetDataSource meterableDataSource)
		{
			ArgumentValidator.ThrowIfNull("meterableDataSource", meterableDataSource);
			return meterableDataSource.DatabasePath;
		}

		// Token: 0x04000595 RID: 1429
		private readonly IMeterableJetDataSource meterableDataSource;
	}
}
