using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002DF RID: 735
	internal class ProtectedAudioPipeline : Pipeline
	{
		// Token: 0x06001666 RID: 5734 RVA: 0x0005F3F4 File Offset: 0x0005D5F4
		private ProtectedAudioPipeline()
		{
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0005F3FC File Offset: 0x0005D5FC
		internal static Pipeline Instance
		{
			get
			{
				return ProtectedAudioPipeline.instance;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0005F403 File Offset: 0x0005D603
		protected override IPipelineStageFactory[] MyStages
		{
			get
			{
				return ProtectedAudioPipeline.audioPipeline;
			}
		}

		// Token: 0x04000D53 RID: 3411
		private static IPipelineStageFactory[] audioPipeline = new IPipelineStageFactory[]
		{
			new PipelineStageFactory<TranscriptionConfigurationStage>(),
			new PipelineStageFactory<ResolveCallerStage>(),
			new PipelineStageFactory<AudioCompressionStage>(),
			new PipelineStageFactory<TranscriptionStage>(),
			new PipelineStageFactory<SearchFolderVerificationStage>(),
			new PipelineStageFactory<CreateProtectedMessageStage>(),
			new PipelineStageFactory<SmtpSubmitStage>(),
			new PipelineStageFactory<LogPipelineStatisticsStage>()
		};

		// Token: 0x04000D54 RID: 3412
		private static Pipeline instance = new ProtectedAudioPipeline();
	}
}
