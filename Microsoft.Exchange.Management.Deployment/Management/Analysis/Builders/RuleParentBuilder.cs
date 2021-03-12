using System;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x02000052 RID: 82
	internal sealed class RuleParentBuilder
	{
		// Token: 0x06000215 RID: 533 RVA: 0x00007ED2 File Offset: 0x000060D2
		public RuleParentBuilder(RuleBuildContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007EEF File Offset: 0x000060EF
		public RuleInBuilder<TParent> WithParent<TParent>(Func<AnalysisMember<TParent>> parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			this.context.Parent = parent;
			return new RuleInBuilder<TParent>(this.context);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007F16 File Offset: 0x00006116
		public RuleInBuilder<object> AsRootRule()
		{
			this.context.Parent = null;
			return new RuleInBuilder<object>(this.context);
		}

		// Token: 0x0400013E RID: 318
		private RuleBuildContext context;
	}
}
