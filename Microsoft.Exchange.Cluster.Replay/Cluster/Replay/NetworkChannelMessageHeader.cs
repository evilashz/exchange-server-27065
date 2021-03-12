using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200025A RID: 602
	internal struct NetworkChannelMessageHeader
	{
		// Token: 0x0400093F RID: 2367
		public NetworkChannelMessage.MessageType MessageType;

		// Token: 0x04000940 RID: 2368
		public int MessageLength;

		// Token: 0x04000941 RID: 2369
		public DateTime MessageUtc;
	}
}
