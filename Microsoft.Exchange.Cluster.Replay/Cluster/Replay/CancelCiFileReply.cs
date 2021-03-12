using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000298 RID: 664
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CancelCiFileReply : NetworkChannelMessage
	{
		// Token: 0x060019ED RID: 6637 RVA: 0x0006C6C6 File Offset: 0x0006A8C6
		internal CancelCiFileReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.CancelCiFileReply)
		{
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0006C6D4 File Offset: 0x0006A8D4
		internal CancelCiFileReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.CancelCiFileReply, packetContent)
		{
		}
	}
}
