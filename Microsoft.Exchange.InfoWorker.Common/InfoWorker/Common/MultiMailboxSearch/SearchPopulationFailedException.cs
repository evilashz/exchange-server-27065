using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C5 RID: 453
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchPopulationFailedException : SearchMailboxTaskException
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x000353FC File Offset: 0x000335FC
		public SearchPopulationFailedException(MailboxInfo mailbox) : base(mailbox.IsPrimary ? Strings.PrimarySearchPopulationFailed(mailbox.DisplayName, mailbox.MailboxGuid.ToString()) : Strings.ArchiveSearchPopulationFailed(mailbox.DisplayName, mailbox.MailboxGuid.ToString()))
		{
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00035457 File Offset: 0x00033657
		protected SearchPopulationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
