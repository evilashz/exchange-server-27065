using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200031E RID: 798
	public sealed class BuddyTreeContextMenu : ContextMenu
	{
		// Token: 0x06001E5A RID: 7770 RVA: 0x000AF667 File Offset: 0x000AD867
		public BuddyTreeContextMenu(UserContext userContext) : base("divBtm", userContext)
		{
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000AF678 File Offset: 0x000AD878
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderHeader(output);
			base.RenderMenuItem(output, -124986716, ThemeFileId.Chat, "divBtmChat", "chat");
			ContextMenu.RenderMenuDivider(output, "divBtmS1");
			base.RenderMenuItem(output, -1273337860, ThemeFileId.New, "divBtmNewBase", null, false, null, null, new BuddyTreeContextMenuNew(this.userContext));
			base.RenderMenuItem(output, -1225440563, ThemeFileId.None, "divBtmRenameGroup", "renameGroup");
			base.RenderMenuItem(output, -297665725, ThemeFileId.None, "divBtmRemoveGroup", "removeGroup");
			ContextMenu.RenderMenuDivider(output, "divBtmS2");
			base.RenderMenuItem(output, -371326789, ThemeFileId.BigPresenceBlocked, "divBtmBlock", "block");
			base.RenderMenuItem(output, -153800658, ThemeFileId.BigPresenceBlocked, "divBtmUnblock", "unblock");
			base.RenderMenuItem(output, 1388922078, ThemeFileId.RemoveBuddy, "divBtmRmvBase", null, false, null, null, new BuddyTreeContextMenuRemove(this.userContext));
		}
	}
}
