using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C4 RID: 452
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchMailboxTaskCancelledException : SearchMailboxTaskException
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x00035394 File Offset: 0x00033594
		public SearchMailboxTaskCancelledException(MailboxInfo mailbox) : base(mailbox.IsPrimary ? Strings.SearchTaskCancelledPrimary(mailbox.DisplayName, mailbox.MailboxGuid.ToString()) : Strings.SearchTaskCancelledArchive(mailbox.DisplayName, mailbox.MailboxGuid.ToString()))
		{
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000353EF File Offset: 0x000335EF
		protected SearchMailboxTaskCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
