using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200034B RID: 843
	internal class ContinuousLogCopyRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000A0400 File Offset: 0x0009E600
		public long FirstGeneration
		{
			get
			{
				return this.m_firstGeneration;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x000A0408 File Offset: 0x0009E608
		public long LastGeneration
		{
			get
			{
				return this.m_lastGeneration;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000A0410 File Offset: 0x0009E610
		// (set) Token: 0x06002260 RID: 8800 RVA: 0x000A0418 File Offset: 0x0009E618
		public ContinuousLogCopyRequest.Flags ReplicationFlags
		{
			get
			{
				return this.FlagsUsed;
			}
			set
			{
				this.FlagsUsed = value;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x000A0421 File Offset: 0x0009E621
		public bool ForAcll
		{
			get
			{
				return (this.FlagsUsed & ContinuousLogCopyRequest.Flags.ForAcll) != ContinuousLogCopyRequest.Flags.None;
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000A0433 File Offset: 0x0009E633
		internal ContinuousLogCopyRequest(NetworkChannel channel, Guid dbGuid, long firstLogNum, long lastLogNum, ContinuousLogCopyRequest.Flags flags) : base(channel, NetworkChannelMessage.MessageType.ContinuousLogCopyRequest, dbGuid)
		{
			this.m_firstGeneration = firstLogNum;
			this.m_lastGeneration = lastLogNum;
			this.FlagsUsed = flags;
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000A0459 File Offset: 0x0009E659
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_firstGeneration);
			base.Packet.Append(this.m_lastGeneration);
			base.Packet.Append((ulong)this.FlagsUsed);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000A0494 File Offset: 0x0009E694
		internal ContinuousLogCopyRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.ContinuousLogCopyRequest, packetContent)
		{
			this.m_firstGeneration = base.Packet.ExtractInt64();
			this.m_lastGeneration = base.Packet.ExtractInt64();
			this.FlagsUsed = (ContinuousLogCopyRequest.Flags)base.Packet.ExtractInt64();
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000A04E1 File Offset: 0x0009E6E1
		public override void Execute()
		{
			LogCopyServerContext.StartContinuousLogTransmission(base.Channel, this);
		}

		// Token: 0x04000E37 RID: 3639
		private long m_firstGeneration;

		// Token: 0x04000E38 RID: 3640
		private long m_lastGeneration;

		// Token: 0x04000E39 RID: 3641
		public ContinuousLogCopyRequest.Flags FlagsUsed;

		// Token: 0x0200034C RID: 844
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E3B RID: 3643
			None = 0UL,
			// Token: 0x04000E3C RID: 3644
			ForAcll = 2UL
		}
	}
}
