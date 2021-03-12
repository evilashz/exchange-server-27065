using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A5 RID: 165
	internal class MailboxAuditFolderStrategy : AuditFolderStrategy
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x00030568 File Offset: 0x0002E768
		public MailboxAuditFolderStrategy(MailboxDataForTags mailboxDataForTags, Trace tracer) : base(mailboxDataForTags, tracer)
		{
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00030572 File Offset: 0x0002E772
		public override DefaultFolderType DefaultFolderType
		{
			get
			{
				return DefaultFolderType.Audits;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00030576 File Offset: 0x0002E776
		public override StoreObjectId GetFolderId(MailboxSession session)
		{
			return session.GetAuditsFolderId();
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0003057E File Offset: 0x0002E77E
		public override EnhancedTimeSpan AuditRecordAgeLimit
		{
			get
			{
				return base.MailboxDataForTags.ElcUserTagInformation.ADUser.MailboxAuditLogAgeLimit;
			}
		}
	}
}
