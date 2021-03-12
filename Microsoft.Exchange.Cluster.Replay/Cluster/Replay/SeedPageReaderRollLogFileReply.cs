using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200013C RID: 316
	internal class SeedPageReaderRollLogFileReply : NetworkChannelMessage
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x00034E80 File Offset: 0x00033080
		internal SeedPageReaderRollLogFileReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileReply)
		{
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00034E8E File Offset: 0x0003308E
		protected override void Serialize()
		{
			base.Serialize();
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00034E96 File Offset: 0x00033096
		internal SeedPageReaderRollLogFileReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileReply, packetContent)
		{
		}
	}
}
