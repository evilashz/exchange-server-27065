using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200034C RID: 844
	public sealed class DefaultSearchScopeMenu : ContextMenu
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x000B8201 File Offset: 0x000B6401
		internal static DefaultSearchScopeMenu Create(UserContext userContext, OutlookModule outlookModule, string archiveName)
		{
			return new DefaultSearchScopeMenu(userContext, outlookModule, archiveName);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000B820B File Offset: 0x000B640B
		private DefaultSearchScopeMenu(UserContext userContext, OutlookModule outlookModule, string archiveName) : base("divDftScpMnu", userContext, true)
		{
			this.outlookModule = outlookModule;
			this.archiveName = archiveName;
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000B8228 File Offset: 0x000B6428
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			SearchScope searchScope = this.userContext.UserOptions.GetSearchScope(this.outlookModule);
			base.RenderMenuItem(output, 1749416719, (searchScope == SearchScope.SelectedFolder) ? ThemeFileId.Search : ThemeFileId.None, "divFS" + DefaultSearchScopeMenu.selectedFolder, "d" + DefaultSearchScopeMenu.selectedFolder, false);
			base.RenderMenuItem(output, -1578460849, (searchScope == SearchScope.SelectedAndSubfolders) ? ThemeFileId.Search : ThemeFileId.None, "divFS" + DefaultSearchScopeMenu.selectedAndSubFolders, "d" + DefaultSearchScopeMenu.selectedAndSubFolders, false);
			if (this.outlookModule == OutlookModule.Tasks || this.outlookModule == OutlookModule.Contacts)
			{
				Strings.IDs displayString = (this.outlookModule == OutlookModule.Tasks) ? -464657744 : -1237143503;
				base.RenderMenuItem(output, displayString, (searchScope == SearchScope.AllItemsInModule) ? ThemeFileId.Search : ThemeFileId.None, "divFS" + DefaultSearchScopeMenu.allItemsInModule, "d" + DefaultSearchScopeMenu.allItemsInModule, false);
				return;
			}
			string text;
			if (!string.IsNullOrEmpty(this.archiveName))
			{
				text = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-1597765325), new object[]
				{
					this.archiveName
				});
			}
			else
			{
				text = LocalizedStrings.GetNonEncoded(591328129);
			}
			base.RenderMenuItem(output, text, (searchScope == SearchScope.AllFoldersAndItems) ? ThemeFileId.Search : ThemeFileId.None, "divFS" + DefaultSearchScopeMenu.allFolders, "d" + DefaultSearchScopeMenu.allFolders, false, null, null);
		}

		// Token: 0x04001725 RID: 5925
		private static readonly string selectedFolder = 0.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001726 RID: 5926
		private static readonly string selectedAndSubFolders = 1.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001727 RID: 5927
		private static readonly string allItemsInModule = 2.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001728 RID: 5928
		private static readonly string allFolders = 3.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001729 RID: 5929
		private OutlookModule outlookModule;

		// Token: 0x0400172A RID: 5930
		private string archiveName;
	}
}
