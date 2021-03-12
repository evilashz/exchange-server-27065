using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000015 RID: 21
	internal sealed class DatabaseLoggingFolderMonitor : DiskSpaceMonitor
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000031E8 File Offset: 0x000013E8
		public DatabaseLoggingFolderMonitor(DataSource dataSource, ResourceManagerConfiguration.ResourceMonitorConfiguration configuration) : base(Strings.DatabaseLoggingResource(dataSource.LogFilePath), dataSource.LogFilePath, configuration, (configuration.HighThreshold == 100) ? Math.Min(Components.TransportAppConfig.JetDatabase.CheckpointDepthMax.ToBytes() * 3UL, ByteQuantifiedSize.FromGB(5UL).ToBytes()) : 0UL)
		{
		}
	}
}
