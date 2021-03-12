using System;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.LogAnalyzer.Extensions.OABDownloadLog
{
	// Token: 0x02000002 RID: 2
	public abstract class OABDownloadLogAnalyzer : LogAnalyzer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected OABDownloadLogAnalyzer(IJob job) : base(job)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D9 File Offset: 0x000002D9
		public sealed override SessionLogAnalyzer CreateSessionLogAnalyzer()
		{
			return null;
		}
	}
}
