using System;
using Microsoft.Exchange.Management.Deployment.Analysis;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000070 RID: 112
	internal abstract class PrereqConclusion : ConclusionImplementation<PrereqConclusion, PrereqSettingConclusion, PrereqRuleConclusion>
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x00026643 File Offset: 0x00024843
		public PrereqConclusion()
		{
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002664B File Offset: 0x0002484B
		public PrereqConclusion(Result result) : base(result)
		{
		}
	}
}
