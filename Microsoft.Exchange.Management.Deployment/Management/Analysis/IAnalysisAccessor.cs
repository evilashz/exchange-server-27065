using System;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000038 RID: 56
	internal interface IAnalysisAccessor
	{
		// Token: 0x060000FE RID: 254
		void UpdateProgress(Rule completedRule);

		// Token: 0x060000FF RID: 255
		void CallOnAnalysisMemberStart(AnalysisMember member);

		// Token: 0x06000100 RID: 256
		void CallOnAnalysisMemberStop(AnalysisMember member);

		// Token: 0x06000101 RID: 257
		void CallOnAnalysisMemberEvaluate(AnalysisMember member, Result result);
	}
}
