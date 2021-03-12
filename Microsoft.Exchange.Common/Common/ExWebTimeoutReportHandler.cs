using System;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000053 RID: 83
	internal class ExWebTimeoutReportHandler
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000847F File Offset: 0x0000667F
		internal ExWebTimeoutReportHandler()
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000084A4 File Offset: 0x000066A4
		internal void Process(HttpContext context)
		{
			ExTraceGlobals.WebHealthTracer.TraceDebug((long)this.GetHashCode(), "ExWebTimeoutReportHandler::ProcessHealth()");
			try
			{
				if (ExMonitoringRequestTracker.Instance.IsKnownMonitoringRequest(context.Request))
				{
					context.Response.StatusCode = 200;
				}
				else
				{
					context.Response.StatusCode = 403;
				}
			}
			catch (Exception ex)
			{
				Exception ex2;
				Exception ex = ex2;
				ExTraceGlobals.WebHealthTracer.TraceError<Exception>((long)this.GetHashCode(), "ExWebTimeoutReportHandler::Process encountered error {0}", ex);
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					ExWatson.SendReport(ex, ReportOptions.DoNotCollectDumps | ReportOptions.DoNotFreezeThreads, string.Empty);
				});
			}
		}
	}
}
