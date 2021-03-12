using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000E7 RID: 231
	internal class MessageTypeEntry
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		public MessageType MessageType
		{
			[DebuggerStepThrough]
			get
			{
				return this.messageType;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000CD34 File Offset: 0x0000AF34
		public MessageFlags MessageFlags
		{
			[DebuggerStepThrough]
			get
			{
				return this.messageFlags;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public MessageSecurityType MessageSecurityType
		{
			[DebuggerStepThrough]
			get
			{
				return this.messageSecurityType;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000CD44 File Offset: 0x0000AF44
		internal MessageTypeEntry(MessageType type, MessageFlags flags)
		{
			this.messageType = type;
			this.messageFlags = flags;
			this.messageSecurityType = MessageSecurityType.None;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000CD61 File Offset: 0x0000AF61
		internal MessageTypeEntry(MessageType type, MessageFlags flags, MessageSecurityType security)
		{
			this.messageType = type;
			this.messageFlags = flags;
			this.messageSecurityType = security;
		}

		// Token: 0x04000391 RID: 913
		private MessageType messageType;

		// Token: 0x04000392 RID: 914
		private MessageFlags messageFlags;

		// Token: 0x04000393 RID: 915
		private MessageSecurityType messageSecurityType;
	}
}
