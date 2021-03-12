using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NestedSentItemsPipelineFeederFactory : IPipelineComponentFactory
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000035AF File Offset: 0x000017AF
		public IPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context)
		{
			throw new NotSupportedException("Doesn't support creating this component without the nested pipeline");
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000035BB File Offset: 0x000017BB
		public IStartStopPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context, IPipeline nestedPipeline)
		{
			return new NestedSentItemsPipelineFeeder(config, context, nestedPipeline);
		}
	}
}
