using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000E7 RID: 231
	internal struct ASTraceFilter : IDisposable
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0001A288 File Offset: 0x00018488
		public ASTraceFilter(string mailboxDN, string requestor)
		{
			this.traceEnabled = false;
			this.traceConfig = ASTraceConfiguration.Instance;
			if (!this.traceConfig.FilteredTracingEnabled)
			{
				return;
			}
			this.traceEnabled = (this.IsMailboxFiltered(mailboxDN) || this.IsRequesterFiltered(requestor));
			if (this.traceEnabled)
			{
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001A2E0 File Offset: 0x000184E0
		public void Dispose()
		{
			if (this.traceEnabled)
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001A310 File Offset: 0x00018510
		private bool IsRequesterFiltered(string requestor)
		{
			return !string.IsNullOrEmpty(requestor) && this.traceConfig.FilteredRequesters.Exists((string user) => StringComparer.OrdinalIgnoreCase.Equals(user, requestor));
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001A370 File Offset: 0x00018570
		private bool IsMailboxFiltered(string mailboxDN)
		{
			return mailboxDN != null && this.traceConfig.FilteredMailboxes.Exists((string legacyDN) => StringComparer.OrdinalIgnoreCase.Equals(mailboxDN, legacyDN));
		}

		// Token: 0x04000379 RID: 889
		private bool traceEnabled;

		// Token: 0x0400037A RID: 890
		private ASTraceConfiguration traceConfig;
	}
}
