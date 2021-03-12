using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200034E RID: 846
	internal class ContinuousLogCopyRequest2 : NetworkChannelDatabaseRequest
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000A05B9 File Offset: 0x0009E7B9
		public bool UseGranular
		{
			get
			{
				return (this.FlagsUsed & ContinuousLogCopyRequest2.Flags.UseGranular) != ContinuousLogCopyRequest2.Flags.None;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000A05CB File Offset: 0x0009E7CB
		public bool ForAcll
		{
			get
			{
				return (this.FlagsUsed & ContinuousLogCopyRequest2.Flags.ForAcll) != ContinuousLogCopyRequest2.Flags.None;
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000A05DD File Offset: 0x0009E7DD
		internal ContinuousLogCopyRequest2(string clientNodeName, NetworkChannel channel, Guid dbGuid, long firstLogNum, ContinuousLogCopyRequest2.Flags flags) : base(channel, NetworkChannelMessage.MessageType.ContinuousLogCopyRequest2, dbGuid)
		{
			this.FlagsUsed = flags;
			this.FirstGeneration = firstLogNum;
			this.ClientNodeName = clientNodeName;
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000A0604 File Offset: 0x0009E804
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((ulong)this.FlagsUsed);
			base.Packet.Append(this.RequestAckCounter);
			base.Packet.Append(this.ReplyAckCounter);
			base.Packet.Append(this.FirstGeneration);
			base.Packet.Append(this.LastGeneration);
			base.Packet.Append(this.ClientNodeName);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000A0680 File Offset: 0x0009E880
		internal ContinuousLogCopyRequest2(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.ContinuousLogCopyRequest2, packetContent)
		{
			this.FlagsUsed = (ContinuousLogCopyRequest2.Flags)base.Packet.ExtractInt64();
			this.RequestAckCounter = base.Packet.ExtractInt64();
			this.ReplyAckCounter = base.Packet.ExtractInt64();
			this.FirstGeneration = base.Packet.ExtractInt64();
			this.LastGeneration = base.Packet.ExtractInt64();
			this.ClientNodeName = base.Packet.ExtractString();
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000A0700 File Offset: 0x0009E900
		public override void Execute()
		{
			LogCopyServerContext.StartContinuousLogTransmission(base.Channel, this);
		}

		// Token: 0x04000E40 RID: 3648
		public ContinuousLogCopyRequest2.Flags FlagsUsed;

		// Token: 0x04000E41 RID: 3649
		public long RequestAckCounter;

		// Token: 0x04000E42 RID: 3650
		public long ReplyAckCounter;

		// Token: 0x04000E43 RID: 3651
		public long FirstGeneration;

		// Token: 0x04000E44 RID: 3652
		public long LastGeneration;

		// Token: 0x04000E45 RID: 3653
		public string ClientNodeName;

		// Token: 0x0200034F RID: 847
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E47 RID: 3655
			None = 0UL,
			// Token: 0x04000E48 RID: 3656
			UseGranular = 1UL,
			// Token: 0x04000E49 RID: 3657
			ForAcll = 2UL,
			// Token: 0x04000E4A RID: 3658
			Pause = 4UL,
			// Token: 0x04000E4B RID: 3659
			Resume = 8UL,
			// Token: 0x04000E4C RID: 3660
			Stop = 16UL
		}
	}
}
