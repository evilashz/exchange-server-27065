using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005A5 RID: 1445
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxData("<{0}:DetailsTable runat=server></{0}:DetailsTable>")]
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class DetailsTable : HtmlTable, IScriptControl
	{
		// Token: 0x06004213 RID: 16915 RVA: 0x000C9539 File Offset: 0x000C7739
		protected override void OnPreRender(EventArgs e)
		{
			if (Util.IsSafari() || Util.IsFirefox() || Util.IsChrome())
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<DetailsTable>(this);
			}
			base.OnPreRender(e);
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000C9568 File Offset: 0x000C7768
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode && (Util.IsSafari() || Util.IsFirefox() || Util.IsChrome()))
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
			base.Render(writer);
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000C95A0 File Offset: 0x000C77A0
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("DetailsTable", this.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x000C95CC File Offset: 0x000C77CC
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(DetailsTable));
		}
	}
}
