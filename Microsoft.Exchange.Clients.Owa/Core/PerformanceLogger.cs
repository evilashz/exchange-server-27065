using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200020E RID: 526
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PerformanceLogger
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x0006B9A2 File Offset: 0x00069BA2
		internal PerformanceLogger(string iisCountLabel, string iisLatencyLabel, string breadcrumbsLabel)
		{
			this.iisCount = iisCountLabel;
			this.iisLatency = iisLatencyLabel;
			this.breadLabel = breadcrumbsLabel;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0006B9BF File Offset: 0x00069BBF
		internal void AppendIISLogsEntry(StringBuilder iis, uint count, long latencyInMilliseconds)
		{
			iis.Append(this.iisCount).Append(count);
			iis.Append(this.iisLatency).Append(latencyInMilliseconds);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0006B9E7 File Offset: 0x00069BE7
		internal void AppendBreadcrumbEntry(StringBuilder breadcrumb, uint count, long latencyInMilliseconds)
		{
			breadcrumb.Append(this.breadLabel).Append(count);
			breadcrumb.Append(" (").Append(latencyInMilliseconds).Append(" ms)");
		}

		// Token: 0x04000C08 RID: 3080
		internal const string LeftParenthesis = " (";

		// Token: 0x04000C09 RID: 3081
		internal const string UnitsAndRightParenthesis = " ms)";

		// Token: 0x04000C0A RID: 3082
		private readonly string iisCount;

		// Token: 0x04000C0B RID: 3083
		private readonly string iisLatency;

		// Token: 0x04000C0C RID: 3084
		private readonly string breadLabel;
	}
}
