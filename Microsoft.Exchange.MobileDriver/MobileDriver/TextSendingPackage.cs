using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200004B RID: 75
	internal class TextSendingPackage
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000A4A4 File Offset: 0x000086A4
		public TextSendingPackage(BookmarkRetriever bookmarkRetriever, ICollection<MobileRecipient> recipients)
		{
			this.BookmarkRetriever = bookmarkRetriever;
			this.Recipients = new List<MobileRecipient>(recipients).AsReadOnly();
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000A4C4 File Offset: 0x000086C4
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000A4CC File Offset: 0x000086CC
		public BookmarkRetriever BookmarkRetriever { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A4D5 File Offset: 0x000086D5
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000A4DD File Offset: 0x000086DD
		public IList<MobileRecipient> Recipients { get; private set; }
	}
}
