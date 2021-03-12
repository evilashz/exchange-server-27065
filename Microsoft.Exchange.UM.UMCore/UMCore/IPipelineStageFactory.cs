using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002D6 RID: 726
	internal interface IPipelineStageFactory
	{
		// Token: 0x06001614 RID: 5652
		PipelineStageBase CreateStage(PipelineWorkItem workItem);
	}
}
