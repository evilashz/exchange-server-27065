using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000047 RID: 71
	internal enum SourceType
	{
		// Token: 0x04000171 RID: 369
		LegacyExchangeDN,
		// Token: 0x04000172 RID: 370
		PublicFolder,
		// Token: 0x04000173 RID: 371
		Recipient,
		// Token: 0x04000174 RID: 372
		MailboxGuid,
		// Token: 0x04000175 RID: 373
		AllPublicFolders,
		// Token: 0x04000176 RID: 374
		AllMailboxes,
		// Token: 0x04000177 RID: 375
		SavedSearchId,
		// Token: 0x04000178 RID: 376
		AutoDetect
	}
}
