using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000D2 RID: 210
	[ClientScriptResource("VoicemailSettingsProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Voicemail.js")]
	public sealed class VoicemailSettingsProperties : VoicemailPropertiesBase
	{
		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0005A28E File Offset: 0x0005848E
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0005A296 File Offset: 0x00058496
		public WebServiceMethod ResetPINWebServiceMethod { get; private set; }

		// Token: 0x06001D64 RID: 7524 RVA: 0x0005A2A0 File Offset: 0x000584A0
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.ResetPINWebServiceMethod = new WebServiceMethod();
			this.ResetPINWebServiceMethod.ServiceUrl = base.ServiceUrl;
			this.ResetPINWebServiceMethod.ID = "ResetPIN";
			this.ResetPINWebServiceMethod.Method = "ResetPIN";
			this.ResetPINWebServiceMethod.ParameterNames = WebServiceParameterNames.Identity;
			this.Controls.Add(this.ResetPINWebServiceMethod);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0005A30C File Offset: 0x0005850C
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "VoicemailSettingsProperties";
			scriptDescriptor.AddComponentProperty("ResetPINWebServiceMethod", this.ResetPINWebServiceMethod.ClientID);
			return scriptDescriptor;
		}
	}
}
