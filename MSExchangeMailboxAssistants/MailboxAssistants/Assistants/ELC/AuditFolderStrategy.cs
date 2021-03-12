using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A4 RID: 164
	internal abstract class AuditFolderStrategy
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x00030511 File Offset: 0x0002E711
		public AuditFolderStrategy(MailboxDataForTags mailboxDataForTags, Trace tracer)
		{
			if (mailboxDataForTags == null)
			{
				throw new ArgumentNullException("mailboxDataForTags");
			}
			this.MailboxDataForTags = mailboxDataForTags;
			this.Tracer = tracer;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00030535 File Offset: 0x0002E735
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0003053D File Offset: 0x0002E73D
		public MailboxDataForTags MailboxDataForTags { get; private set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00030546 File Offset: 0x0002E746
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0003054E File Offset: 0x0002E74E
		public Trace Tracer { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600064D RID: 1613
		public abstract DefaultFolderType DefaultFolderType { get; }

		// Token: 0x0600064E RID: 1614
		public abstract StoreObjectId GetFolderId(MailboxSession session);

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600064F RID: 1615
		public abstract EnhancedTimeSpan AuditRecordAgeLimit { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00030557 File Offset: 0x0002E757
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0003055F File Offset: 0x0002E75F
		public int ItemExpired { get; set; }
	}
}
