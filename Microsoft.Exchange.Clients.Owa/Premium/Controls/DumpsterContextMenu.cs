using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200035E RID: 862
	public sealed class DumpsterContextMenu : ContextMenu
	{
		// Token: 0x06002080 RID: 8320 RVA: 0x000BC037 File Offset: 0x000BA237
		public DumpsterContextMenu(UserContext userContext) : base("divVwm", userContext)
		{
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000BC048 File Offset: 0x000BA248
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, -1604222507, ThemeFileId.Delete, "divp", "delete");
			ContextMenu.RenderMenuDivider(output, "divS1");
			base.RenderMenuItem(output, -1010127550, ThemeFileId.Recover, "divr", "recover");
		}

		// Token: 0x04001770 RID: 6000
		private const string ContextMenuId = "divVwm";
	}
}
