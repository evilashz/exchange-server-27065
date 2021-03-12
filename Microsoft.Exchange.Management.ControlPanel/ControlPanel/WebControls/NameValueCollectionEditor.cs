using System;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200061C RID: 1564
	[ClientScriptResource("NameValueCollectionEditor", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:NameValueCollectionEditor runat=server></{0}:NameValueCollectionEditor>")]
	[ControlValueProperty("Values")]
	public class NameValueCollectionEditor : EcpCollectionEditor
	{
	}
}
