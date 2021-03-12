using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000348 RID: 840
	internal class NotifyEndOfLogReply : NetworkChannelMessage
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x000A02D7 File Offset: 0x0009E4D7
		internal long EndOfLogGeneration
		{
			get
			{
				return this.m_endOfLogGeneration;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000A02DF File Offset: 0x0009E4DF
		internal DateTime EndOfLogUtc
		{
			get
			{
				return this.m_endOfLogUtc;
			}
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000A02E7 File Offset: 0x0009E4E7
		internal NotifyEndOfLogReply(NetworkChannel channel, NetworkChannelMessage.MessageType msgType, long endOfLogGen, DateTime utc) : base(channel, msgType)
		{
			this.m_endOfLogGeneration = endOfLogGen;
			this.m_endOfLogUtc = utc;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000A0300 File Offset: 0x0009E500
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_endOfLogGeneration);
			base.Packet.Append(this.m_endOfLogUtc);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000A032A File Offset: 0x0009E52A
		internal NotifyEndOfLogReply(NetworkChannel channel, NetworkChannelMessage.MessageType msgType, byte[] packetContent) : base(channel, msgType, packetContent)
		{
			this.m_endOfLogGeneration = base.Packet.ExtractInt64();
			this.m_endOfLogUtc = base.Packet.ExtractDateTime();
		}

		// Token: 0x04000E34 RID: 3636
		private long m_endOfLogGeneration;

		// Token: 0x04000E35 RID: 3637
		private DateTime m_endOfLogUtc;
	}
}
