using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C3 RID: 451
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchMailboxTaskTimeoutException : SearchMailboxTaskException
	{
		// Token: 0x06000C32 RID: 3122 RVA: 0x0003532C File Offset: 0x0003352C
		public SearchMailboxTaskTimeoutException(MailboxInfo mailbox) : base(mailbox.IsPrimary ? Strings.SearchTaskTimeoutPrimary(mailbox.DisplayName, mailbox.MailboxGuid.ToString()) : Strings.SearchTaskTimeoutArchive(mailbox.DisplayName, mailbox.MailboxGuid.ToString()))
		{
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00035387 File Offset: 0x00033587
		protected SearchMailboxTaskTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
