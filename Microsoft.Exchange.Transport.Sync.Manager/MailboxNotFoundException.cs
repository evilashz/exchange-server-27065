using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MailboxNotFoundException : CacheCorruptException
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002538 File Offset: 0x00000738
		public MailboxNotFoundException(Guid databaseGuid, Guid mailboxGuid, Exception innerException) : base(databaseGuid, mailboxGuid, Strings.MailboxNotFoundExceptionInfo(databaseGuid, mailboxGuid, innerException.Message), innerException)
		{
		}
	}
}
