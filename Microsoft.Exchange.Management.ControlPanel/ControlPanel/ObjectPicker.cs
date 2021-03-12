using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000625 RID: 1573
	[ControlValueProperty("Value")]
	[ClientScriptResource("ObjectPicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ToolboxData("<{0}:ObjectPicker runat=server></{0}:ObjectPicker>")]
	public class ObjectPicker : PickerControlBase
	{
		// Token: 0x060045A0 RID: 17824 RVA: 0x000D27CA File Offset: 0x000D09CA
		public ObjectPicker() : base(HtmlTextWriterTag.Div)
		{
			base.ValueProperty = "Identity";
			base.PickerFormUrl = "~/pickerurlplaceholder.aspx";
		}
	}
}
