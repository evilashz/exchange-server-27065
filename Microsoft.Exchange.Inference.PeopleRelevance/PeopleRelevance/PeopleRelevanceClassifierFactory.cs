using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleRelevanceClassifierFactory : IPipelineComponentFactory
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004431 File Offset: 0x00002631
		public IPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context)
		{
			return new PeopleRelevanceClassifier();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004438 File Offset: 0x00002638
		public IStartStopPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context, IPipeline nestedPipeline)
		{
			throw new NotSupportedException("Doesn't support creating a component that uses nested pipeline");
		}
	}
}
