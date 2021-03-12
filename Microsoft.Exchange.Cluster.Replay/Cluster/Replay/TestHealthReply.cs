using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200027B RID: 635
	internal class TestHealthReply : NetworkChannelMessage
	{
		// Token: 0x060018B1 RID: 6321 RVA: 0x000655F3 File Offset: 0x000637F3
		internal TestHealthReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.TestHealthReply)
		{
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00065601 File Offset: 0x00063801
		internal TestHealthReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.TestHealthReply, packetContent)
		{
		}
	}
}
