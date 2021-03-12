using System;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000059 RID: 89
	internal class AccessedFailedResultException : AnalysisException
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00008208 File Offset: 0x00006408
		public AccessedFailedResultException()
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008210 File Offset: 0x00006410
		public AccessedFailedResultException(AnalysisMember source, Exception inner) : base(source, Strings.AccessedFailedResult, inner)
		{
		}
	}
}
