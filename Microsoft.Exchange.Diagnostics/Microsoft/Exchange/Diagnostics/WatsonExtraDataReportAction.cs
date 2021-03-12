using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000D2 RID: 210
	internal class WatsonExtraDataReportAction : WatsonReportAction
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x00018F9C File Offset: 0x0001719C
		public WatsonExtraDataReportAction(string text) : base(text, true)
		{
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00018FA6 File Offset: 0x000171A6
		public override string ActionName
		{
			get
			{
				return "Extra Data";
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00018FAD File Offset: 0x000171AD
		public override string Evaluate(WatsonReport watsonReport)
		{
			watsonReport.LogExtraData(base.Expression);
			return base.Expression;
		}
	}
}
