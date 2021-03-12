using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200035E RID: 862
	internal class LogCopyServerStatusMsg : NetworkChannelMessage
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000A151D File Offset: 0x0009F71D
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x000A1525 File Offset: 0x0009F725
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

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x000A152E File Offset: 0x0009F72E
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x000A1536 File Offset: 0x0009F736
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

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x000A153F File Offset: 0x0009F73F
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x000A1547 File Offset: 0x0009F747
		internal int EndOfLogSector
		{
			get
			{
				return this.m_endOfLogSector;
			}
			set
			{
				this.m_endOfLogSector = value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000A1550 File Offset: 0x0009F750
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x000A1558 File Offset: 0x0009F758
		internal int EndOfLogByteOffset
		{
			get
			{
				return this.m_endOfLogByteOffset;
			}
			set
			{
				this.m_endOfLogByteOffset = value;
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000A1564 File Offset: 0x0009F764
		internal LogCopyServerStatusMsg(NetworkChannel channel, FullEndOfLog eol) : base(channel, NetworkChannelMessage.MessageType.LogCopyServerStatus)
		{
			this.EndOfLogGeneration = eol.Generation;
			this.EndOfLogUtc = eol.Utc;
			this.EndOfLogSector = eol.Sector;
			this.EndOfLogByteOffset = eol.ByteOffset;
			if (eol.PositionInE00)
			{
				this.FlagsUsed |= LogCopyServerStatusMsg.Flags.GranularStatus;
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000A15C4 File Offset: 0x0009F7C4
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((ulong)this.FlagsUsed);
			base.Packet.Append(this.RequestAckCounter);
			base.Packet.Append(this.ReplyAckCounter);
			base.Packet.Append(this.EndOfLogGeneration);
			base.Packet.Append(this.EndOfLogUtc);
			base.Packet.Append(this.EndOfLogSector);
			base.Packet.Append(this.EndOfLogByteOffset);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000A1650 File Offset: 0x0009F850
		internal LogCopyServerStatusMsg(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.LogCopyServerStatus, packetContent)
		{
			this.FlagsUsed = (LogCopyServerStatusMsg.Flags)base.Packet.ExtractInt64();
			this.RequestAckCounter = base.Packet.ExtractInt64();
			this.ReplyAckCounter = base.Packet.ExtractInt64();
			this.m_endOfLogGeneration = base.Packet.ExtractInt64();
			this.m_endOfLogUtc = base.Packet.ExtractDateTime();
			this.m_endOfLogSector = base.Packet.ExtractInt32();
			this.m_endOfLogByteOffset = base.Packet.ExtractInt32();
		}

		// Token: 0x04000E94 RID: 3732
		public LogCopyServerStatusMsg.Flags FlagsUsed;

		// Token: 0x04000E95 RID: 3733
		public long RequestAckCounter;

		// Token: 0x04000E96 RID: 3734
		public long ReplyAckCounter;

		// Token: 0x04000E97 RID: 3735
		private long m_endOfLogGeneration;

		// Token: 0x04000E98 RID: 3736
		private DateTime m_endOfLogUtc;

		// Token: 0x04000E99 RID: 3737
		private int m_endOfLogSector;

		// Token: 0x04000E9A RID: 3738
		private int m_endOfLogByteOffset;

		// Token: 0x0200035F RID: 863
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E9C RID: 3740
			None = 0UL,
			// Token: 0x04000E9D RID: 3741
			AckRequested = 1UL,
			// Token: 0x04000E9E RID: 3742
			GranularStatus = 2UL,
			// Token: 0x04000E9F RID: 3743
			GranularCompletionsDisabled = 8UL
		}
	}
}
