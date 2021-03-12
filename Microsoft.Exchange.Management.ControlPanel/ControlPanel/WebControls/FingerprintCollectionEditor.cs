using System;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005E3 RID: 1507
	[ToolboxData("<{0}:FingerprintCollectionEditor runat=server></{0}:FingerprintCollectionEditor>")]
	[ClientScriptResource("FingerprintCollectionEditor", "Microsoft.Exchange.Management.ControlPanel.Client.DLPPolicy.js")]
	[DefaultProperty("Text")]
	[ControlValueProperty("Values")]
	public class FingerprintCollectionEditor : EcpCollectionEditor
	{
	}
}
