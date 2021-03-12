using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200000C RID: 12
	internal sealed class MdbPeopleModelWriterFactory : IPipelineComponentFactory
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002E0A File Offset: 0x0000100A
		public IPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context)
		{
			return new MdbPeopleModelWriter(config, context);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E13 File Offset: 0x00001013
		public IStartStopPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context, IPipeline nestedPipeline)
		{
			throw new NotSupportedException("Doesn't support creating a component that uses nested pipeline");
		}
	}
}
