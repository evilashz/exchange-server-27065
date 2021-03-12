using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005FD RID: 1533
	[ControlValueProperty("Value")]
	[ClientScriptResource("LegacyEnumCheckBoxList", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class LegacyEnumCheckBoxList : CheckBoxList, IScriptControl, INamingContainer
	{
		// Token: 0x060044C0 RID: 17600 RVA: 0x000CF910 File Offset: 0x000CDB10
		protected override void OnPreRender(EventArgs e)
		{
			base.Attributes.Add("cellspacing", "0");
			base.Attributes.Add("cellpadding", "0");
			if (this.Page != null)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<LegacyEnumCheckBoxList>(this);
			}
			base.OnPreRender(e);
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x000CF967 File Offset: 0x000CDB67
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
			base.Render(writer);
		}

		// Token: 0x17002691 RID: 9873
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x000CF98C File Offset: 0x000CDB8C
		public int[] CheckBoxValues
		{
			get
			{
				int[] array = new int[this.Items.Count];
				for (int i = 0; i < this.Items.Count; i++)
				{
					array[i] = int.Parse(this.Items[i].Value);
				}
				return array;
			}
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000CF9DC File Offset: 0x000CDBDC
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptComponentDescriptor scriptComponentDescriptor = new ScriptComponentDescriptor("LegacyEnumCheckBoxList");
			scriptComponentDescriptor.AddProperty("id", this.ClientID);
			scriptComponentDescriptor.AddProperty("CheckBoxValues", this.CheckBoxValues);
			return new ScriptDescriptor[]
			{
				scriptComponentDescriptor
			};
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x000CFA24 File Offset: 0x000CDC24
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(LegacyEnumCheckBoxList));
		}
	}
}
