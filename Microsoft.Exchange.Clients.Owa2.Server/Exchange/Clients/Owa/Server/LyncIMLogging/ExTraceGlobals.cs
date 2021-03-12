using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Clients.Owa.Server.LyncIMLogging
{
	// Token: 0x0200012F RID: 303
	public class ExTraceGlobals
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x000232F4 File Offset: 0x000214F4
		private ExTraceGlobals()
		{
			SyncLogConfiguration syncLogConfiguration = LogConfiguration.CreateSyncLogConfiguration();
			SyncLog syncLog = new SyncLog(syncLogConfiguration);
			ExTraceGlobals.syncLogSession = syncLog.OpenGlobalSession();
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00023320 File Offset: 0x00021520
		public static ExTraceGlobals InstantMessagingTracer
		{
			get
			{
				if (ExTraceGlobals.instantMessagingTracer == null)
				{
					lock (ExTraceGlobals.instanceInitializationLock)
					{
						if (ExTraceGlobals.instantMessagingTracer == null)
						{
							ExTraceGlobals.instantMessagingTracer = new ExTraceGlobals();
						}
					}
				}
				return ExTraceGlobals.instantMessagingTracer;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00023378 File Offset: 0x00021578
		public void TraceDebug(long id, string message)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug(id, message);
			ExTraceGlobals.syncLogSession.LogDebugging((TSLID)0UL, "DEBUG:" + message, new object[0]);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000233A8 File Offset: 0x000215A8
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug(id, formatString, args);
			ExTraceGlobals.syncLogSession.LogDebugging((TSLID)0UL, "DEBUG:" + formatString, args);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000233D4 File Offset: 0x000215D4
		public void TraceError(long id, string message)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceError(id, message);
			ExTraceGlobals.syncLogSession.LogError((TSLID)0UL, "ERROR:" + message, new object[0]);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00023404 File Offset: 0x00021604
		public void TraceError(long id, string formatString, params object[] args)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceError(id, formatString, args);
			ExTraceGlobals.syncLogSession.LogError((TSLID)0UL, "ERROR:" + formatString, args);
		}

		// Token: 0x040006B8 RID: 1720
		private static object instanceInitializationLock = new object();

		// Token: 0x040006B9 RID: 1721
		private static ExTraceGlobals instantMessagingTracer = null;

		// Token: 0x040006BA RID: 1722
		private static GlobalSyncLogSession syncLogSession;
	}
}
