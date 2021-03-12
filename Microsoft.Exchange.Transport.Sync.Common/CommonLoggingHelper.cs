using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000065 RID: 101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class CommonLoggingHelper
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00006C3E File Offset: 0x00004E3E
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00006C45 File Offset: 0x00004E45
		public static SyncLogSession SyncLogSession
		{
			get
			{
				return CommonLoggingHelper.syncLogSession;
			}
			set
			{
				CommonLoggingHelper.syncLogSession = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00006C4D File Offset: 0x00004E4D
		public static ExEventLog EventLogger
		{
			get
			{
				return CommonLoggingHelper.eventLogger;
			}
		}

		// Token: 0x04000112 RID: 274
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.EventLogTracer.Category, "MSExchangeTransportSyncCommon");

		// Token: 0x04000113 RID: 275
		private static SyncLogSession syncLogSession = SyncLogSession.InMemorySyncLogSession;
	}
}
