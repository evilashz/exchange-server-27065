using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace System.Collections.Concurrent
{
	// Token: 0x02000480 RID: 1152
	[FriendAccessAllowed]
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E", LocalizationResources = "mscorlib")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x06003809 RID: 14345 RVA: 0x000D6186 File Offset: 0x000D4386
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000D618E File Offset: 0x000D438E
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000D61A3 File Offset: 0x000D43A3
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000D61B8 File Offset: 0x000D43B8
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000D61CD File Offset: 0x000D43CD
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000D61E1 File Offset: 0x000D43E1
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x04001859 RID: 6233
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x0400185A RID: 6234
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x0400185B RID: 6235
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x0400185C RID: 6236
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x0400185D RID: 6237
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x0400185E RID: 6238
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x0400185F RID: 6239
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
