using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000020 RID: 32
	public static class LoggingUtilities
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00007FEE File Offset: 0x000061EE
		public static bool LogEvent(ExEventLog.EventTuple tuple, params object[] eventLogParams)
		{
			return LoggingUtilities.logger.LogEvent(tuple, string.Empty, eventLogParams);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008004 File Offset: 0x00006204
		public static void SendWatson(Exception exception)
		{
			bool flag = true;
			if (!bool.TryParse(ConfigurationManager.AppSettings["SendWatsonReport"], out flag))
			{
				flag = true;
			}
			if (flag)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "Sending Watson report");
				ExWatson.SendReport(exception, ReportOptions.None, null);
			}
		}

		// Token: 0x04000260 RID: 608
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.CoreTracer.Category, "MSExchange OWA");
	}
}
