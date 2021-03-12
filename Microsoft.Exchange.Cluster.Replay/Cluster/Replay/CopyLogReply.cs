using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200034D RID: 845
	internal class CopyLogReply : NetworkChannelFileTransferReply
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x000A04EF File Offset: 0x0009E6EF
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x000A04F7 File Offset: 0x0009E6F7
		internal long ThisLogGeneration
		{
			get
			{
				return this.m_thisLogGeneration;
			}
			set
			{
				this.m_thisLogGeneration = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x000A0500 File Offset: 0x0009E700
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x000A0508 File Offset: 0x0009E708
		internal long EndOfLogGeneration
		{
			get
			{
				return this.m_endOfLogGeneration;
			}
			set
			{
				this.m_endOfLogGeneration = value;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000A0511 File Offset: 0x0009E711
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x000A0519 File Offset: 0x0009E719
		internal DateTime EndOfLogUtc
		{
			get
			{
				return this.m_endOfLogUtc;
			}
			set
			{
				this.m_endOfLogUtc = value;
			}
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000A0522 File Offset: 0x0009E722
		internal CopyLogReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.CopyLogReply)
		{
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000A0530 File Offset: 0x0009E730
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.ThisLogGeneration);
			base.Packet.Append(this.EndOfLogGeneration);
			base.Packet.Append(this.EndOfLogUtc);
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000A056C File Offset: 0x0009E76C
		internal CopyLogReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.CopyLogReply, packetContent)
		{
			this.m_thisLogGeneration = base.Packet.ExtractInt64();
			this.m_endOfLogGeneration = base.Packet.ExtractInt64();
			this.m_endOfLogUtc = base.Packet.ExtractDateTime();
		}

		// Token: 0x04000E3D RID: 3645
		private long m_thisLogGeneration;

		// Token: 0x04000E3E RID: 3646
		private long m_endOfLogGeneration;

		// Token: 0x04000E3F RID: 3647
		private DateTime m_endOfLogUtc;
	}
}
