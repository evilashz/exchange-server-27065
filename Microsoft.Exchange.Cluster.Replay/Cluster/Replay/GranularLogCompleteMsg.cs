using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000356 RID: 854
	internal class GranularLogCompleteMsg : NetworkChannelMessage
	{
		// Token: 0x06002292 RID: 8850 RVA: 0x000A0FED File Offset: 0x0009F1ED
		internal GranularLogCompleteMsg(NetworkChannel channel, GranularLogCloseData closeData) : base(channel, NetworkChannelMessage.MessageType.GranularLogComplete)
		{
			this.ChecksumUsed = closeData.ChecksumUsed;
			this.Generation = closeData.Generation;
			this.LastWriteUtc = closeData.LastWriteUtc;
			this.ChecksumBytes = closeData.ChecksumBytes;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000A102C File Offset: 0x0009F22C
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((ulong)this.FlagsUsed);
			base.Packet.Append(this.RequestAckCounter);
			base.Packet.Append(this.ReplyAckCounter);
			base.Packet.Append((uint)this.ChecksumUsed);
			base.Packet.Append(this.Generation);
			base.Packet.Append(this.LastWriteUtc);
			this.m_checksumLengthInBytes = ((this.ChecksumBytes == null) ? 0 : this.ChecksumBytes.Length);
			base.Packet.Append(this.m_checksumLengthInBytes);
			if (this.m_checksumLengthInBytes > 0)
			{
				base.Packet.Append(this.ChecksumBytes);
			}
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000A10EC File Offset: 0x0009F2EC
		internal GranularLogCompleteMsg(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.GranularLogComplete, packetContent)
		{
			this.FlagsUsed = (GranularLogCompleteMsg.Flags)base.Packet.ExtractInt64();
			this.RequestAckCounter = base.Packet.ExtractInt64();
			this.ReplyAckCounter = base.Packet.ExtractInt64();
			this.ChecksumUsed = (GranularLogCloseData.ChecksumAlgorithm)base.Packet.ExtractInt32();
			this.Generation = base.Packet.ExtractInt64();
			this.LastWriteUtc = base.Packet.ExtractDateTime();
			this.m_checksumLengthInBytes = base.Packet.ExtractInt32();
			if (this.m_checksumLengthInBytes > 0)
			{
				this.ChecksumBytes = base.Packet.ExtractBytes(this.m_checksumLengthInBytes);
			}
		}

		// Token: 0x04000E6E RID: 3694
		public GranularLogCompleteMsg.Flags FlagsUsed;

		// Token: 0x04000E6F RID: 3695
		public long RequestAckCounter;

		// Token: 0x04000E70 RID: 3696
		public long ReplyAckCounter;

		// Token: 0x04000E71 RID: 3697
		public GranularLogCloseData.ChecksumAlgorithm ChecksumUsed;

		// Token: 0x04000E72 RID: 3698
		public long Generation;

		// Token: 0x04000E73 RID: 3699
		public DateTime LastWriteUtc;

		// Token: 0x04000E74 RID: 3700
		private int m_checksumLengthInBytes;

		// Token: 0x04000E75 RID: 3701
		public byte[] ChecksumBytes;

		// Token: 0x02000357 RID: 855
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E77 RID: 3703
			None = 0UL
		}
	}
}
