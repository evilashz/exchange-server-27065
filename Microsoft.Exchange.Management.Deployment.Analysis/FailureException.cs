using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000015 RID: 21
	public class FailureException : AnalysisException
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000042E1 File Offset: 0x000024E1
		public FailureException() : base(null, Strings.FailedResult)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000042F4 File Offset: 0x000024F4
		public FailureException(string message) : base(null, message)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000042FE File Offset: 0x000024FE
		public FailureException(AnalysisMember source, string message) : base(source, message)
		{
		}
	}
}
