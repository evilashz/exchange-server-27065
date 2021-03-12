using System;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000058 RID: 88
	internal class AnalysisException : Exception
	{
		// Token: 0x06000228 RID: 552 RVA: 0x000081BF File Offset: 0x000063BF
		public AnalysisException()
		{
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000081C7 File Offset: 0x000063C7
		public AnalysisException(AnalysisMember source)
		{
			this.AnalysisMemberSource = source;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000081D6 File Offset: 0x000063D6
		public AnalysisException(AnalysisMember source, string message) : base(message)
		{
			this.AnalysisMemberSource = source;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000081E6 File Offset: 0x000063E6
		public AnalysisException(AnalysisMember source, string message, Exception inner) : base(message, inner)
		{
			this.AnalysisMemberSource = source;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000081F7 File Offset: 0x000063F7
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000081FF File Offset: 0x000063FF
		public AnalysisMember AnalysisMemberSource { get; set; }
	}
}
