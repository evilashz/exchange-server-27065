using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000CE RID: 206
	internal class WatsonEnvVarReportAction : WatsonReportAction
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x000178EC File Offset: 0x00015AEC
		public WatsonEnvVarReportAction(string varName) : base(varName, false)
		{
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000178F6 File Offset: 0x00015AF6
		public override string ActionName
		{
			get
			{
				return "Environment Variable";
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00017900 File Offset: 0x00015B00
		public override string Evaluate(WatsonReport watsonReport)
		{
			string text = base.Expression + "=" + Environment.GetEnvironmentVariable(base.Expression);
			watsonReport.LogExtraData(text);
			return text;
		}
	}
}
