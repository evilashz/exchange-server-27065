using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001B7 RID: 439
	[ClientScriptResource("NewDataClassification", "Microsoft.Exchange.Management.ControlPanel.Client.DLPPolicy.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(WizardForm))]
	public class NewDataClassification : BaseForm
	{
		// Token: 0x060023DA RID: 9178 RVA: 0x0006DE28 File Offset: 0x0006C028
		public NewDataClassification()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x0006DE36 File Offset: 0x0006C036
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddComponentProperty("CollectionEditor", this.lvFingerprints.ClientID, true);
			descriptor.AddComponentProperty("AjaxUploader", this.fingerprintUploadHandler.ClientID, true);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04001E33 RID: 7731
		protected FingerprintCollectionEditor lvFingerprints;

		// Token: 0x04001E34 RID: 7732
		protected FingerprintUploader fingerprintUploadHandler;
	}
}
