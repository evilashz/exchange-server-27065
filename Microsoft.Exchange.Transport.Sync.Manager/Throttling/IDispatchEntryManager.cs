using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDispatchEntryManager
	{
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000387 RID: 903
		// (remove) Token: 0x06000388 RID: 904
		event EventHandler<DispatchEntry> EntryExpiredEvent;

		// Token: 0x06000389 RID: 905
		bool ContainsSubscription(Guid subscriptionGuid);

		// Token: 0x0600038A RID: 906
		int GetNumberOfEntriesForDatabase(Guid databaseGuid);

		// Token: 0x0600038B RID: 907
		bool HasBudget(WorkType workType);

		// Token: 0x0600038C RID: 908
		void Add(DispatchEntry dispatchEntry);

		// Token: 0x0600038D RID: 909
		DispatchEntry RemoveDispatchAttempt(Guid databaseGuid, Guid subscriptionGuid);

		// Token: 0x0600038E RID: 910
		void MarkDispatchSuccess(Guid subscriptionGuid);

		// Token: 0x0600038F RID: 911
		bool TryRemoveDispatchedEntry(Guid subscriptionGuid, out DispatchEntry dispatchEntry);

		// Token: 0x06000390 RID: 912
		void DisabledExpiration();

		// Token: 0x06000391 RID: 913
		XElement GetDiagnosticInfo(SyncDiagnosticMode mode);
	}
}
