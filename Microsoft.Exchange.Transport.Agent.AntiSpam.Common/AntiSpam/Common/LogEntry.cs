using System;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000002 RID: 2
	internal class LogEntry
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public LogEntry(string reason, string reasonData, string diagnostics)
		{
			this.reason = reason;
			this.reasonData = reasonData;
			this.diagnostics = diagnostics;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020ED File Offset: 0x000002ED
		public LogEntry(string reason, string reasonData) : this(reason, reasonData, null)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F8 File Offset: 0x000002F8
		public LogEntry(object reason, object reasonData) : this((reason != null) ? reason.ToString() : null, (reasonData != null) ? reasonData.ToString() : null)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002118 File Offset: 0x00000318
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		public string ReasonData
		{
			get
			{
				return this.reasonData;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002128 File Offset: 0x00000328
		public string Diagnostics
		{
			get
			{
				return this.diagnostics;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly string reason;

		// Token: 0x04000002 RID: 2
		private readonly string reasonData;

		// Token: 0x04000003 RID: 3
		private readonly string diagnostics;
	}
}
