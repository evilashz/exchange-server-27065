using System;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000050 RID: 80
	[ClientScriptResource("FacebookUserConsentForm", "Microsoft.Exchange.Management.ControlPanel.Client.Connect.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class FacebookUserConsentForm : SlabControl
	{
		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00052EB0 File Offset: 0x000510B0
		// (set) Token: 0x060019D7 RID: 6615 RVA: 0x00052EB8 File Offset: 0x000510B8
		public string AuthorizationUrl { get; set; }

		// Token: 0x060019D8 RID: 6616 RVA: 0x00052EC1 File Offset: 0x000510C1
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.userConsentForm.Attributes.Add("vm-AuthorizationUrl", this.AuthorizationUrl);
		}

		// Token: 0x04001AE6 RID: 6886
		private const string FacebookUserConsentFormComponent = "FacebookUserConsentForm";

		// Token: 0x04001AE7 RID: 6887
		protected HtmlGenericControl userConsentForm;
	}
}
