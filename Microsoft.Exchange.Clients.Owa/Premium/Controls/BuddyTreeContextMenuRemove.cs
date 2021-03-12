using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200031F RID: 799
	public sealed class BuddyTreeContextMenuRemove : ContextMenu
	{
		// Token: 0x06001E5C RID: 7772 RVA: 0x000AF779 File Offset: 0x000AD979
		public BuddyTreeContextMenuRemove(UserContext userContext) : base("divBtmRmv", userContext)
		{
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000AF787 File Offset: 0x000AD987
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, 699356425, ThemeFileId.None, "divBtmRmvGroup", "removeFromGroup");
			base.RenderMenuItem(output, -205408082, ThemeFileId.None, "divBtmRmvList", "removeFromList");
		}
	}
}
