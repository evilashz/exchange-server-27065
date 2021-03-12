using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000341 RID: 833
	public sealed class CopyMoveContextMenu : ContextMenu
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x000B5F0B File Offset: 0x000B410B
		internal CopyMoveContextMenu(UserContext userContext) : base("divCMM", userContext)
		{
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x000B5F19 File Offset: 0x000B4119
		protected override bool HasShadedColumn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000B5F1C File Offset: 0x000B411C
		protected override void RenderMenuItems(TextWriter output)
		{
			TargetFolderMRU.GetFolders(base.UserContext, out this.mruFolderIds, out this.mruFolderNames, out this.mruFolderClassNames, out this.mruFolderCount);
			this.currentMenuItemIndex = 0;
			while (this.currentMenuItemIndex < this.mruFolderCount)
			{
				string text = this.mruFolderNames[this.currentMenuItemIndex];
				if (text.Length > 40)
				{
					text = text.Substring(0, 40);
					text += "...";
				}
				string str = this.mruFolderIds[this.currentMenuItemIndex].ToString();
				base.RenderMenuItem(output, text, ThemeFileId.None, "b" + str, "MvToMruF", false, null, null);
				this.currentMenuItemIndex++;
			}
			if (this.mruFolderCount > 0)
			{
				ContextMenu.RenderMenuDivider(output, "divCMMDvd");
			}
			base.RenderMenuItem(output, -1664268159, ThemeFileId.Move, "divMoveToFolder", "MvToF");
			base.RenderMenuItem(output, -1581636675, ThemeFileId.CopyToFolder, "divCopyToFolder", "CpToF");
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000B6018 File Offset: 0x000B4218
		protected override void RenderMenuItemExpandoData(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (this.currentMenuItemIndex >= this.mruFolderCount)
			{
				return;
			}
			output.Write(" sT=\"");
			output.Write(this.mruFolderClassNames[this.currentMenuItemIndex]);
			output.Write("\"");
		}

		// Token: 0x040016F8 RID: 5880
		private const int FolderDisplayNameLengthMax = 40;

		// Token: 0x040016F9 RID: 5881
		private int currentMenuItemIndex;

		// Token: 0x040016FA RID: 5882
		private OwaStoreObjectId[] mruFolderIds;

		// Token: 0x040016FB RID: 5883
		private string[] mruFolderNames;

		// Token: 0x040016FC RID: 5884
		private string[] mruFolderClassNames;

		// Token: 0x040016FD RID: 5885
		private int mruFolderCount;
	}
}
