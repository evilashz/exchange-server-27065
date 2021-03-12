using System;
using Microsoft.Exchange.EseRepl;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000253 RID: 595
	internal class ExchangeNetworkPerfmonCounters
	{
		// Token: 0x06001744 RID: 5956 RVA: 0x0006015A File Offset: 0x0005E35A
		internal ExchangeNetworkPerfmonCounters(NetworkManagerPerfmonInstance instance)
		{
			this.m_instance = instance;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00060169 File Offset: 0x0005E369
		public void RecordLogCopyThruputReceived(long bytesCopied)
		{
			this.m_instance.LogCopyThruputReceived.IncrementBy(bytesCopied / 1024L);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00060184 File Offset: 0x0005E384
		public void RecordSeederThruputReceived(long bytesCopied)
		{
			this.m_instance.SeederThruputReceived.IncrementBy(bytesCopied / 1024L);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000601A0 File Offset: 0x0005E3A0
		public void RecordCompressedDataReceived(int compressedSize, int decompressedSize, NetworkPath.ConnectionPurpose connectionPurpose)
		{
			switch (connectionPurpose)
			{
			case NetworkPath.ConnectionPurpose.Seeding:
				this.m_instance.TotalCompressedSeedingBytesReceived.IncrementBy((long)compressedSize);
				this.m_instance.TotalSeedingBytesDecompressed.IncrementBy((long)decompressedSize);
				return;
			case NetworkPath.ConnectionPurpose.LogCopy:
				this.m_instance.TotalCompressedLogBytesReceived.IncrementBy((long)compressedSize);
				this.m_instance.TotalLogBytesDecompressed.IncrementBy((long)decompressedSize);
				return;
			default:
				return;
			}
		}

		// Token: 0x04000923 RID: 2339
		private NetworkManagerPerfmonInstance m_instance;
	}
}
