using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000349 RID: 841
	internal class NotifyEndOfLogAsyncReply : NotifyEndOfLogReply
	{
		// Token: 0x06002257 RID: 8791 RVA: 0x000A0357 File Offset: 0x0009E557
		internal NotifyEndOfLogAsyncReply(NetworkChannel channel, long endOfLogGen, DateTime utc) : base(channel, NetworkChannelMessage.MessageType.NotifyEndOfLogAsyncReply, endOfLogGen, utc)
		{
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000A0367 File Offset: 0x0009E567
		internal NotifyEndOfLogAsyncReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.NotifyEndOfLogAsyncReply, packetContent)
		{
		}
	}
}
