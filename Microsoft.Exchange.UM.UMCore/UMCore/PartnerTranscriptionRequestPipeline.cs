using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C5 RID: 709
	internal class PartnerTranscriptionRequestPipeline : Pipeline
	{
		// Token: 0x06001586 RID: 5510 RVA: 0x0005BDF8 File Offset: 0x00059FF8
		private PartnerTranscriptionRequestPipeline()
		{
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0005BE00 File Offset: 0x0005A000
		internal static Pipeline Instance
		{
			get
			{
				return PartnerTranscriptionRequestPipeline.instance;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0005BE07 File Offset: 0x0005A007
		protected override IPipelineStageFactory[] MyStages
		{
			get
			{
				return PartnerTranscriptionRequestPipeline.myStages;
			}
		}

		// Token: 0x04000CDD RID: 3293
		private static IPipelineStageFactory[] myStages = new IPipelineStageFactory[]
		{
			new PipelineStageFactory<AudioCompressionStage>(),
			new PipelineStageFactory<CreateUnProtectedMessageStage>(),
			new PipelineStageFactory<SmtpSubmitStage>(),
			new PipelineStageFactory<LogPipelineStatisticsStage>()
		};

		// Token: 0x04000CDE RID: 3294
		private static Pipeline instance = new PartnerTranscriptionRequestPipeline();
	}
}
