using System;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000026 RID: 38
	internal class EnterBlockModeMsg : NetworkChannelMessage, INetworkChannelRequest
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00004AE0 File Offset: 0x00002CE0
		internal EnterBlockModeMsg(NetworkChannel channel, EnterBlockModeMsg.Flags flags, Guid dbGuid, long firstGenerationToExpect) : base(channel, NetworkChannelMessage.MessageType.EnterBlockMode)
		{
			this.FlagsUsed = flags;
			this.AckCounter = Win32StopWatch.GetSystemPerformanceCounter();
			this.FirstGenerationToExpect = firstGenerationToExpect;
			this.DatabaseGuid = dbGuid;
			this.ActiveNodeName = Environment.MachineName;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004B1C File Offset: 0x00002D1C
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((ulong)this.FlagsUsed);
			base.Packet.Append(this.AckCounter);
			base.Packet.Append(this.FirstGenerationToExpect);
			base.Packet.Append(this.DatabaseGuid);
			base.Packet.Append(this.ActiveNodeName);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004B84 File Offset: 0x00002D84
		internal EnterBlockModeMsg(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.EnterBlockMode, packetContent)
		{
			this.FlagsUsed = (EnterBlockModeMsg.Flags)base.Packet.ExtractInt64();
			this.AckCounter = base.Packet.ExtractInt64();
			this.FirstGenerationToExpect = base.Packet.ExtractInt64();
			this.DatabaseGuid = base.Packet.ExtractGuid();
			this.ActiveNodeName = base.Packet.ExtractString();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004BF3 File Offset: 0x00002DF3
		public void Execute()
		{
		}

		// Token: 0x040000B0 RID: 176
		public EnterBlockModeMsg.Flags FlagsUsed;

		// Token: 0x040000B1 RID: 177
		public long AckCounter;

		// Token: 0x040000B2 RID: 178
		public long FirstGenerationToExpect;

		// Token: 0x040000B3 RID: 179
		public Guid DatabaseGuid;

		// Token: 0x040000B4 RID: 180
		public string ActiveNodeName;

		// Token: 0x02000027 RID: 39
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x040000B6 RID: 182
			None = 0UL,
			// Token: 0x040000B7 RID: 183
			PrepareToEnter = 1UL,
			// Token: 0x040000B8 RID: 184
			PassiveIsReady = 2UL,
			// Token: 0x040000B9 RID: 185
			PassiveReject = 4UL
		}
	}
}
