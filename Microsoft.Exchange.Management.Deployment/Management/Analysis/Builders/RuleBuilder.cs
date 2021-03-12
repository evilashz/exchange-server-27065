using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.Analysis.Features;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x0200004F RID: 79
	internal sealed class RuleBuilder<TParent> : IRuleFeatureBuilder, IFeatureBuilder
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00007D77 File Offset: 0x00005F77
		public RuleBuilder(RuleBuildContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public Rule Condition(Func<Result<TParent>, RuleResult> setFunction)
		{
			this.context.SetFunction = delegate(Result x)
			{
				IEnumerable<Result<bool>> result;
				try
				{
					result = new RuleResult[]
					{
						setFunction((Result<TParent>)x)
					};
				}
				catch (Exception exception)
				{
					result = new RuleResult[]
					{
						new RuleResult(exception)
					};
				}
				return result;
			};
			return (Rule)this.context.Construct();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007E31 File Offset: 0x00006031
		void IFeatureBuilder.AddFeature(Feature feature)
		{
			((IFeatureBuilder)this.context).AddFeature(feature);
		}

		// Token: 0x0400013B RID: 315
		private RuleBuildContext context;
	}
}
