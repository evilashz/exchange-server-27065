using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.FilteredTracing
{
	// Token: 0x02000008 RID: 8
	internal struct GuidTraceFilter : IDisposable
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00003900 File Offset: 0x00001B00
		public GuidTraceFilter(Guid mdbGuid, Guid mailboxGuid)
		{
			this.traceConfig = IWTraceConfiguration.Instance;
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

		// Token: 0x0600004D RID: 77 RVA: 0x00003958 File Offset: 0x00001B58
		public void Dispose()
		{
			if (this.traceEnabled)
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000396C File Offset: 0x00001B6C
		private bool IsMDBFiltered(Guid mdbGuid)
		{
			return this.traceConfig.FilteredMDBs.Contains(mdbGuid);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000397F File Offset: 0x00001B7F
		private bool IsMailboxFiltered(Guid mailboxGuid)
		{
			return this.traceConfig.FilteredMailboxs.Contains(mailboxGuid);
		}

		// Token: 0x04000046 RID: 70
		private bool traceEnabled;

		// Token: 0x04000047 RID: 71
		private IWTraceConfiguration traceConfig;
	}
}
