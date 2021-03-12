using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000021 RID: 33
	public sealed class SeverityFeature : Feature
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000048C5 File Offset: 0x00002AC5
		public SeverityFeature(Severity severity)
		{
			this.severity = severity;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000048D4 File Offset: 0x00002AD4
		public Severity Severity
		{
			get
			{
				return this.severity;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000048DC File Offset: 0x00002ADC
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.Severity);
		}

		// Token: 0x04000059 RID: 89
		private readonly Severity severity;
	}
}
