using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000017 RID: 23
	public class MultipleResultsException : AnalysisException
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000433D File Offset: 0x0000253D
		public MultipleResultsException()
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004345 File Offset: 0x00002545
		public MultipleResultsException(AnalysisMember source) : base(source, Strings.AccessedValueWhenMultipleResults)
		{
		}
	}
}
