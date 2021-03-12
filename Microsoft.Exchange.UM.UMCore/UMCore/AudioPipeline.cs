using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002B0 RID: 688
	internal class AudioPipeline : Pipeline
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x000598A1 File Offset: 0x00057AA1
		private AudioPipeline()
		{
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x000598A9 File Offset: 0x00057AA9
		internal static Pipeline Instance
		{
			get
			{
				return AudioPipeline.instance;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x000598B0 File Offset: 0x00057AB0
		protected override IPipelineStageFactory[] MyStages
		{
			get
			{
				return AudioPipeline.audioPipeline;
			}
		}

		// Token: 0x04000CB5 RID: 3253
		private static IPipelineStageFactory[] audioPipeline = new IPipelineStageFactory[]
		{
			new PipelineStageFactory<TranscriptionConfigurationStage>(),
			new PipelineStageFactory<ResolveCallerStage>(),
			new PipelineStageFactory<AudioCompressionStage>(),
			new PipelineStageFactory<TranscriptionStage>(),
			new PipelineStageFactory<SearchFolderVerificationStage>(),
			new PipelineStageFactory<CreateUnProtectedMessageStage>(),
			new PipelineStageFactory<SmtpSubmitStage>(),
			new PipelineStageFactory<LogPipelineStatisticsStage>(),
			new PipelineStageFactory<VoiceMessageCollectionStage>()
		};

		// Token: 0x04000CB6 RID: 3254
		private static Pipeline instance = new AudioPipeline();
	}
}
