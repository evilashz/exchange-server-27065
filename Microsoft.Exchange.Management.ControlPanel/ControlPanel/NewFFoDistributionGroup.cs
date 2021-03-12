using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000528 RID: 1320
	public class NewFFoDistributionGroup : BaseForm
	{
		// Token: 0x06003EE7 RID: 16103 RVA: 0x000BD610 File Offset: 0x000BB810
		protected override void OnLoad(EventArgs e)
		{
			TextBox textBox = (TextBox)this.wizard1.Sections["generalInfoSection"].FindControl("groupType");
			if (!string.IsNullOrEmpty(base.Request.QueryString["GroupType"]))
			{
				textBox.Text = base.Request.QueryString["GroupType"];
			}
			if (textBox.Text == "Distribution")
			{
				base.Title = Strings.NewDistributionGroupTitle;
				base.Caption = Strings.NewDistributionGroupCaption;
				return;
			}
			base.Title = Strings.NewSecurityGroupTitle;
			base.Caption = Strings.NewSecurityGroupCaption;
		}

		// Token: 0x040028B6 RID: 10422
		protected PropertyPageSheet wizard1;
	}
}
