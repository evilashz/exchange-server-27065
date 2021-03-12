using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005CA RID: 1482
	[ClientScriptResource("RadioButtonList", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class EcpRadioButtonList : RadioButtonList, IScriptControl, INamingContainer
	{
		// Token: 0x06004324 RID: 17188 RVA: 0x000CBA11 File Offset: 0x000C9C11
		protected override void OnPreRender(EventArgs e)
		{
			if (this.Page != null)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<EcpRadioButtonList>(this);
			}
			base.OnPreRender(e);
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000CBA33 File Offset: 0x000C9C33
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
			base.Render(writer);
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x000CBA58 File Offset: 0x000C9C58
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("RadioButtonList", this.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x000CBA84 File Offset: 0x000C9C84
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(EcpRadioButtonList));
		}
	}
}
