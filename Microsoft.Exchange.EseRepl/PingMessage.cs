using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000023 RID: 35
	internal class PingMessage : NetworkChannelMessage
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x000049E9 File Offset: 0x00002BE9
		internal PingMessage(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.Ping)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000049F7 File Offset: 0x00002BF7
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((ulong)this.FlagsUsed);
			base.Packet.Append(this.RequestAckCounter);
			base.Packet.Append(this.ReplyAckCounter);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004A34 File Offset: 0x00002C34
		internal PingMessage(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.Ping, packetContent)
		{
			this.FlagsUsed = (PingMessage.Flags)base.Packet.ExtractInt64();
			this.RequestAckCounter = base.Packet.ExtractInt64();
			this.ReplyAckCounter = base.Packet.ExtractInt64();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004A81 File Offset: 0x00002C81
		public PingMessage() : base(NetworkChannelMessage.MessageType.Ping)
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004A90 File Offset: 0x00002C90
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

		// Token: 0x040000A8 RID: 168
		public const int SizeRequired = 40;

		// Token: 0x040000A9 RID: 169
		public PingMessage.Flags FlagsUsed;

		// Token: 0x040000AA RID: 170
		public long RequestAckCounter;

		// Token: 0x040000AB RID: 171
		public long ReplyAckCounter;

		// Token: 0x02000024 RID: 36
		[Flags]
		public enum Flags : ulong
		{
			// Token: 0x040000AD RID: 173
			None = 0UL,
			// Token: 0x040000AE RID: 174
			Request = 1UL,
			// Token: 0x040000AF RID: 175
			Reply = 2UL
		}
	}
}
