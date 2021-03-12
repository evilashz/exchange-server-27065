using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000620 RID: 1568
	[ToolboxData("<{0}:NumberEditControl runat=server></{0}:NumberEditControl >")]
	[ClientScriptResource("NumberEditControl", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class NumberEditControl : StringEditControl
	{
		// Token: 0x06004596 RID: 17814 RVA: 0x000D24AC File Offset: 0x000D06AC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			NumericInputExtender numericInputExtender = new NumericInputExtender();
			numericInputExtender.TargetControlID = this.textBox.UniqueID;
			this.Controls.Add(numericInputExtender);
		}
	}
}
