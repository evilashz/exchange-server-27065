using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000365 RID: 869
	internal class GetE00GenerationReply : NetworkChannelMessage
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x000A19A7 File Offset: 0x0009FBA7
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x000A19AF File Offset: 0x0009FBAF
		internal long LogGeneration
		{
			get
			{
				return this.m_logGeneration;
			}
			set
			{
				this.m_logGeneration = value;
			}
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000A19B8 File Offset: 0x0009FBB8
		internal GetE00GenerationReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.GetE00GenerationReply)
		{
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000A19C6 File Offset: 0x0009FBC6
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_logGeneration);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000A19DF File Offset: 0x0009FBDF
		internal GetE00GenerationReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.GetE00GenerationReply, packetContent)
		{
			this.m_logGeneration = base.Packet.ExtractInt64();
		}

		// Token: 0x04000EA5 RID: 3749
		private long m_logGeneration;
	}
}
