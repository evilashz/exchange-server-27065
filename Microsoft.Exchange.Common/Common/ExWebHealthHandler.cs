using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200004D RID: 77
	internal class ExWebHealthHandler
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00008069 File Offset: 0x00006269
		internal ExWebHealthHandler(string appName)
		{
			this.appName = appName;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00008078 File Offset: 0x00006278
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00008080 File Offset: 0x00006280
		internal Func<ExWebHealthHandler.CustomHealthCheckResult> CustomHealthCallback { get; set; }

		// Token: 0x06000194 RID: 404 RVA: 0x000080A4 File Offset: 0x000062A4
		internal void ProcessHealth(IExWebHealthResponseWrapper response)
		{
			ExTraceGlobals.WebHealthTracer.TraceDebug((long)this.GetHashCode(), "ExWebHealthHandler::ProcessHealth()");
			string value = "Passed";
			response.AddHeader("X-MSExchApplicationHealthHandler", this.appName);
			Func<ExWebHealthHandler.CustomHealthCheckResult> customHealthCallback = this.CustomHealthCallback;
			if (customHealthCallback != null)
			{
				ExTraceGlobals.WebHealthTracer.TraceDebug((long)this.GetHashCode(), "ExWebHealthHandler::ProcessHealth calling custom health check callback ");
				try
				{
					ExWebHealthHandler.CustomHealthCheckResult customHealthCheckResult = customHealthCallback();
					ExTraceGlobals.WebHealthTracer.TraceDebug<ExWebHealthHandler.CustomHealthCheckResult>((long)this.GetHashCode(), "ExWebHealthHandler::ProcessHealth Custom health check status = {0}", customHealthCheckResult);
					if (customHealthCheckResult == ExWebHealthHandler.CustomHealthCheckResult.Healthy)
					{
						value = "Passed";
					}
					else if (customHealthCheckResult == ExWebHealthHandler.CustomHealthCheckResult.NotHealthy)
					{
						value = "Failed";
					}
					else
					{
						value = "NonDeterministic";
					}
				}
				catch (Exception ex)
				{
					Exception ex2;
					Exception ex = ex2;
					ExTraceGlobals.WebHealthTracer.TraceError<Exception>((long)this.GetHashCode(), "ExWebHealthHandler::ProcessHealth Callback encountered error {0}", ex);
					if (ex is OutOfMemoryException)
					{
						value = "Failed";
					}
					else
					{
						value = "NonDeterministic";
						if (this.ShouldSubmitWatson(ex))
						{
							ThreadPool.QueueUserWorkItem(delegate(object o)
							{
								ExWatson.SendReport(ex, ReportOptions.DoNotCollectDumps, string.Empty);
							});
						}
					}
				}
			}
			response.AddHeader("X-MSExchApplicationHealthHandlerStatus", value);
			response.StatusCode = 200;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000081E0 File Offset: 0x000063E0
		protected virtual bool ShouldSubmitWatson(Exception ex)
		{
			return true;
		}

		// Token: 0x04000178 RID: 376
		private readonly string appName;

		// Token: 0x0200004E RID: 78
		internal enum CustomHealthCheckResult
		{
			// Token: 0x0400017B RID: 379
			Healthy,
			// Token: 0x0400017C RID: 380
			NotHealthy,
			// Token: 0x0400017D RID: 381
			HealthCannotBeDetermined
		}

		// Token: 0x0200004F RID: 79
		internal static class Headers
		{
			// Token: 0x0400017E RID: 382
			public const string HealthHandler = "X-MSExchApplicationHealthHandler";

			// Token: 0x0400017F RID: 383
			public const string HealthHandlerStatus = "X-MSExchApplicationHealthHandlerStatus";

			// Token: 0x04000180 RID: 384
			public const string HealthHandlerStatusFailureValue = "Failed";

			// Token: 0x04000181 RID: 385
			public const string HealthHandlerStatusSuccessValue = "Passed";

			// Token: 0x04000182 RID: 386
			public const string HealthHandlerStatusCannotBeDetermined = "NonDeterministic";
		}
	}
}
