using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PicwContactExtractorFactory : IPipelineComponentFactory
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00003B81 File Offset: 0x00001D81
		public IPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context)
		{
			return new PicwContactExtractor();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B88 File Offset: 0x00001D88
		public IStartStopPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context, IPipeline nestedPipeline)
		{
			throw new NotSupportedException("Doesn't support creating a component that uses nested pipeline");
		}
	}
}
