using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000021 RID: 33
	internal class PassiveStatusMsg : NetworkChannelMessage
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000048C4 File Offset: 0x00002AC4
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

		// Token: 0x060000DF RID: 223 RVA: 0x00004951 File Offset: 0x00002B51
		protected override void Serialize()
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004953 File Offset: 0x00002B53
		internal override void SendInternal()
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004958 File Offset: 0x00002B58
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

		// Token: 0x0400009C RID: 156
		public const int TotalMsgSize = 54;

		// Token: 0x0400009D RID: 157
		public PassiveStatusMsg.Flags FlagsUsed;

		// Token: 0x0400009E RID: 158
		public long AckCounter;

		// Token: 0x0400009F RID: 159
		public uint GenInNetworkBuffer;

		// Token: 0x040000A0 RID: 160
		public uint GenWrittenToInspector;

		// Token: 0x040000A1 RID: 161
		public uint LastGenInspected;

		// Token: 0x040000A2 RID: 162
		public uint LastGenReplayed;

		// Token: 0x040000A3 RID: 163
		public bool IsCrossSite;

		// Token: 0x02000022 RID: 34
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x040000A5 RID: 165
			None = 0UL,
			// Token: 0x040000A6 RID: 166
			PassiveIsRequestingAck = 1UL,
			// Token: 0x040000A7 RID: 167
			AckEndOfGeneration = 2UL
		}
	}
}
