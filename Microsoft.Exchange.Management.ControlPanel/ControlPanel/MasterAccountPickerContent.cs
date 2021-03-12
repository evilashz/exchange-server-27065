using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000339 RID: 825
	[ToolboxData("<{0}:MasterAccountPickerContent runat=server></{0}:PickerContent>")]
	[ClientScriptResource("MasterAccountPickerContent", "Microsoft.Exchange.Management.ControlPanel.Client.Pickers.js")]
	public class MasterAccountPickerContent : PickerContent
	{
		// Token: 0x06002F35 RID: 12085 RVA: 0x00090029 File Offset: 0x0008E229
		public MasterAccountPickerContent()
		{
			base.ListView.ShowHeader = true;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x00090040 File Offset: 0x0008E240
		protected override void OnPreRender(EventArgs e)
		{
			ClientControlBinding clientControlBinding = new ComponentBinding(this, "LinkedDomainController");
			clientControlBinding.Name = "LinkedDomainController";
			base.FilterParameters.Add(clientControlBinding);
			clientControlBinding = new ComponentBinding(this, "UserName");
			clientControlBinding.Name = "UserName";
			base.FilterParameters.Add(clientControlBinding);
			clientControlBinding = new ComponentBinding(this, "Password");
			clientControlBinding.Name = "Password";
			base.FilterParameters.Add(clientControlBinding);
			base.OnPreRender(e);
		}
	}
}
