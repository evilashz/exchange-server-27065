using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C6 RID: 454
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchFolderQueryFailedException : SearchMailboxTaskException
	{
		// Token: 0x06000C38 RID: 3128 RVA: 0x00035464 File Offset: 0x00033664
		public SearchFolderQueryFailedException(MailboxInfo mailbox) : base(mailbox.IsPrimary ? Strings.PrimarySearchPopulationFailed(mailbox.DisplayName, mailbox.MailboxGuid.ToString()) : Strings.ArchiveSearchPopulationFailed(mailbox.DisplayName, mailbox.MailboxGuid.ToString()))
		{
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000354BF File Offset: 0x000336BF
		protected SearchFolderQueryFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
