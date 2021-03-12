using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA0 RID: 3232
	[Cmdlet("Get", "MailboxRestoreRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxRestoreRequestStatistics : GetRequestStatistics<MailboxRestoreRequestIdParameter, MailboxRestoreRequestStatistics>
	{
		// Token: 0x06007C16 RID: 31766 RVA: 0x001FC568 File Offset: 0x001FA768
		internal override void CheckIndexEntry(IRequestIndexEntry index)
		{
			base.CheckIndexEntry(index);
			base.CheckIndexEntryLocalUserNotNull(index);
		}
	}
}
