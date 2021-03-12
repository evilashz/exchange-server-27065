using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200025E RID: 606
	internal class NetworkChannelCompressionReply : NetworkChannelMessage
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00061DDF File Offset: 0x0005FFDF
		internal NetworkChannel.DataEncodingScheme AcceptedEncoding
		{
			get
			{
				return this.m_acceptedEncoding;
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00061DE7 File Offset: 0x0005FFE7
		internal NetworkChannelCompressionReply(NetworkChannel channel, NetworkChannel.DataEncodingScheme acceptedEncoding) : base(channel, NetworkChannelMessage.MessageType.CompressionReply)
		{
			this.m_acceptedEncoding = acceptedEncoding;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00061DFC File Offset: 0x0005FFFC
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((uint)this.m_acceptedEncoding);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00061E15 File Offset: 0x00060015
		internal NetworkChannelCompressionReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.CompressionReply, packetContent)
		{
			this.m_acceptedEncoding = (NetworkChannel.DataEncodingScheme)base.Packet.ExtractUInt32();
		}

		// Token: 0x04000948 RID: 2376
		private NetworkChannel.DataEncodingScheme m_acceptedEncoding;
	}
}
