using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000607 RID: 1543
	[ClientScriptResource("MoreOptionRecipientConditionEditor", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class MoreOptionRecipientConditionEditor : RecipientConditionEditorBase
	{
		// Token: 0x060044F4 RID: 17652 RVA: 0x000CFF8E File Offset: 0x000CE18E
		protected override void Render(HtmlTextWriter writer)
		{
			this.moreOptionId = this.ClientID + "_moreOption";
			writer.Write(string.Format("<a id=\"{0}\">{1}</a>", this.moreOptionId, Strings.MoreOptionsLabel));
			base.Render(writer);
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x000CFFCD File Offset: 0x000CE1CD
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("MoreOptionButton", this.moreOptionId);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04002E40 RID: 11840
		private string moreOptionId;
	}
}
