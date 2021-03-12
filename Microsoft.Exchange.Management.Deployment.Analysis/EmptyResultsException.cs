using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000014 RID: 20
	public class EmptyResultsException : AnalysisException
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000042C6 File Offset: 0x000024C6
		public EmptyResultsException()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000042CE File Offset: 0x000024CE
		public EmptyResultsException(AnalysisMember source) : base(source, Strings.EmptyResults)
		{
		}
	}
}
