using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000034 RID: 52
	public class ViewAddressList : OrgSettingsContentPage
	{
		// Token: 0x06001944 RID: 6468 RVA: 0x0004F188 File Offset: 0x0004D388
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			string text = base.Request.QueryString["altype"];
			if (text.Equals("al", StringComparison.OrdinalIgnoreCase))
			{
				this.propertySheel.ServiceUrl = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=AddressList&workflow=GetForSDO");
				base.Title = Strings.ViewAddressList;
				this.typeLabel.Text = Strings.AddressList;
				this.powershellMsg.Text = Strings.ALRecipientFilterDescription;
				return;
			}
			if (text.Equals("gal", StringComparison.OrdinalIgnoreCase))
			{
				this.propertySheel.ServiceUrl = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=GlobalAddressList&workflow=GetGALSDO");
				base.Title = Strings.ViewGAL;
				this.typeLabel.Text = Strings.GAL;
				this.powershellMsg.Text = Strings.GALRecipientFilterDescription;
				return;
			}
			throw new BadQueryParameterException("altype");
		}

		// Token: 0x04001AA5 RID: 6821
		private const string ALTypeKey = "altype";

		// Token: 0x04001AA6 RID: 6822
		private const string AL = "al";

		// Token: 0x04001AA7 RID: 6823
		private const string GAL = "gal";

		// Token: 0x04001AA8 RID: 6824
		protected PropertyPageSheet propertySheel;

		// Token: 0x04001AA9 RID: 6825
		protected Label typeLabel;

		// Token: 0x04001AAA RID: 6826
		protected Label powershellMsg;
	}
}
