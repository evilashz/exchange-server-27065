using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000D4 RID: 212
	internal static class WatsonEventLog
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x000190C4 File Offset: 0x000172C4
		public static bool TryLogCrash(object[] parameters)
		{
			bool result;
			try
			{
				WatsonEventLog.eventLogger.LogEvent(CommonEventLogConstants.Tuple_ExchangeCrash, null, parameters);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00019100 File Offset: 0x00017300
		public static bool TryLogReportError(object[] parameters)
		{
			bool result;
			try
			{
				WatsonEventLog.eventLogger.LogEvent(CommonEventLogConstants.Tuple_WatsonReportError, null, parameters);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000443 RID: 1091
		private static ExEventLog eventLogger = new ExEventLog(new Guid("{173CD7C5-7EDB-4476-94D4-5D424A7D32B4}"), "MSExchange Common");
	}
}
