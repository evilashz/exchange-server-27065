using System;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x0200005C RID: 92
	internal class MultipleResultsException : AnalysisException
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00008266 File Offset: 0x00006466
		public MultipleResultsException()
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000826E File Offset: 0x0000646E
		public MultipleResultsException(AnalysisMember source) : base(source, Strings.AccessedValueWhenMultipleResults)
		{
		}
	}
}
