using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x02000118 RID: 280
	internal class MeterableJetDataSourceFactory
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0002E269 File Offset: 0x0002C469
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0002E270 File Offset: 0x0002C470
		internal static Func<DataSource, IMeterableJetDataSource> CreateMeterableJetDataSourceFunc
		{
			get
			{
				return MeterableJetDataSourceFactory.createMeterableDataSourceFunc;
			}
			set
			{
				MeterableJetDataSourceFactory.createMeterableDataSourceFunc = value;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002E278 File Offset: 0x0002C478
		internal static IMeterableJetDataSource CreateMeterableDataSource(DataSource dataSource)
		{
			return MeterableJetDataSourceFactory.CreateMeterableJetDataSourceFunc(dataSource);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002E285 File Offset: 0x0002C485
		private static IMeterableJetDataSource CreateRealMeterableJetDataSource(DataSource dataSource)
		{
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			return new MeterableJetDataSource(dataSource);
		}

		// Token: 0x04000562 RID: 1378
		private static Func<DataSource, IMeterableJetDataSource> createMeterableDataSourceFunc = new Func<DataSource, IMeterableJetDataSource>(MeterableJetDataSourceFactory.CreateRealMeterableJetDataSource);
	}
}
