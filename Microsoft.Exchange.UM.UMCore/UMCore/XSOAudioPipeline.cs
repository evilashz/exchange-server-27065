using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002EF RID: 751
	internal class XSOAudioPipeline : Pipeline
	{
		// Token: 0x060016E4 RID: 5860 RVA: 0x000624A7 File Offset: 0x000606A7
		private XSOAudioPipeline()
		{
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x000624AF File Offset: 0x000606AF
		internal static Pipeline Instance
		{
			get
			{
				return XSOAudioPipeline.instance;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x000624B6 File Offset: 0x000606B6
		protected override IPipelineStageFactory[] MyStages
		{
			get
			{
				return XSOAudioPipeline.audioPipeline;
			}
		}

		// Token: 0x04000D81 RID: 3457
		private static IPipelineStageFactory[] audioPipeline = new IPipelineStageFactory[]
		{
			new PipelineStageFactory<TranscriptionConfigurationStage>(),
			new PipelineStageFactory<AudioCompressionStage>(),
			new PipelineStageFactory<TranscriptionStage>(),
			new PipelineStageFactory<CreateUnProtectedMessageStage>(),
			new PipelineStageFactory<XSOSubmitStage>(),
			new PipelineStageFactory<LogPipelineStatisticsStage>()
		};

		// Token: 0x04000D82 RID: 3458
		private static Pipeline instance = new XSOAudioPipeline();
	}
}
