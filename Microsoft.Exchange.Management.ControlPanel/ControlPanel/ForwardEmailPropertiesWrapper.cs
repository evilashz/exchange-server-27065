using System;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A4 RID: 676
	[ClientScriptResource("ForwardEmailPropertiesWrapper", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public sealed class ForwardEmailPropertiesWrapper : PropertiesWrapper
	{
		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x00088102 File Offset: 0x00086302
		protected override bool IsToolbarRightAlign
		{
			get
			{
				return false;
			}
		}
	}
}
