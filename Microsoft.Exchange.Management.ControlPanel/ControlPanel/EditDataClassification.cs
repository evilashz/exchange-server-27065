using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001B8 RID: 440
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(WizardForm))]
	[ClientScriptResource("EditDataClassification", "Microsoft.Exchange.Management.ControlPanel.Client.DLPPolicy.js")]
	public class EditDataClassification : BaseForm
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x0006DE6D File Offset: 0x0006C06D
		public EditDataClassification()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x0006DE7B File Offset: 0x0006C07B
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddComponentProperty("CollectionEditor", this.lvFingerprints.ClientID, true);
			descriptor.AddComponentProperty("AjaxUploader", this.fingerprintUploadHandler.ClientID, true);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04001E35 RID: 7733
		protected FingerprintCollectionEditor lvFingerprints;

		// Token: 0x04001E36 RID: 7734
		protected FingerprintUploader fingerprintUploadHandler;
	}
}
