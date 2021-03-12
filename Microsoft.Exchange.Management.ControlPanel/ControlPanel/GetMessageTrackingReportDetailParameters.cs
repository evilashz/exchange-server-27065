using System;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E1 RID: 737
	public class GetMessageTrackingReportDetailParameters : GetMessageTrackingReportParameters
	{
		// Token: 0x17001E10 RID: 7696
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x00089FEF File Offset: 0x000881EF
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x0008A001 File Offset: 0x00088201
		public string RecipientPathFilter
		{
			get
			{
				return (string)base["RecipientPathFilter"];
			}
			set
			{
				base["RecipientPathFilter"] = value;
			}
		}

		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x0008A00F File Offset: 0x0008820F
		// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x0008A021 File Offset: 0x00088221
		public ReportTemplate ReportTemplate
		{
			get
			{
				return (ReportTemplate)base["ReportTemplate"];
			}
			set
			{
				base["ReportTemplate"] = value;
			}
		}

		// Token: 0x0400222C RID: 8748
		public new const string RbacParameters = "?Identity&ReportTemplate&RecipientPathFilter";
	}
}
