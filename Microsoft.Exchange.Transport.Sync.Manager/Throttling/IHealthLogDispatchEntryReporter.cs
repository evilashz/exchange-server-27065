using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IHealthLogDispatchEntryReporter
	{
		// Token: 0x0600023E RID: 574
		void ReportDispatchAttempt(DispatchEntry dispatchEntry, DispatchTrigger dispatchTrigger, WorkType? workType, DispatchResult dispatchResult, ISubscriptionInformation subscriptionInformation, ExDateTime? lastDispatchTime);
	}
}
