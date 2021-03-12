using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200007C RID: 124
	internal struct SenderGuidTraceFilter : IDisposable
	{
		// Token: 0x06000390 RID: 912 RVA: 0x0000FE3C File Offset: 0x0000E03C
		public SenderGuidTraceFilter(Guid mdbGuid, Guid mailboxGuid)
		{
			this.traceConfig = TraceConfigurationSingleton<MailboxGuidTraceConfiguration>.Instance;
			this.traceEnabled = false;
			if (!this.traceConfig.FilteredTracingEnabled)
			{
				return;
			}
			this.traceEnabled = (this.IsMDBFiltered(mdbGuid) || this.IsMailboxFiltered(mailboxGuid));
			if (this.traceEnabled)
			{
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000FE94 File Offset: 0x0000E094
		public void Dispose()
		{
			if (this.traceEnabled)
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		private bool IsMDBFiltered(Guid mdbGuid)
		{
			return this.traceConfig.FilteredMDBs.Contains(mdbGuid);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000FEBB File Offset: 0x0000E0BB
		private bool IsMailboxFiltered(Guid mailboxGuid)
		{
			return this.traceConfig.FilteredMailboxs.Contains(mailboxGuid);
		}

		// Token: 0x04000201 RID: 513
		private bool traceEnabled;

		// Token: 0x04000202 RID: 514
		private MailboxGuidTraceConfiguration traceConfig;
	}
}
