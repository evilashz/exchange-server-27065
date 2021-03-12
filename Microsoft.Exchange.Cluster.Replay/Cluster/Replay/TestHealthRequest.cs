using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200027A RID: 634
	internal class TestHealthRequest : NetworkChannelMessage, INetworkChannelRequest
	{
		// Token: 0x060018AE RID: 6318 RVA: 0x000655B4 File Offset: 0x000637B4
		internal TestHealthRequest(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.TestHealthRequest)
		{
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000655C2 File Offset: 0x000637C2
		internal TestHealthRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.TestHealthRequest, packetContent)
		{
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000655D4 File Offset: 0x000637D4
		public void Execute()
		{
			TestHealthReply testHealthReply = new TestHealthReply(base.Channel);
			testHealthReply.Send();
		}
	}
}
