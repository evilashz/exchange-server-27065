using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001F0 RID: 496
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct MessageReadState
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00020614 File Offset: 0x0001E814
		public byte[] MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002061C File Offset: 0x0001E81C
		public bool MarkAsRead
		{
			get
			{
				return this.markAsRead;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00020624 File Offset: 0x0001E824
		public MessageReadState(byte[] messageId, bool markAsRead)
		{
			this.messageId = messageId;
			this.markAsRead = markAsRead;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00020634 File Offset: 0x0001E834
		internal static MessageReadState Parse(Reader reader)
		{
			return new MessageReadState(reader.ReadSizeAndByteArray(), reader.ReadBool());
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00020647 File Offset: 0x0001E847
		internal void Serialize(Writer writer)
		{
			writer.WriteSizedBytes(this.MessageId);
			writer.WriteBool(this.MarkAsRead);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00020664 File Offset: 0x0001E864
		public override string ToString()
		{
			string arg = this.markAsRead ? "Read" : "Unread";
			if (this.messageId.Length != 22)
			{
				return string.Format("{0}: Foreign({1}b)", arg, this.messageId.Length);
			}
			return string.Format("{0}: {1}", arg, StoreLongTermId.Parse(this.messageId, false));
		}

		// Token: 0x04000492 RID: 1170
		internal static readonly uint MinimumSize = 3U;

		// Token: 0x04000493 RID: 1171
		private readonly byte[] messageId;

		// Token: 0x04000494 RID: 1172
		private readonly bool markAsRead;
	}
}
