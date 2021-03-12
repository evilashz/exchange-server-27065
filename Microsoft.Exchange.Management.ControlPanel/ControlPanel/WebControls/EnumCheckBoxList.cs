using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005D9 RID: 1497
	[ClientScriptResource("EnumCheckBoxList", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[ControlValueProperty("Value")]
	public class EnumCheckBoxList : CheckBoxList, IScriptControl, INamingContainer
	{
		// Token: 0x06004364 RID: 17252 RVA: 0x000CC19C File Offset: 0x000CA39C
		protected override void OnPreRender(EventArgs e)
		{
			base.Attributes.Add("cellspacing", "0");
			base.Attributes.Add("cellpadding", "0");
			if (this.Page != null)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<EnumCheckBoxList>(this);
			}
			base.OnPreRender(e);
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x000CC1F3 File Offset: 0x000CA3F3
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
			base.Render(writer);
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000CC218 File Offset: 0x000CA418
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("EnumCheckBoxList", this.ClientID);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000CC244 File Offset: 0x000CA444
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(EnumCheckBoxList));
		}
	}
}
