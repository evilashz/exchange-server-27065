using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000354 RID: 852
	internal class PassiveStatusMsg : NetworkChannelMessage
	{
		// Token: 0x0600228E RID: 8846 RVA: 0x000A0EC8 File Offset: 0x0009F0C8
		public static byte[] SerializeToBytes(PassiveStatusMsg.Flags msgFlags, long ackCounter, uint genInNetworkBuffer, uint genWrittenToInspector, uint lastGenInspected, uint lastGenReplayed, bool isCrossSite)
		{
			NetworkChannelPacket networkChannelPacket = new NetworkChannelPacket(54);
			networkChannelPacket.GrowthDisabled = true;
			networkChannelPacket.Append(1);
			int val = 49;
			networkChannelPacket.Append(val);
			val = 1096045392;
			networkChannelPacket.Append(val);
			val = 49;
			networkChannelPacket.Append(val);
			DateTime utcNow = DateTime.UtcNow;
			networkChannelPacket.Append(utcNow);
			networkChannelPacket.Append((long)msgFlags);
			networkChannelPacket.Append(ackCounter);
			networkChannelPacket.Append(genInNetworkBuffer);
			networkChannelPacket.Append(genWrittenToInspector);
			networkChannelPacket.Append(lastGenInspected);
			networkChannelPacket.Append(lastGenReplayed);
			networkChannelPacket.Append(isCrossSite);
			return networkChannelPacket.Buffer;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000A0F55 File Offset: 0x0009F155
		protected override void Serialize()
		{
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000A0F57 File Offset: 0x0009F157
		internal override void SendInternal()
		{
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000A0F5C File Offset: 0x0009F15C
		internal PassiveStatusMsg(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.PassiveStatus, packetContent)
		{
			this.FlagsUsed = (PassiveStatusMsg.Flags)base.Packet.ExtractInt64();
			this.AckCounter = base.Packet.ExtractInt64();
			this.GenInNetworkBuffer = base.Packet.ExtractUInt32();
			this.GenWrittenToInspector = base.Packet.ExtractUInt32();
			this.LastGenInspected = base.Packet.ExtractUInt32();
			this.LastGenReplayed = base.Packet.ExtractUInt32();
			this.IsCrossSite = base.Packet.ExtractBool();
		}

		// Token: 0x04000E62 RID: 3682
		public const int TotalMsgSize = 54;

		// Token: 0x04000E63 RID: 3683
		public PassiveStatusMsg.Flags FlagsUsed;

		// Token: 0x04000E64 RID: 3684
		public long AckCounter;

		// Token: 0x04000E65 RID: 3685
		public uint GenInNetworkBuffer;

		// Token: 0x04000E66 RID: 3686
		public uint GenWrittenToInspector;

		// Token: 0x04000E67 RID: 3687
		public uint LastGenInspected;

		// Token: 0x04000E68 RID: 3688
		public uint LastGenReplayed;

		// Token: 0x04000E69 RID: 3689
		public bool IsCrossSite;

		// Token: 0x02000355 RID: 853
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E6B RID: 3691
			None = 0UL,
			// Token: 0x04000E6C RID: 3692
			PassiveIsRequestingAck = 1UL,
			// Token: 0x04000E6D RID: 3693
			AckEndOfGeneration = 2UL
		}
	}
}
