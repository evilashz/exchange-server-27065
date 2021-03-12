using System;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x02000051 RID: 81
	internal sealed class RuleInBuilder<TParent>
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00007E8E File Offset: 0x0000608E
		public RuleInBuilder(RuleBuildContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007EAB File Offset: 0x000060AB
		public RuleConcurrencyBuilder<TParent> In(Analysis analysis)
		{
			if (analysis == null)
			{
				throw new ArgumentNullException("analysis");
			}
			this.context.Analysis = analysis;
			return new RuleConcurrencyBuilder<TParent>(this.context);
		}

		// Token: 0x0400013D RID: 317
		private RuleBuildContext context;
	}
}
