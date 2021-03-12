using System;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015C RID: 348
	internal class NewMergeRequestCommand : NewMergeRequestCommandBase
	{
		// Token: 0x0600111D RID: 4381 RVA: 0x000480A3 File Offset: 0x000462A3
		internal NewMergeRequestCommand(ExchangeOutlookAnywhereEndpoint endpoint, ExchangeJobItemSubscriptionSettings subscriptionSettings, object targetMailboxId, string mergeRequestName, bool whatIf, bool useAdmin) : base("New-MergeRequest", endpoint, subscriptionSettings, whatIf, useAdmin)
		{
			MigrationUtil.ThrowOnNullArgument(targetMailboxId, "targetMailboxId");
			MigrationUtil.ThrowOnNullOrEmptyArgument(mergeRequestName, "mergeRequestName");
			base.RequestName = mergeRequestName;
			this.TargetMailboxId = targetMailboxId;
		}

		// Token: 0x1700051B RID: 1307
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x000480DC File Offset: 0x000462DC
		public object TargetMailboxId
		{
			set
			{
				base.AddParameter("TargetMailbox", value);
			}
		}

		// Token: 0x040005F9 RID: 1529
		public const string NewMergeRequestCommandName = "New-MergeRequest";

		// Token: 0x040005FA RID: 1530
		internal const string TargetMailboxParameter = "TargetMailbox";
	}
}
