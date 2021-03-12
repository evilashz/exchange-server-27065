using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200004C RID: 76
	internal class ExMonitoringRequestTracker
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007EF6 File Offset: 0x000060F6
		internal static ExMonitoringRequestTracker Instance
		{
			get
			{
				return ExMonitoringRequestTracker.instance;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007EFD File Offset: 0x000060FD
		internal string MonitoringInstanceId
		{
			get
			{
				return this.monitoringInstanceId;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007F08 File Offset: 0x00006108
		private ExMonitoringRequestTracker()
		{
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] array = new byte[16];
				rngcryptoServiceProvider.GetBytes(array);
				this.monitoringInstanceId = new Guid(array).ToString();
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007F74 File Offset: 0x00006174
		internal void ReportMonitoringRequest(HttpRequest request)
		{
			string text = request.Headers["X-MonitoringInstance"];
			if (!string.IsNullOrEmpty(text))
			{
				if (this.knownMonitoringIds.Count > 1000)
				{
					ExTraceGlobals.WebHealthTracer.TraceDebug<int>((long)this.GetHashCode(), "ExMonitoringRequestTracker::ReportMonitoringRequest() - Resetting list of known monitoring IDs since there were more than {0} elements", 1000);
					this.knownMonitoringIds.Clear();
				}
				this.knownMonitoringIds.GetOrAdd(text, true);
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007FE0 File Offset: 0x000061E0
		internal bool IsKnownMonitoringRequest(HttpRequest request)
		{
			string text = request.Headers["X-MonitoringInstance"];
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.WebHealthTracer.TraceDebug((long)this.GetHashCode(), "ExMonitoringRequestTracker::IsKnownMonitoringRequest() - No monitoring ID header present");
				return false;
			}
			if (this.knownMonitoringIds.ContainsKey(text))
			{
				ExTraceGlobals.WebHealthTracer.TraceDebug<string>((long)this.GetHashCode(), "ExMonitoringRequestTracker::IsKnownMonitoringRequest() - Request contained a valid ID: {0}", text);
				return true;
			}
			ExTraceGlobals.WebHealthTracer.TraceDebug<string>((long)this.GetHashCode(), "ExMonitoringRequestTracker::IsKnownMonitoringRequest() - Request contained a unknown ID: {0}", text);
			return false;
		}

		// Token: 0x04000173 RID: 371
		private const int MaxNumberOfKnownMonitoringClients = 1000;

		// Token: 0x04000174 RID: 372
		internal const string MonitoringInstanceIdHeaderName = "X-MonitoringInstance";

		// Token: 0x04000175 RID: 373
		private static ExMonitoringRequestTracker instance = new ExMonitoringRequestTracker();

		// Token: 0x04000176 RID: 374
		private readonly string monitoringInstanceId;

		// Token: 0x04000177 RID: 375
		private readonly ConcurrentDictionary<string, bool> knownMonitoringIds = new ConcurrentDictionary<string, bool>();
	}
}
