using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000358 RID: 856
	public sealed class DocumentBreadcrumbBarContextMenu : ContextMenu
	{
		// Token: 0x06002061 RID: 8289 RVA: 0x000BB5E3 File Offset: 0x000B97E3
		public DocumentBreadcrumbBarContextMenu(UserContext userContext) : base("divDbcbm", userContext, false)
		{
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000BB5F2 File Offset: 0x000B97F2
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, -1028120515, ThemeFileId.None, "divAFB", "addfavbcb");
			base.RenderMenuItem(output, 636550561, ThemeFileId.None, "divCSB", "copyuribcb");
		}
	}
}
