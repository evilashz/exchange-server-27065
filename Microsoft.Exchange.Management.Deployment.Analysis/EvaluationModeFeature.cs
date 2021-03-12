using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200001A RID: 26
	internal sealed class EvaluationModeFeature : Feature
	{
		// Token: 0x060000CB RID: 203 RVA: 0x0000439E File Offset: 0x0000259E
		public EvaluationModeFeature(Evaluate evaluationMode)
		{
			this.evaluationMode = evaluationMode;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000043AD File Offset: 0x000025AD
		public Evaluate EvaluationMode
		{
			get
			{
				return this.evaluationMode;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000043B5 File Offset: 0x000025B5
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.evaluationMode);
		}

		// Token: 0x0400004C RID: 76
		private readonly Evaluate evaluationMode;
	}
}
