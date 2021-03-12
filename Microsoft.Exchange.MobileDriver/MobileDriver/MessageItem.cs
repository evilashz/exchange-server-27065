using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000036 RID: 54
	internal class MessageItem
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00006BFC File Offset: 0x00004DFC
		public MessageItem(Message message, MobileRecipient sender, ICollection<MobileRecipient> recipients, int maxSegmentsPerRecipient)
		{
			this.Message = message;
			this.Sender = sender;
			this.Recipients = recipients;
			this.MaxSegmentsPerRecipient = maxSegmentsPerRecipient;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006C21 File Offset: 0x00004E21
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00006C29 File Offset: 0x00004E29
		public Message Message { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00006C32 File Offset: 0x00004E32
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00006C3A File Offset: 0x00004E3A
		public MobileRecipient Sender { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006C43 File Offset: 0x00004E43
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00006C4B File Offset: 0x00004E4B
		public ICollection<MobileRecipient> Recipients { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006C54 File Offset: 0x00004E54
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00006C5C File Offset: 0x00004E5C
		public int MaxSegmentsPerRecipient { get; private set; }
	}
}
