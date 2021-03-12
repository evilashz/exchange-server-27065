using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200036E RID: 878
	public class EditPostToolbar : Toolbar
	{
		// Token: 0x06002104 RID: 8452 RVA: 0x000BDF7C File Offset: 0x000BC17C
		internal EditPostToolbar(Importance importance, Markup currentMarkup) : base(ToolbarType.Form)
		{
			this.initialImportance = importance;
			this.initialMarkup = currentMarkup;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000BDF9C File Offset: 0x000BC19C
		protected override void RenderButtons()
		{
			base.RenderHelpButton(HelpIdsLight.MailLight.ToString(), string.Empty);
			base.RenderButton(ToolbarButtons.Post);
			base.RenderButton(ToolbarButtons.AttachFile);
			base.RenderButton(ToolbarButtons.ImportanceHigh, (this.initialImportance == Importance.High) ? ToolbarButtonFlags.Pressed : ToolbarButtonFlags.None);
			base.RenderButton(ToolbarButtons.ImportanceLow, (this.initialImportance == Importance.Low) ? ToolbarButtonFlags.Pressed : ToolbarButtonFlags.None);
			base.RenderButton(ToolbarButtons.Flag);
			base.RenderButton(ToolbarButtons.Categories);
			if (base.UserContext.IsFeatureEnabled(Feature.Signature))
			{
				base.RenderButton(ToolbarButtons.InsertSignature);
			}
			bool flag = base.UserContext.BrowserType == BrowserType.IE;
			if (flag)
			{
				ToolbarButtonFlags flags = ToolbarButtonFlags.None;
				if (!base.UserContext.IsFeatureEnabled(Feature.SpellChecker))
				{
					flags = ToolbarButtonFlags.Disabled;
				}
				base.RenderButton(ToolbarButtons.SpellCheck, flags, new Toolbar.RenderMenuItems(base.RenderSpellCheckLanguageDialog));
			}
			string currentSelection = "0";
			if (this.initialMarkup != Markup.Html)
			{
				currentSelection = "1";
			}
			base.RenderHtmlTextToggle(currentSelection);
		}

		// Token: 0x04001797 RID: 6039
		private Importance initialImportance = Importance.Normal;

		// Token: 0x04001798 RID: 6040
		private Markup initialMarkup;
	}
}
