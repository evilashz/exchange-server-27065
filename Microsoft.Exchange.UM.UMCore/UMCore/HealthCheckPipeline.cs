using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002BD RID: 701
	internal class HealthCheckPipeline : Pipeline
	{
		// Token: 0x06001540 RID: 5440 RVA: 0x0005B3A0 File Offset: 0x000595A0
		static HealthCheckPipeline()
		{
			HealthCheckPipeline.healthCheckPipeline = new IPipelineStageFactory[3];
			for (int i = 0; i < HealthCheckPipeline.healthCheckPipeline.Length; i++)
			{
				HealthCheckPipeline.healthCheckPipeline[i] = new PipelineStageFactory<HealthCheckStage>();
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0005B3E0 File Offset: 0x000595E0
		private HealthCheckPipeline()
		{
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0005B3E8 File Offset: 0x000595E8
		internal static Pipeline Instance
		{
			get
			{
				return HealthCheckPipeline.instance;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0005B3EF File Offset: 0x000595EF
		protected override IPipelineStageFactory[] MyStages
		{
			get
			{
				return HealthCheckPipeline.healthCheckPipeline;
			}
		}

		// Token: 0x04000CCE RID: 3278
		private static IPipelineStageFactory[] healthCheckPipeline;

		// Token: 0x04000CCF RID: 3279
		private static Pipeline instance = new HealthCheckPipeline();
	}
}
