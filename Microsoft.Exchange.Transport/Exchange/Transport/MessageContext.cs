using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200003A RID: 58
	internal class MessageContext : PoisonContext
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00006531 File Offset: 0x00004731
		public MessageContext(long messageId, string internetMessageId, MessageProcessingSource msgSource) : base(msgSource)
		{
			this.msgId = messageId;
			this.internetMessageId = internetMessageId;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006548 File Offset: 0x00004748
		internal long MessageId
		{
			get
			{
				return this.msgId;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006550 File Offset: 0x00004750
		internal string InternetMessageId
		{
			get
			{
				return this.internetMessageId;
			}
		}

		// Token: 0x040000AE RID: 174
		private readonly long msgId;

		// Token: 0x040000AF RID: 175
		private readonly string internetMessageId;
	}
}
