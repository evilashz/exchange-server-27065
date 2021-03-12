using System;
using Microsoft.Exchange.Management.Deployment.Analysis;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000076 RID: 118
	internal sealed class ModeFeature : Feature
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x000267C5 File Offset: 0x000249C5
		public ModeFeature(SetupMode modes)
		{
			this.mode = modes;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000267D4 File Offset: 0x000249D4
		public SetupMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000267DC File Offset: 0x000249DC
		public bool Contains(SetupMode mode)
		{
			return (this.mode & mode) > SetupMode.None;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000267E9 File Offset: 0x000249E9
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.mode.ToString());
		}

		// Token: 0x040005C6 RID: 1478
		private readonly SetupMode mode;
	}
}
