using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200035A RID: 858
	internal class PingMessage : NetworkChannelMessage
	{
		// Token: 0x06002299 RID: 8857 RVA: 0x000A1305 File Offset: 0x0009F505
		internal PingMessage(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.Ping)
		{
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000A1313 File Offset: 0x0009F513
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((ulong)this.FlagsUsed);
			base.Packet.Append(this.RequestAckCounter);
			base.Packet.Append(this.ReplyAckCounter);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000A1350 File Offset: 0x0009F550
		internal PingMessage(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.Ping, packetContent)
		{
			this.FlagsUsed = (PingMessage.Flags)base.Packet.ExtractInt64();
			this.RequestAckCounter = base.Packet.ExtractInt64();
			this.ReplyAckCounter = base.Packet.ExtractInt64();
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000A139D File Offset: 0x0009F59D
		public PingMessage() : base(NetworkChannelMessage.MessageType.Ping)
		{
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000A13AC File Offset: 0x0009F5AC
		internal static PingMessage ReadFromNet(NetworkChannel ch, byte[] workingBuf, int startOffset)
		{
			int len = 24;
			ch.Read(workingBuf, startOffset, len);
			PingMessage pingMessage = new PingMessage();
			BufDeserializer bufDeserializer = new BufDeserializer(workingBuf, startOffset);
			pingMessage.FlagsUsed = (PingMessage.Flags)bufDeserializer.ExtractInt64();
			pingMessage.RequestAckCounter = bufDeserializer.ExtractInt64();
			pingMessage.ReplyAckCounter = bufDeserializer.ExtractInt64();
			return pingMessage;
		}

		// Token: 0x04000E82 RID: 3714
		public const int SizeRequired = 40;

		// Token: 0x04000E83 RID: 3715
		public PingMessage.Flags FlagsUsed;

		// Token: 0x04000E84 RID: 3716
		public long RequestAckCounter;

		// Token: 0x04000E85 RID: 3717
		public long ReplyAckCounter;

		// Token: 0x0200035B RID: 859
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x04000E87 RID: 3719
			None = 0UL,
			// Token: 0x04000E88 RID: 3720
			Request = 1UL,
			// Token: 0x04000E89 RID: 3721
			Reply = 2UL
		}
	}
}
