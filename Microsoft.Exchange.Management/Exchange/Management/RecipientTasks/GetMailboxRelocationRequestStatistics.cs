using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C99 RID: 3225
	[Cmdlet("Get", "MailboxRelocationRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxRelocationRequestStatistics : GetRequestStatistics<MailboxRelocationRequestIdParameter, MailboxRelocationRequestStatistics>
	{
		// Token: 0x06007BE3 RID: 31715 RVA: 0x001FB780 File Offset: 0x001F9980
		internal override void CheckIndexEntry(IRequestIndexEntry index)
		{
			base.CheckIndexEntry(index);
			base.CheckIndexEntryLocalUserNotNull(index);
		}
	}
}
