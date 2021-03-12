using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecipientCacheContactWriterFactory : IPipelineComponentFactory
	{
		// Token: 0x06000114 RID: 276 RVA: 0x000067E5 File Offset: 0x000049E5
		public IPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context)
		{
			return new RecipientCacheContactWriter();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000067EC File Offset: 0x000049EC
		public IStartStopPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context, IPipeline nestedPipeline)
		{
			throw new NotSupportedException("Doesn't support creating a component that uses nested pipeline");
		}
	}
}
