using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200025B RID: 603
	internal class NetworkChannelCompressionRequest : NetworkChannelMessage, INetworkChannelRequest
	{
		// Token: 0x06001797 RID: 6039 RVA: 0x00061C22 File Offset: 0x0005FE22
		internal NetworkChannelCompressionRequest(NetworkChannel channel, NetworkChannel.DataEncodingScheme requestedEncoding) : base(channel, NetworkChannelMessage.MessageType.CompressionRequest)
		{
			this.m_requestedEncoding = requestedEncoding;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00061C37 File Offset: 0x0005FE37
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((uint)this.m_requestedEncoding);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00061C50 File Offset: 0x0005FE50
		protected internal NetworkChannelCompressionRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.CompressionRequest, packetContent)
		{
			this.m_requestedEncoding = (NetworkChannel.DataEncodingScheme)base.Packet.ExtractUInt32();
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00061C70 File Offset: 0x0005FE70
		public void Execute()
		{
			NetworkChannel.DataEncodingScheme dataEncodingScheme = NetworkChannel.VerifyDataEncoding(this.m_requestedEncoding);
			base.Channel.SetEncoding(dataEncodingScheme);
			NetworkChannelCompressionReply networkChannelCompressionReply = new NetworkChannelCompressionReply(base.Channel, dataEncodingScheme);
			networkChannelCompressionReply.Send();
			base.Channel.KeepAlive = true;
		}

		// Token: 0x04000942 RID: 2370
		private NetworkChannel.DataEncodingScheme m_requestedEncoding;
	}
}
