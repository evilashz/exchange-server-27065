using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000403 RID: 1027
	internal sealed class RecipientAnrContextMenu : ContextMenu
	{
		// Token: 0x06002555 RID: 9557 RVA: 0x000D8149 File Offset: 0x000D6349
		public RecipientAnrContextMenu(UserContext userContext) : base("divAm", userContext, false, true)
		{
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000D8159 File Offset: 0x000D6359
		private void RenderAnrHeader(TextWriter output)
		{
			output.Write("<div class=\"sttc\" nowrap><span id=\"spnImg\" class=\"cmIco\"></span>");
			RenderingUtilities.RenderInlineSpacer(output, this.userContext, 12);
			output.Write("<span id=\"spnHdr\"></span></div>");
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000D8180 File Offset: 0x000D6380
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			this.RenderAnrHeader(output);
			output.Write("<div id=divAmNms></div>");
			ContextMenu.RenderMenuDivider(output, "divRemoveDivider", false);
			base.RenderMenuItem(output, 1388922078, ThemeFileId.None, "divAmRmv", "rmv");
		}
	}
}
