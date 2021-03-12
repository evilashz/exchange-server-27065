using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000016 RID: 22
	internal class DatabaseMonitor : DiskSpaceMonitor
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003250 File Offset: 0x00001450
		public DatabaseMonitor(DataSource dataSource, ResourceManagerConfiguration.ResourceMonitorConfiguration configuration) : base(Strings.DatabaseResource(dataSource.DatabasePath), dataSource.DatabasePath, configuration, (configuration.HighThreshold == 100) ? ByteQuantifiedSize.FromMB(500UL).ToBytes() : 0UL)
		{
			this.dataSource = dataSource;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000032A2 File Offset: 0x000014A2
		protected override ulong GetFreeBytesAvailableOffset()
		{
			return this.dataSource.GetAvailableFreeSpace();
		}

		// Token: 0x04000038 RID: 56
		private DataSource dataSource;
	}
}
