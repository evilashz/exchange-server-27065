using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002A5 RID: 677
	internal class SeedDatabaseFileReply : NetworkChannelFileTransferReply
	{
		// Token: 0x06001A61 RID: 6753 RVA: 0x0006F43C File Offset: 0x0006D63C
		internal SeedDatabaseFileReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.SeedDatabaseFileReply)
		{
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0006F44A File Offset: 0x0006D64A
		internal SeedDatabaseFileReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedDatabaseFileReply, packetContent)
		{
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0006F459 File Offset: 0x0006D659
		protected override void Serialize()
		{
			base.Serialize();
		}
	}
}
