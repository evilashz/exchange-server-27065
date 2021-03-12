using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002B6 RID: 694
	internal class CDRPipeline : Pipeline
	{
		// Token: 0x0600150E RID: 5390 RVA: 0x0005A954 File Offset: 0x00058B54
		private CDRPipeline()
		{
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0005A95C File Offset: 0x00058B5C
		internal static Pipeline Instance
		{
			get
			{
				return CDRPipeline.instance;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0005A963 File Offset: 0x00058B63
		protected override IPipelineStageFactory[] MyStages
		{
			get
			{
				return CDRPipeline.cdrPipeline;
			}
		}

		// Token: 0x04000CC2 RID: 3266
		private static IPipelineStageFactory[] cdrPipeline = new IPipelineStageFactory[]
		{
			new PipelineStageFactory<CDRPipelineStage>()
		};

		// Token: 0x04000CC3 RID: 3267
		private static Pipeline instance = new CDRPipeline();
	}
}
