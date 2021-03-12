using System;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005A8 RID: 1448
	[ControlValueProperty("Values")]
	[ClientScriptResource("DLPCollectionEditor", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:DLPCollectionEditor runat=server></{0}:DLPCollectionEditor>")]
	[DefaultProperty("Text")]
	public class DLPCollectionEditor : EcpCollectionEditor
	{
	}
}
