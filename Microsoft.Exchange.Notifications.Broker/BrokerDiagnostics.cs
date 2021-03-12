using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000002 RID: 2
	internal static class BrokerDiagnostics
	{
		// Token: 0x06000001 RID: 1 RVA: 0x0000211E File Offset: 0x0000031E
		public static void SendWatsonReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate)
		{
			ExWatson.SendReportOnUnhandledException(methodDelegate, delegate(object exception)
			{
				bool flag = true;
				Exception ex = exception as Exception;
				if (ex != null)
				{
					ExTraceGlobals.ServiceTracer.TraceError<Exception>(0L, "Encountered unhandled exception: {0}", ex);
					flag = BrokerDiagnostics.ShouldSendReport(ex);
					if (flag)
					{
						ExWatson.SetWatsonReportAlreadySent(ex);
					}
				}
				ExTraceGlobals.ServiceTracer.TraceError<bool>(0L, "SendWatsonReportOnUnhandledException shouldSendReport: {0}", flag);
				return flag;
			});
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002143 File Offset: 0x00000343
		private static bool ShouldSendReport(Exception exception)
		{
			return !ExWatson.IsWatsonReportAlreadySent(exception) && !(exception is WebException);
		}
	}
}
