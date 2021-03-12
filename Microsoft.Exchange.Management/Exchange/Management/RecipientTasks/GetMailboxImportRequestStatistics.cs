using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C93 RID: 3219
	[Cmdlet("Get", "MailboxImportRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxImportRequestStatistics : GetRequestStatistics<MailboxImportRequestIdParameter, MailboxImportRequestStatistics>
	{
		// Token: 0x06007BB3 RID: 31667 RVA: 0x001FAEFA File Offset: 0x001F90FA
		internal override void CheckIndexEntry(IRequestIndexEntry index)
		{
			base.CheckIndexEntry(index);
			base.CheckIndexEntryLocalUserNotNull(index);
		}
	}
}
