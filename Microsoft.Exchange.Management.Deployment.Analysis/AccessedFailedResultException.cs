using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000011 RID: 17
	public class AccessedFailedResultException : AnalysisException
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004247 File Offset: 0x00002447
		public AccessedFailedResultException()
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000424F File Offset: 0x0000244F
		public AccessedFailedResultException(AnalysisMember source, Exception inner) : base(source, Strings.AccessedFailedResult, inner)
		{
		}
	}
}
