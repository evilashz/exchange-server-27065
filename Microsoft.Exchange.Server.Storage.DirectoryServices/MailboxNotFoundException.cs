using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x0200000C RID: 12
	public class MailboxNotFoundException : StoreException
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002318 File Offset: 0x00000518
		public MailboxNotFoundException(LID lid, Guid mailboxGuid) : base(lid, ErrorCodeValue.NotFound, string.Format("Mailbox not found: {0}", mailboxGuid))
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002336 File Offset: 0x00000536
		public MailboxNotFoundException(LID lid, string mailboxId) : base(lid, ErrorCodeValue.NotFound, string.Format("Mailbox not found: {0}", mailboxId))
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000234F File Offset: 0x0000054F
		public MailboxNotFoundException(LID lid, Guid mailboxGuid, Exception innerException) : base(lid, ErrorCodeValue.NotFound, string.Format("Mailbox not found: {0}", mailboxGuid), innerException)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000236E File Offset: 0x0000056E
		public MailboxNotFoundException(LID lid, string mailboxId, Exception innerException) : base(lid, ErrorCodeValue.NotFound, string.Format("Mailbox not found: {0}", mailboxId), innerException)
		{
		}

		// Token: 0x0400000D RID: 13
		private const string MailboxNotFoundTemplate = "Mailbox not found: {0}";
	}
}
