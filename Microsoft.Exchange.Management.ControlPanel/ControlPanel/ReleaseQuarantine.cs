using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000371 RID: 881
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(WizardForm))]
	[ClientScriptResource("ReleaseQuarantine", "Microsoft.Exchange.Management.ControlPanel.Client.Quarantine.js")]
	public class ReleaseQuarantine : BaseForm
	{
		// Token: 0x06003027 RID: 12327 RVA: 0x00092F34 File Offset: 0x00091134
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			RadioButton radioButton = (RadioButton)this.properties.Controls[0].FindControl("rbReleaseSelected");
			RadioButton radioButton2 = (RadioButton)this.properties.Controls[0].FindControl("rbReleaseAll");
			PickerContent pickerContent = (PickerContent)this.properties.Controls[0].FindControl("pickerContent");
			descriptor.AddComponentProperty("PickerContent", pickerContent.ClientID, true);
			descriptor.AddElementProperty("ReleaseAllRadioButton", radioButton2.ClientID, true);
			descriptor.AddElementProperty("ReleaseSelectedRadioButton", radioButton.ClientID, true);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x0400234E RID: 9038
		protected PropertyPageSheet properties;
	}
}
