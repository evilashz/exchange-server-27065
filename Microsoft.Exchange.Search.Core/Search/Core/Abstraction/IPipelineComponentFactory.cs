using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000037 RID: 55
	internal interface IPipelineComponentFactory
	{
		// Token: 0x06000123 RID: 291
		IPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context);

		// Token: 0x06000124 RID: 292
		IStartStopPipelineComponent CreateInstance(IPipelineComponentConfig config, IPipelineContext context, IPipeline nestedPipeline);
	}
}
