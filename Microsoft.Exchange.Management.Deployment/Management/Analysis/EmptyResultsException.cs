using System;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x0200005A RID: 90
	internal class EmptyResultsException : AnalysisException
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00008224 File Offset: 0x00006424
		public EmptyResultsException()
		{
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000822C File Offset: 0x0000642C
		public EmptyResultsException(AnalysisMember source) : base(source, Strings.EmptyResults)
		{
		}
	}
}
