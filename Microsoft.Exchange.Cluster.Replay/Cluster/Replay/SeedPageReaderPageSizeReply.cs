using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200013A RID: 314
	internal class SeedPageReaderPageSizeReply : NetworkChannelMessage
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00034CA4 File Offset: 0x00032EA4
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x00034CAC File Offset: 0x00032EAC
		public long PageSize
		{
			get
			{
				return this.m_pageSize;
			}
			set
			{
				this.m_pageSize = value;
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00034CB5 File Offset: 0x00032EB5
		internal SeedPageReaderPageSizeReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderPageSizeReply)
		{
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00034CC3 File Offset: 0x00032EC3
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_pageSize);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00034CDC File Offset: 0x00032EDC
		internal SeedPageReaderPageSizeReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderPageSizeReply, packetContent)
		{
			this.m_pageSize = base.Packet.ExtractInt64();
		}

		// Token: 0x04000522 RID: 1314
		private long m_pageSize;
	}
}
