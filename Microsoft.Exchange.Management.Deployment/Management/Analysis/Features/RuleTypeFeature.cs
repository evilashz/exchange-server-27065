using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x02000064 RID: 100
	internal class RuleTypeFeature : Feature
	{
		// Token: 0x06000255 RID: 597 RVA: 0x000084A5 File Offset: 0x000066A5
		public RuleTypeFeature(RuleType ruleType) : base(false, false)
		{
			this.RuleType = ruleType;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000084B6 File Offset: 0x000066B6
		// (set) Token: 0x06000257 RID: 599 RVA: 0x000084BE File Offset: 0x000066BE
		public RuleType RuleType { get; private set; }

		// Token: 0x06000258 RID: 600 RVA: 0x000084C7 File Offset: 0x000066C7
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.RuleType.ToString());
		}
	}
}
