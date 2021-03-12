using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200001E RID: 30
	internal sealed class ForEachResultFeature : Feature
	{
		// Token: 0x060000DD RID: 221 RVA: 0x0000486D File Offset: 0x00002A6D
		public ForEachResultFeature(Func<AnalysisMember> forEachResult)
		{
			this.forEachResultFunc = forEachResult;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000487C File Offset: 0x00002A7C
		public Func<AnalysisMember> ForEachResultFunc
		{
			get
			{
				return this.forEachResultFunc;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004884 File Offset: 0x00002A84
		public AnalysisMember AnalysisMember
		{
			get
			{
				return this.forEachResultFunc();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004891 File Offset: 0x00002A91
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.AnalysisMember.Name);
		}

		// Token: 0x04000053 RID: 83
		private readonly Func<AnalysisMember> forEachResultFunc;
	}
}
