using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000E4 RID: 228
	internal struct InfoFromUserMailboxSession
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x00040678 File Offset: 0x0003E878
		internal InfoFromUserMailboxSession(MailboxSession session)
		{
			this.UserLegacyDN = session.MailboxOwnerLegacyDN;
			this.DatabaseGuid = session.MailboxOwner.MailboxInfo.GetDatabaseGuid();
			this.MailboxGuid = session.MailboxOwner.MailboxInfo.MailboxGuid;
			this.ExternalDirectoryOrganizationId = UserSettings.GetExternalDirectoryOrganizationId(session);
			this.DefaultCalendarFolderId = session.GetDefaultFolderId(DefaultFolderType.Calendar);
			this.DefaultDeletedItemsFolderId = session.GetDefaultFolderId(DefaultFolderType.DeletedItems);
			this.DefaultJunkEmailFolderId = session.GetDefaultFolderId(DefaultFolderType.JunkEmail);
			this.DefaultOutboxFolderId = session.GetDefaultFolderId(DefaultFolderType.Outbox);
		}

		// Token: 0x04000665 RID: 1637
		internal string UserLegacyDN;

		// Token: 0x04000666 RID: 1638
		internal Guid DatabaseGuid;

		// Token: 0x04000667 RID: 1639
		internal Guid MailboxGuid;

		// Token: 0x04000668 RID: 1640
		internal Guid ExternalDirectoryOrganizationId;

		// Token: 0x04000669 RID: 1641
		internal StoreObjectId DefaultCalendarFolderId;

		// Token: 0x0400066A RID: 1642
		internal StoreObjectId DefaultDeletedItemsFolderId;

		// Token: 0x0400066B RID: 1643
		internal StoreObjectId DefaultJunkEmailFolderId;

		// Token: 0x0400066C RID: 1644
		internal StoreObjectId DefaultOutboxFolderId;
	}
}
