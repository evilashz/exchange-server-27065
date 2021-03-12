using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000012 RID: 18
	public class CanceledException : AnalysisException
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004263 File Offset: 0x00002463
		public CanceledException() : base(null, Strings.CanceledMessage)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004276 File Offset: 0x00002476
		public CanceledException(string message) : base(null, message)
		{
		}
	}
}
