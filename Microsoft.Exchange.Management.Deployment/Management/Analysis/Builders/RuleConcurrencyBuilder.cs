using System;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x02000050 RID: 80
	internal sealed class RuleConcurrencyBuilder<TParent>
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00007E3F File Offset: 0x0000603F
		public RuleConcurrencyBuilder(RuleBuildContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007E5C File Offset: 0x0000605C
		public RuleBuilder<TParent> AsSync()
		{
			this.context.RunAs = ConcurrencyType.Synchronous;
			return new RuleBuilder<TParent>(this.context);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007E75 File Offset: 0x00006075
		public RuleBuilder<TParent> AsAsync()
		{
			this.context.RunAs = ConcurrencyType.ASynchronous;
			return new RuleBuilder<TParent>(this.context);
		}

		// Token: 0x0400013C RID: 316
		private RuleBuildContext context;
	}
}
