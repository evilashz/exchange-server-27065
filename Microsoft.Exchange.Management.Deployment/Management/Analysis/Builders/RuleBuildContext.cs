using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x0200004E RID: 78
	internal sealed class RuleBuildContext : BuildContext<bool>
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00007D42 File Offset: 0x00005F42
		public RuleBuildContext(Func<RuleBuildContext, AnalysisMember<bool>> constructor)
		{
			this.constructor = constructor;
			this.SetFunction = null;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00007D58 File Offset: 0x00005F58
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00007D60 File Offset: 0x00005F60
		public Func<Result, IEnumerable<Result<bool>>> SetFunction { get; set; }

		// Token: 0x0600020C RID: 524 RVA: 0x00007D69 File Offset: 0x00005F69
		public override AnalysisMember<bool> Construct()
		{
			return this.constructor(this);
		}

		// Token: 0x04000139 RID: 313
		private Func<RuleBuildContext, AnalysisMember<bool>> constructor;
	}
}
