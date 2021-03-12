using System;
using System.Web.UI;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003B1 RID: 945
	public class AuditReportPage : BaseForm
	{
		// Token: 0x060031A0 RID: 12704 RVA: 0x000991D0 File Offset: 0x000973D0
		protected override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);
			if (!base.IsPostBack)
			{
				if (this.dcStartDate == null)
				{
					this.FindDateChooser(ref this.dcStartDate, "dcStartDate");
				}
				if (this.dcEndDate == null)
				{
					this.FindDateChooser(ref this.dcStartDate, "dcEndDate");
				}
				ExDateTime? exDateTime = new ExDateTime?(ExDateTime.UtcNow.ToUserExDateTime());
				if (exDateTime != null)
				{
					DateTime value = (DateTime)exDateTime.Value;
					this.dcStartDate.Value = value.AddDays(-15.0);
					this.dcEndDate.Value = value;
				}
			}
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x00099270 File Offset: 0x00097470
		private void FindDateChooser(ref DateChooser dateChooser, string id)
		{
			Control control = base.ContentPanel;
			control = control.FindControl("mainProperties");
			control = control.FindControl("mainSection");
			dateChooser = (DateChooser)control.FindControl(id);
		}

		// Token: 0x04002412 RID: 9234
		protected DateChooser dcStartDate;

		// Token: 0x04002413 RID: 9235
		protected DateChooser dcEndDate;
	}
}
