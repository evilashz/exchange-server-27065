using System;
using System.IO;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x0200010B RID: 267
	internal sealed class DatabaseUsedSpaceMeter : UsedDiskSpaceResourceMeter
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x00028A1E File Offset: 0x00026C1E
		public DatabaseUsedSpaceMeter(IMeterableJetDataSource meterableDataSource, PressureTransitions pressureTransitions) : base("DatabaseUsedSpace", DatabaseUsedSpaceMeter.GetDatabaseDir(meterableDataSource), pressureTransitions)
		{
			this.meterableDataSource = meterableDataSource;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00028A3C File Offset: 0x00026C3C
		protected override long GetCurrentPressure()
		{
			ulong num;
			ulong num2;
			ulong num3;
			if (base.GetSpace(out num, out num2, out num3))
			{
				return (long)((num2 - num - (ulong)this.meterableDataSource.GetAvailableFreeSpace()) * 100UL / num2);
			}
			return 0L;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00028A6F File Offset: 0x00026C6F
		private static string GetDatabaseDir(IMeterableJetDataSource meterableDataSource)
		{
			ArgumentValidator.ThrowIfNull("meterableDataSource", meterableDataSource);
			return Path.GetDirectoryName(meterableDataSource.DatabasePath);
		}

		// Token: 0x040004EE RID: 1262
		private readonly IMeterableJetDataSource meterableDataSource;
	}
}
