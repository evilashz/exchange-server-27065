using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000358 RID: 856
	internal class GranularTerminationMsg : NetworkChannelMessage
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x000A119D File Offset: 0x0009F39D
		public bool IsOverflow
		{
			get
			{
				return (this.FlagsUsed & GranularTerminationMsg.Flags.Overflow) != GranularTerminationMsg.Flags.None;
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000A11AF File Offset: 0x0009F3AF
		internal GranularTerminationMsg(NetworkChannel channel, bool isOverflow, string explanation, long nextGenToSend, long eolGen, DateTime eolUtc) : base(channel, NetworkChannelMessage.MessageType.GranularTermination)
		{
			if (isOverflow)
			{
				this.FlagsUsed = GranularTerminationMsg.Flags.Overflow;
			}
			this.NextGenerationServerWillSend = nextGenToSend;
			this.EndOfLogGeneration = eolGen;
			this.EndOfLogUtc = eolUtc;
			this.TerminationErrorString = explanation;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000A11E8 File Offset: 0x0009F3E8
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((ulong)this.FlagsUsed);
			base.Packet.Append(this.RequestAckCounter);
			base.Packet.Append(this.ReplyAckCounter);
			base.Packet.Append(this.NextGenerationServerWillSend);
			base.Packet.Append(this.EndOfLogGeneration);
			base.Packet.Append(this.EndOfLogUtc);
			base.Packet.Append(this.TerminationErrorString);
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000A1274 File Offset: 0x0009F474
		internal GranularTerminationMsg(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.GranularTermination, packetContent)
		{
			this.FlagsUsed = (GranularTerminationMsg.Flags)base.Packet.ExtractInt64();
			this.RequestAckCounter = base.Packet.ExtractInt64();
			this.ReplyAckCounter = base.Packet.ExtractInt64();
			this.NextGenerationServerWillSend = base.Packet.ExtractInt64();
			this.EndOfLogGeneration = base.Packet.ExtractInt64();
			this.EndOfLogUtc = base.Packet.ExtractDateTime();
			this.TerminationErrorString = base.Packet.ExtractString();
		}

		// Token: 0x04000E78 RID: 3704
		public GranularTerminationMsg.Flags FlagsUsed;

		// Token: 0x04000E79 RID: 3705
		public long RequestAckCounter;

		// Token: 0x04000E7A RID: 3706
		public long ReplyAckCounter;

		// Token: 0x04000E7B RID: 3707
		public long NextGenerationServerWillSend;

		// Token: 0x04000E7C RID: 3708
		public long EndOfLogGeneration;

		// Token: 0x04000E7D RID: 3709
		public DateTime EndOfLogUtc;

		// Token: 0x04000E7E RID: 3710
		public string TerminationErrorString;

		// Token: 0x02000359 RID: 857
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E80 RID: 3712
			None = 0UL,
			// Token: 0x04000E81 RID: 3713
			Overflow = 1UL
		}
	}
}
