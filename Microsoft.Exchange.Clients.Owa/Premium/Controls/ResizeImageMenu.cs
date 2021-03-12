using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200040A RID: 1034
	public sealed class ResizeImageMenu : ContextMenu
	{
		// Token: 0x06002567 RID: 9575 RVA: 0x000D88F5 File Offset: 0x000D6AF5
		internal ResizeImageMenu(UserContext userContext) : base("divRszMnu", userContext, true)
		{
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000D8904 File Offset: 0x000D6B04
		private void RenderResizeMenuHeader(TextWriter output)
		{
			output.Write("<div class=\"rszHd\" nowrap>");
			output.Write(LocalizedStrings.GetNonEncoded(2054402001));
			output.Write("</div>");
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000D892C File Offset: 0x000D6B2C
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			this.RenderResizeMenuHeader(output);
			base.RenderMenuItem(output, -21894641, ThemeFileId.Clear, null, "25");
			base.RenderMenuItem(output, -787528491, ThemeFileId.Clear, null, "50");
			base.RenderMenuItem(output, 1852211349, ThemeFileId.Clear, null, "100");
			base.RenderMenuItem(output, -668906919, ThemeFileId.Clear, null, "200");
			ContextMenu.RenderMenuDivider(output, null);
			base.RenderMenuItem(output, 323750620, ThemeFileId.Clear, null, "fitToWindow");
		}

		// Token: 0x040019E4 RID: 6628
		private const string ResizeImageMenuId = "divRszMnu";
	}
}
