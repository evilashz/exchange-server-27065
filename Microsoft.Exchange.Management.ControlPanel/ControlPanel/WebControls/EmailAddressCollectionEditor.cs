using System;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005D4 RID: 1492
	[ControlValueProperty("Values")]
	[ClientScriptResource("EmailAddressCollectionEditor", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:EmailAddressCollectionEditor runat=server></{0}:EmailAddressCollectionEditor>")]
	[DefaultProperty("Text")]
	public class EmailAddressCollectionEditor : EcpCollectionEditor
	{
		// Token: 0x0600435A RID: 17242 RVA: 0x000CC021 File Offset: 0x000CA221
		public EmailAddressCollectionEditor()
		{
			base.DialogWidth = 600;
		}
	}
}
