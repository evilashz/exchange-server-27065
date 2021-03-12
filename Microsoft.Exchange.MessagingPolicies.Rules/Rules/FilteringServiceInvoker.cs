using System;
using System.Collections.Generic;
using Microsoft.Filtering;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000015 RID: 21
	internal abstract class FilteringServiceInvoker
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000044E7 File Offset: 0x000026E7
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000044EF File Offset: 0x000026EF
		public string ErrorDescription { get; protected set; }

		// Token: 0x060000B4 RID: 180
		public abstract FilteringServiceInvoker.BeginScanResult BeginScan(FilteringServiceInvokerRequest filteringServiceInvokerRequest, ITracer tracer, Dictionary<string, string> classificationsToLookFor, FilteringServiceInvoker.ScanCompleteCallback scanCompleteCallback);

		// Token: 0x02000016 RID: 22
		public enum ScanResult
		{
			// Token: 0x040000CC RID: 204
			Success,
			// Token: 0x040000CD RID: 205
			Failure,
			// Token: 0x040000CE RID: 206
			CrashFailure,
			// Token: 0x040000CF RID: 207
			Timeout,
			// Token: 0x040000D0 RID: 208
			QueueTimeout
		}

		// Token: 0x02000017 RID: 23
		public enum BeginScanResult
		{
			// Token: 0x040000D2 RID: 210
			Queued,
			// Token: 0x040000D3 RID: 211
			Failure
		}

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x060000B7 RID: 183
		public delegate void ScanCompleteCallback(FilteringServiceInvoker.ScanResult scanResult, IEnumerable<DiscoveredDataClassification> classifications, FilteringResults filteringResults, Exception exception);
	}
}
