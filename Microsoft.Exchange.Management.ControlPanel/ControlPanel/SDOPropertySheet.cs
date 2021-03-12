using System;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200064E RID: 1614
	[ClientScriptResource("SDOPropertySheet", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[RequiredScript(typeof(DetailsPane))]
	public class SDOPropertySheet : PropertyPageSheet
	{
		// Token: 0x06004671 RID: 18033 RVA: 0x000D535C File Offset: 0x000D355C
		public SDOPropertySheet()
		{
			base.UseWarningPanel = true;
			base.HasSaveMethod = false;
			this.CssClass = "DetailsProperties";
			base.ViewModel = "SDOViewModel";
		}
	}
}
