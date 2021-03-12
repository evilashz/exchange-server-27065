using System;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200035C RID: 860
	internal class EnterBlockModeMsg : NetworkChannelMessage, INetworkChannelRequest
	{
		// Token: 0x0600229E RID: 8862 RVA: 0x000A13FC File Offset: 0x0009F5FC
		internal EnterBlockModeMsg(NetworkChannel channel, EnterBlockModeMsg.Flags flags, Guid dbGuid, long firstGenerationToExpect) : base(channel, NetworkChannelMessage.MessageType.EnterBlockMode)
		{
			this.FlagsUsed = flags;
			this.AckCounter = Win32StopWatch.GetSystemPerformanceCounter();
			this.FirstGenerationToExpect = firstGenerationToExpect;
			this.DatabaseGuid = dbGuid;
			this.ActiveNodeName = Environment.MachineName;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000A1438 File Offset: 0x0009F638
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((ulong)this.FlagsUsed);
			base.Packet.Append(this.AckCounter);
			base.Packet.Append(this.FirstGenerationToExpect);
			base.Packet.Append(this.DatabaseGuid);
			base.Packet.Append(this.ActiveNodeName);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000A14A0 File Offset: 0x0009F6A0
		internal EnterBlockModeMsg(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.EnterBlockMode, packetContent)
		{
			this.FlagsUsed = (EnterBlockModeMsg.Flags)base.Packet.ExtractInt64();
			this.AckCounter = base.Packet.ExtractInt64();
			this.FirstGenerationToExpect = base.Packet.ExtractInt64();
			this.DatabaseGuid = base.Packet.ExtractGuid();
			this.ActiveNodeName = base.Packet.ExtractString();
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000A150F File Offset: 0x0009F70F
		public void Execute()
		{
			LogCopier.EnterBlockMode(this, base.Channel);
		}

		// Token: 0x04000E8A RID: 3722
		public EnterBlockModeMsg.Flags FlagsUsed;

		// Token: 0x04000E8B RID: 3723
		public long AckCounter;

		// Token: 0x04000E8C RID: 3724
		public long FirstGenerationToExpect;

		// Token: 0x04000E8D RID: 3725
		public Guid DatabaseGuid;

		// Token: 0x04000E8E RID: 3726
		public string ActiveNodeName;

		// Token: 0x0200035D RID: 861
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E90 RID: 3728
			None = 0UL,
			// Token: 0x04000E91 RID: 3729
			PrepareToEnter = 1UL,
			// Token: 0x04000E92 RID: 3730
			PassiveIsReady = 2UL,
			// Token: 0x04000E93 RID: 3731
			PassiveReject = 4UL
		}
	}
}
