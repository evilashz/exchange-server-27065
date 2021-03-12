using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000320 RID: 800
	public sealed class BuddyTreeContextMenuNew : ContextMenu
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x000AF7C5 File Offset: 0x000AD9C5
		public BuddyTreeContextMenuNew(UserContext userContext) : base("divBtmNew", userContext)
		{
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000AF7D4 File Offset: 0x000AD9D4
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, 1939053649, ThemeFileId.None, "divBtmNewGroup", "newGroup");
			base.RenderMenuItem(output, 252880604, ThemeFileId.None, "divBtmNewContact", "newContact");
			base.RenderMenuItem(output, 252880604, ThemeFileId.None, "divBtmNewOcsContact", "newOcsContact");
		}
	}
}
