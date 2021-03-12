using System;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x0200005B RID: 91
	internal class FailureException : AnalysisException
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000823F File Offset: 0x0000643F
		public FailureException() : base(null, Strings.FailedResult)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008252 File Offset: 0x00006452
		public FailureException(string message) : base(null, message)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000825C File Offset: 0x0000645C
		public FailureException(AnalysisMember source, string message) : base(source, message)
		{
		}
	}
}
