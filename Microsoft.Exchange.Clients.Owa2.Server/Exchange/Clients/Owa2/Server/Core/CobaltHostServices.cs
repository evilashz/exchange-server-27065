using System;
using System.Threading;
using Cobalt;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200003D RID: 61
	public class CobaltHostServices : HostServices
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00005D8C File Offset: 0x00003F8C
		static CobaltHostServices()
		{
			HostServices.Singleton = new CobaltHostServices();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005D98 File Offset: 0x00003F98
		public static void Initialize()
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005D9A File Offset: 0x00003F9A
		public override bool IsLoggingLevelEnabled(Log.Level level)
		{
			return true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005DA0 File Offset: 0x00003FA0
		public override void WriteToLog(Log.Level level, string message)
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			int lid = 0;
			switch (level)
			{
			case 0:
			case 3:
			case 7:
				ExTraceGlobals.CobaltTracer.TraceWarning(lid, (long)managedThreadId, message);
				return;
			case 1:
			case 2:
				ExTraceGlobals.CobaltTracer.TraceError(lid, (long)managedThreadId, message);
				return;
			case 4:
			case 5:
			case 6:
				ExTraceGlobals.CobaltTracer.TraceInformation(lid, (long)managedThreadId, message);
				return;
			default:
				ExTraceGlobals.CobaltTracer.TraceInformation(lid, (long)managedThreadId, message);
				return;
			}
		}
	}
}
