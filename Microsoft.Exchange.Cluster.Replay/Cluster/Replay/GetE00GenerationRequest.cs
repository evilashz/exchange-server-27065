using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000364 RID: 868
	internal class GetE00GenerationRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x060022C2 RID: 8898 RVA: 0x000A1971 File Offset: 0x0009FB71
		internal GetE00GenerationRequest(NetworkChannel channel, Guid dbGuid) : base(channel, NetworkChannelMessage.MessageType.GetE00GenerationRequest, dbGuid)
		{
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000A1980 File Offset: 0x0009FB80
		internal GetE00GenerationRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.GetE00GenerationRequest, packetContent)
		{
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000A198F File Offset: 0x0009FB8F
		public override void Execute()
		{
			base.Channel.MonitoredDatabase.SendE00Generation(base.Channel);
		}
	}
}
