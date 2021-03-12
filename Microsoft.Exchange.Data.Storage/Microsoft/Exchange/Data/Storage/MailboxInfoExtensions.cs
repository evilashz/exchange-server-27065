using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000276 RID: 630
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MailboxInfoExtensions
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x0007BC01 File Offset: 0x00079E01
		public static Guid GetDatabaseGuid(this IMailboxInfo mailboxInfo)
		{
			ArgumentValidator.ThrowIfNull("mailboxInfo", mailboxInfo);
			if (mailboxInfo.MailboxDatabase.IsNullOrEmpty())
			{
				return Guid.Empty;
			}
			return mailboxInfo.MailboxDatabase.ObjectGuid;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0007BC2C File Offset: 0x00079E2C
		public static string GetMailboxLegacyDn(this IMailboxInfo mailboxInfo, string userLegacyDn)
		{
			if (mailboxInfo.IsArchive || mailboxInfo.IsAggregated)
			{
				return userLegacyDn + "/guid=" + mailboxInfo.MailboxGuid;
			}
			return userLegacyDn;
		}
	}
}
