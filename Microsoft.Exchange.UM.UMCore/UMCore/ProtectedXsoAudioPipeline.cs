using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E0 RID: 736
	internal class ProtectedXsoAudioPipeline : Pipeline
	{
		// Token: 0x0600166A RID: 5738 RVA: 0x0005F470 File Offset: 0x0005D670
		private ProtectedXsoAudioPipeline()
		{
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0005F478 File Offset: 0x0005D678
		internal static Pipeline Instance
		{
			get
			{
				return ProtectedXsoAudioPipeline.instance;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0005F47F File Offset: 0x0005D67F
		protected override IPipelineStageFactory[] MyStages
		{
			get
			{
				return ProtectedXsoAudioPipeline.audioPipeline;
			}
		}

		// Token: 0x04000D55 RID: 3413
		private static IPipelineStageFactory[] audioPipeline = new IPipelineStageFactory[]
		{
			new PipelineStageFactory<TranscriptionConfigurationStage>(),
			new PipelineStageFactory<AudioCompressionStage>(),
			new PipelineStageFactory<TranscriptionStage>(),
			new PipelineStageFactory<CreateProtectedMessageStage>(),
			new PipelineStageFactory<XSOSubmitStage>(),
			new PipelineStageFactory<LogPipelineStatisticsStage>()
		};

		// Token: 0x04000D56 RID: 3414
		private static Pipeline instance = new ProtectedXsoAudioPipeline();
	}
}
