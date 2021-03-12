using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000359 RID: 857
	public sealed class DocumentFolderContextMenu : ContextMenu
	{
		// Token: 0x06002063 RID: 8291 RVA: 0x000BB630 File Offset: 0x000B9830
		public DocumentFolderContextMenu(UserContext userContext) : base("divDfm", userContext)
		{
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000BB640 File Offset: 0x000B9840
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, 197744374, ThemeFileId.None, "divDfmO", "opendl");
			base.RenderMenuItem(output, 839524911, ThemeFileId.None, "divDfmON", "opennewdl");
			base.RenderMenuItem(output, 461135208, ThemeFileId.None, "divDfmR", "renamedl");
			base.RenderMenuItem(output, 1381996313, ThemeFileId.Delete, "divDfmD", "deletedl");
		}
	}
}
