using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C8C RID: 3212
	[Cmdlet("Get", "MailboxExportRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxExportRequestStatistics : GetRequestStatistics<MailboxExportRequestIdParameter, MailboxExportRequestStatistics>
	{
		// Token: 0x06007B7D RID: 31613 RVA: 0x001FA692 File Offset: 0x001F8892
		internal override void CheckIndexEntry(IRequestIndexEntry index)
		{
			base.CheckIndexEntry(index);
			base.CheckIndexEntryLocalUserNotNull(index);
		}
	}
}
