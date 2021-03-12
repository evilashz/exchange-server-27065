using System;
using System.Web.UI;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000CF RID: 207
	public abstract class VoicemailPropertiesBase : Properties
	{
		// Token: 0x06001D58 RID: 7512 RVA: 0x0005A0FF File Offset: 0x000582FF
		public VoicemailPropertiesBase()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0005A110 File Offset: 0x00058310
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.AddElementProperty("CaptionLabel", base.GetCaptionLabel().ClientID);
			return scriptDescriptor;
		}
	}
}
