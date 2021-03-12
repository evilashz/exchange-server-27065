using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000010 RID: 16
	public class AnalysisException : Exception
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000041EE File Offset: 0x000023EE
		public AnalysisException()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000041F6 File Offset: 0x000023F6
		public AnalysisException(AnalysisMember source)
		{
			this.AnalysisMemberSource = source;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004205 File Offset: 0x00002405
		public AnalysisException(string message) : base(message)
		{
			this.AnalysisMemberSource = null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004215 File Offset: 0x00002415
		public AnalysisException(AnalysisMember source, string message) : base(message)
		{
			this.AnalysisMemberSource = source;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004225 File Offset: 0x00002425
		public AnalysisException(AnalysisMember source, string message, Exception inner) : base(message, inner)
		{
			this.AnalysisMemberSource = source;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004236 File Offset: 0x00002436
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000423E File Offset: 0x0000243E
		public AnalysisMember AnalysisMemberSource { get; set; }
	}
}
