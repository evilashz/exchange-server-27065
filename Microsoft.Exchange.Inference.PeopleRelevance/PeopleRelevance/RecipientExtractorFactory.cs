using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecipientExtractorFactory : IPipelineComponentFactory
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00006AD5 File Offset: 0x00004CD5
		public IPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context)
		{
			return new RecipientExtractor();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006ADC File Offset: 0x00004CDC
		public IStartStopPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context, IPipeline nestedPipeline)
		{
			throw new NotSupportedException("Doesn't support creating a component that uses nested pipeline");
		}
	}
}
