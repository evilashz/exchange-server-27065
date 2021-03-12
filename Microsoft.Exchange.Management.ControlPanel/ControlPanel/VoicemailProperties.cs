using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000D1 RID: 209
	[ClientScriptResource("VoicemailProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Voicemail.js")]
	public sealed class VoicemailProperties : VoicemailPropertiesBase
	{
		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0005A191 File Offset: 0x00058391
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0005A199 File Offset: 0x00058399
		public WebServiceMethod ClearSettingsWebServiceMethod { get; private set; }

		// Token: 0x06001D5E RID: 7518 RVA: 0x0005A1A4 File Offset: 0x000583A4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			CommonMaster commonMaster = (CommonMaster)this.Page.Master;
			commonMaster.AddCssFiles("VoicemailSprite.css");
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0005A1D4 File Offset: 0x000583D4
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.ClearSettingsWebServiceMethod = new WebServiceMethod();
			this.ClearSettingsWebServiceMethod.ServiceUrl = base.ServiceUrl;
			this.ClearSettingsWebServiceMethod.ID = "ClearSettings";
			this.ClearSettingsWebServiceMethod.Method = "ClearSettings";
			this.ClearSettingsWebServiceMethod.ParameterNames = WebServiceParameterNames.Identity;
			this.Controls.Add(this.ClearSettingsWebServiceMethod);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0005A240 File Offset: 0x00058440
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "VoicemailProperties";
			scriptDescriptor.AddComponentProperty("ClearSettingsWebServiceMethod", this.ClearSettingsWebServiceMethod.ClientID);
			scriptDescriptor.AddProperty("LCID", Util.GetLCID());
			return scriptDescriptor;
		}
	}
}
