using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AntispamUpdate
{
	// Token: 0x02000005 RID: 5
	internal static class UpdateSvcEventLogger
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002D74 File Offset: 0x00000F74
		internal static bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params string[] messageArgs)
		{
			return UpdateSvcEventLogger.eventLogger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x04000017 RID: 23
		private static ExEventLog eventLogger = new ExEventLog(AntispamUpdateSvc.ComponentGuid, "MSExchange Anti-spam Update");
	}
}
