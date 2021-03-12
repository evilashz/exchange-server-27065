using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002D7 RID: 727
	internal class PipelineStageFactory<T> : IPipelineStageFactory where T : PipelineStageBase, new()
	{
		// Token: 0x06001615 RID: 5653 RVA: 0x0005EA74 File Offset: 0x0005CC74
		public PipelineStageBase CreateStage(PipelineWorkItem workItem)
		{
			T t = Activator.CreateInstance<T>();
			t.Initialize(workItem);
			return t;
		}
	}
}
