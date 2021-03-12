using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E8 RID: 744
	internal class TextPipeline : Pipeline
	{
		// Token: 0x06001695 RID: 5781 RVA: 0x0005FFFF File Offset: 0x0005E1FF
		private TextPipeline()
		{
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00060007 File Offset: 0x0005E207
		internal static Pipeline Instance
		{
			get
			{
				return TextPipeline.instance;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0006000E File Offset: 0x0005E20E
		protected override IPipelineStageFactory[] MyStages
		{
			get
			{
				return TextPipeline.textPipeline;
			}
		}

		// Token: 0x04000D65 RID: 3429
		private static IPipelineStageFactory[] textPipeline = new IPipelineStageFactory[]
		{
			new PipelineStageFactory<ResolveCallerStage>(),
			new PipelineStageFactory<CreateUnProtectedMessageStage>(),
			new PipelineStageFactory<SmtpSubmitStage>(),
			new PipelineStageFactory<LogPipelineStatisticsStage>()
		};

		// Token: 0x04000D66 RID: 3430
		private static Pipeline instance = new TextPipeline();
	}
}
