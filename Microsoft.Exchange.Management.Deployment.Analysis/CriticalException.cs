using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000013 RID: 19
	public class CriticalException : AnalysisException
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004280 File Offset: 0x00002480
		public CriticalException() : base(null, Strings.CriticalMessage)
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004293 File Offset: 0x00002493
		public CriticalException(string message) : base(null, message)
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000429D File Offset: 0x0000249D
		public CriticalException(AnalysisMember source, string message) : base(source, message)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000042A7 File Offset: 0x000024A7
		public CriticalException(AnalysisMember source, Exception inner) : base(source, Strings.CriticalMessage, inner)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000042BB File Offset: 0x000024BB
		public CriticalException(AnalysisMember source, string message, Exception inner) : base(source, message, inner)
		{
		}
	}
}
