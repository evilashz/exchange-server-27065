using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001CB RID: 459
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchTaskCancelled : MultiMailboxSearchException
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x0003556A File Offset: 0x0003376A
		public DiscoverySearchTaskCancelled(MailboxInfoList mailboxes, Guid databaseId) : base(Strings.SearchTaskCancelled(mailboxes.ToString(), databaseId.ToString()))
		{
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0003558A File Offset: 0x0003378A
		protected DiscoverySearchTaskCancelled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
