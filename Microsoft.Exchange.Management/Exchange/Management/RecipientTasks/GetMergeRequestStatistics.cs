using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C85 RID: 3205
	[Cmdlet("Get", "MergeRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMergeRequestStatistics : GetRequestStatistics<MergeRequestIdParameter, MergeRequestStatistics>
	{
		// Token: 0x06007B26 RID: 31526 RVA: 0x001F94B5 File Offset: 0x001F76B5
		internal override void CheckIndexEntry(IRequestIndexEntry index)
		{
			base.CheckIndexEntry(index);
			base.CheckIndexEntryLocalUserNotNull(index);
		}
	}
}
