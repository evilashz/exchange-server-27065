using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200064A RID: 1610
	[ToolboxData("<{0}:RoleGroupPickerControl runat=server></{0}:RoleGroupPickerControl>")]
	[ClientScriptResource("RoleGroupPickerControl", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class RoleGroupPickerControl : PickerControl
	{
		// Token: 0x06004655 RID: 18005 RVA: 0x000D4BA0 File Offset: 0x000D2DA0
		public RoleGroupPickerControl()
		{
			base.ValueProperty = "RoleGroupObjectIdentity";
			base.DisplayProperty = "DisplayName";
			base.PickerFormUrl = "~/Pickers/RoleGroupPicker.aspx";
		}
	}
}
