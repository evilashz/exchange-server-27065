using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000363 RID: 867
	internal class QueryLogRangeReply : NetworkChannelMessage
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x000A18A8 File Offset: 0x0009FAA8
		// (set) Token: 0x060022BA RID: 8890 RVA: 0x000A18B0 File Offset: 0x0009FAB0
		internal long FirstAvailableGeneration
		{
			get
			{
				return this.m_firstAvailableGeneration;
			}
			set
			{
				this.m_firstAvailableGeneration = value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x000A18B9 File Offset: 0x0009FAB9
		// (set) Token: 0x060022BC RID: 8892 RVA: 0x000A18C1 File Offset: 0x0009FAC1
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

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x000A18CA File Offset: 0x0009FACA
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x000A18D2 File Offset: 0x0009FAD2
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

		// Token: 0x060022BF RID: 8895 RVA: 0x000A18DB File Offset: 0x0009FADB
		internal QueryLogRangeReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.QueryLogRangeReply)
		{
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000A18E9 File Offset: 0x0009FAE9
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_firstAvailableGeneration);
			base.Packet.Append(this.m_endOfLogGeneration);
			base.Packet.Append(this.m_endOfLogUtc);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000A1924 File Offset: 0x0009FB24
		internal QueryLogRangeReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.QueryLogRangeReply, packetContent)
		{
			this.m_firstAvailableGeneration = base.Packet.ExtractInt64();
			this.m_endOfLogGeneration = base.Packet.ExtractInt64();
			this.m_endOfLogUtc = base.Packet.ExtractDateTime();
		}

		// Token: 0x04000EA2 RID: 3746
		private long m_firstAvailableGeneration;

		// Token: 0x04000EA3 RID: 3747
		private long m_endOfLogGeneration;

		// Token: 0x04000EA4 RID: 3748
		private DateTime m_endOfLogUtc;
	}
}
