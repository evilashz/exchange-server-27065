using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000581 RID: 1409
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("RecipientConditionEditor", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public abstract class RecipientConditionEditorBase : ScriptControlBase, INamingContainer
	{
		// Token: 0x17002559 RID: 9561
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x000C7313 File Offset: 0x000C5513
		// (set) Token: 0x06004164 RID: 16740 RVA: 0x000C731B File Offset: 0x000C551B
		[DefaultValue(false)]
		public bool HasDefaultItem { get; set; }

		// Token: 0x1700255A RID: 9562
		// (get) Token: 0x06004165 RID: 16741
		protected abstract RulePhrase[] SupportedConditions { get; }

		// Token: 0x06004166 RID: 16742 RVA: 0x000C7324 File Offset: 0x000C5524
		public RecipientConditionEditorBase() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000C732E File Offset: 0x000C552E
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddScriptProperty("AllConditions", this.SupportedConditions.ToJsonString(null));
			descriptor.AddProperty("HasDefaultItem", this.HasDefaultItem);
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000C7364 File Offset: 0x000C5564
		protected override void CreateChildControls()
		{
			List<Type> list = new List<Type>();
			RuleEditor.GetRequiredFormlets(this.SupportedConditions, list);
			Panel panel = new Panel();
			panel.Style.Add(HtmlTextWriterStyle.Display, "none");
			foreach (Type type in list)
			{
				if (string.Equals(type.Name, "PeoplePicker", StringComparison.Ordinal))
				{
					PeoplePicker peoplePicker = (PeoplePicker)Activator.CreateInstance(type);
					peoplePicker.PreferOwaPicker = false;
					peoplePicker.AllowTyping = true;
					peoplePicker.IsStandalonePicker = false;
					panel.Controls.Add(peoplePicker);
				}
				else
				{
					panel.Controls.Add((Control)Activator.CreateInstance(type));
				}
			}
			this.Controls.Add(panel);
			base.CreateChildControls();
		}
	}
}
