using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001E1 RID: 481
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Extension.js")]
	[RequiredScript(typeof(Properties))]
	public class ExtensionWizardProperties : Properties
	{
		// Token: 0x060025BB RID: 9659 RVA: 0x000742B1 File Offset: 0x000724B1
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			base.UseSetObject = false;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000742C0 File Offset: 0x000724C0
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "ExtensionWizardProperties";
			return scriptDescriptor;
		}
	}
}
