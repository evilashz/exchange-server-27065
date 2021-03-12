using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200000D RID: 13
	internal interface ICrashRepository
	{
		// Token: 0x06000048 RID: 72
		List<Guid> GetAllResourceIDs();

		// Token: 0x06000049 RID: 73
		bool GetQuarantineInfoContext(Guid resourceGuid, TimeSpan quarantineExpiryWindow, out QuarantineInfoContext quarantineInfoContext);

		// Token: 0x0600004A RID: 74
		bool GetResourceCrashInfoData(Guid resourceGuid, TimeSpan crashExpiryWindow, out Dictionary<long, ResourceEventCounterCrashInfo> resourceCrashData, out SortedSet<DateTime> allCrashTimes);

		// Token: 0x0600004B RID: 75
		void PersistCrashInfo(Guid resourceGuid, long eventCounter, ResourceEventCounterCrashInfo resourceEventCounterCrashInfo, int maxCrashEntries);

		// Token: 0x0600004C RID: 76
		bool PersistQuarantineInfo(Guid resourceGuid, QuarantineInfoContext quarantineInfoContext, bool overrideExisting = false);

		// Token: 0x0600004D RID: 77
		void PurgeResourceData(Guid resourceGuid);
	}
}
