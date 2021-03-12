using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200035B RID: 859
	public sealed class DocumentViewContextMenu : ContextMenu
	{
		// Token: 0x06002067 RID: 8295 RVA: 0x000BB7D5 File Offset: 0x000B99D5
		public DocumentViewContextMenu(UserContext userContext) : base("divVwm", userContext)
		{
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000BB7E4 File Offset: 0x000B99E4
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, 197744374, ThemeFileId.None, "divO", "opndc");
			base.RenderMenuItem(output, 1927924110, ThemeFileId.None, "divON", "opndlinnew");
			base.RenderMenuItem(output, 1547877601, ThemeFileId.HtmlDocument, "divOAWP", "opnwp");
			base.RenderMenuItem(output, 11725295, ThemeFileId.Send, "divS", "senddc");
			base.RenderMenuItem(output, 636550561, ThemeFileId.Copy, "divCS", "copyuri");
			base.RenderMenuItem(output, -1028120515, ThemeFileId.None, "divAF", "addfav");
			ContextMenu.RenderMenuDivider(output, "divS1");
			base.RenderMenuItem(output, string.Empty, ThemeFileId.Contact, "divIMB", "infomb", false, "shwNms(0)", null);
			base.RenderMenuItem(output, string.Empty, ThemeFileId.Contact, "divICOT", "infocot", false, "shwNms(1)", null);
		}
	}
}
