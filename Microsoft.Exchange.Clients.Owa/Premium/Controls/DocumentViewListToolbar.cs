using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.DocumentLibrary;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200035C RID: 860
	public sealed class DocumentViewListToolbar : Toolbar
	{
		// Token: 0x06002069 RID: 8297 RVA: 0x000BB8DE File Offset: 0x000B9ADE
		internal DocumentViewListToolbar(IDocumentLibraryFolder libraryFolder, UriFlags libraryType, bool isRootFolder) : base("divTBL")
		{
			this.libraryFolder = libraryFolder;
			this.libraryType = libraryType;
			this.isRootFolder = isRootFolder;
			this.contextMenu = new DocumentBreadcrumbBarContextMenu(base.UserContext);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000BB918 File Offset: 0x000B9B18
		internal DocumentViewListToolbar(IDocumentLibrary documentLibrary, UriFlags libraryType, bool isRootFolder) : base("divTBL")
		{
			this.documentLibrary = documentLibrary;
			this.libraryType = libraryType;
			this.isRootFolder = isRootFolder;
			this.contextMenu = new DocumentBreadcrumbBarContextMenu(base.UserContext);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000BB952 File Offset: 0x000B9B52
		internal DocumentViewListToolbar(SharepointSession sharepointSession, UriFlags libraryType, bool isRootFolder) : base("divTBL")
		{
			this.sharepointSession = sharepointSession;
			this.libraryType = libraryType;
			this.isRootFolder = isRootFolder;
			this.contextMenu = new DocumentBreadcrumbBarContextMenu(base.UserContext);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000BB98C File Offset: 0x000B9B8C
		internal DocumentViewListToolbar(UncSession uncSession, UriFlags libraryType, bool isRootFolder) : base("divTBL")
		{
			this.uncSession = uncSession;
			this.libraryType = libraryType;
			this.isRootFolder = isRootFolder;
			this.contextMenu = new DocumentBreadcrumbBarContextMenu(base.UserContext);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000BB9C6 File Offset: 0x000B9BC6
		protected override void RenderButtons()
		{
			if (this.isRootFolder)
			{
				base.RenderButton(ToolbarButtons.ParentFolder, ToolbarButtonFlags.Disabled);
			}
			else
			{
				base.RenderButton(ToolbarButtons.ParentFolder);
			}
			if (this.CanAddLibraryToFavorites())
			{
				base.RenderButton(ToolbarButtons.AddToFavorites);
			}
			this.RenderBreadcrumbBar();
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000BBA08 File Offset: 0x000B9C08
		private bool CanAddLibraryToFavorites()
		{
			UriFlags uriFlags = this.libraryType;
			switch (uriFlags)
			{
			case UriFlags.Sharepoint:
			case UriFlags.Unc:
			case UriFlags.SharepointDocumentLibrary:
			case UriFlags.UncDocumentLibrary:
				break;
			case UriFlags.Sharepoint | UriFlags.Unc:
			case UriFlags.DocumentLibrary:
				return false;
			default:
				switch (uriFlags)
				{
				case UriFlags.SharepointFolder:
				case UriFlags.UncFolder:
					break;
				default:
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000BBA54 File Offset: 0x000B9C54
		private void RenderBreadcrumbBar()
		{
			base.Writer.Write("<td class=\"w100 lnkbr\"><img align=absmiddle height=16 width=0>&nbsp;");
			UriFlags uriFlags = this.libraryType;
			switch (uriFlags)
			{
			case UriFlags.Sharepoint:
				this.RenderSharepointSiteBreadcrumbs();
				break;
			case UriFlags.Unc:
				this.RenderUncSiteBreadcrumbs();
				break;
			case UriFlags.Sharepoint | UriFlags.Unc:
			case UriFlags.DocumentLibrary:
				break;
			case UriFlags.SharepointDocumentLibrary:
				this.RenderSharepointBreadcrumbs(this.documentLibrary.GetHierarchy());
				break;
			case UriFlags.UncDocumentLibrary:
				this.RenderUncBreadcrumbs();
				break;
			default:
				switch (uriFlags)
				{
				case UriFlags.SharepointFolder:
					this.RenderSharepointBreadcrumbs(this.libraryFolder.GetHierarchy());
					break;
				case UriFlags.UncFolder:
					this.RenderUncBreadcrumbs();
					break;
				}
				break;
			}
			this.contextMenu.Render(base.Writer);
			base.Writer.Write("</td>");
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000BBB10 File Offset: 0x000B9D10
		private void RenderSharepointSiteBreadcrumbs()
		{
			this.RenderCrumb(this.sharepointSession.DisplayName, new Strings.IDs?(-527057840), this.sharepointSession.Uri.ToString(), UriFlags.Sharepoint, true);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000BBB3F File Offset: 0x000B9D3F
		private void RenderUncSiteBreadcrumbs()
		{
			base.Writer.Write("\\\\ ");
			this.RenderCrumb(this.uncSession.Title, this.uncSession.Uri.ToString(), UriFlags.Unc, true);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000BBB74 File Offset: 0x000B9D74
		private void RenderSharepointBreadcrumbs(List<KeyValuePair<string, Uri>> crumbs)
		{
			bool flag = false;
			UriFlags[] array = new UriFlags[]
			{
				UriFlags.Sharepoint,
				UriFlags.SharepointDocumentLibrary,
				UriFlags.SharepointFolder
			};
			for (int i = 0; i < crumbs.Count; i++)
			{
				if (flag)
				{
					base.Writer.Write(" / ");
				}
				else
				{
					flag = true;
				}
				this.RenderCrumb(crumbs[i].Key, crumbs[i].Value.AbsoluteUri, (i < array.Length) ? array[i] : UriFlags.SharepointFolder, false);
			}
			if (flag)
			{
				base.Writer.Write(" / ");
			}
			if (this.libraryFolder != null)
			{
				this.RenderCrumb(this.libraryFolder.DisplayName, new Strings.IDs?(-527057840), this.libraryFolder.Uri.ToString(), UriFlags.SharepointFolder, true);
				return;
			}
			if (this.documentLibrary != null)
			{
				this.RenderCrumb(this.documentLibrary.Title, new Strings.IDs?(477016274), this.documentLibrary.Uri.ToString(), UriFlags.SharepointDocumentLibrary, true);
			}
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000BBC7C File Offset: 0x000B9E7C
		private void RenderUncBreadcrumbs()
		{
			List<KeyValuePair<string, Uri>> list = (this.libraryFolder != null) ? this.libraryFolder.GetHierarchy() : this.documentLibrary.GetHierarchy();
			bool flag = false;
			UriFlags[] array = new UriFlags[]
			{
				UriFlags.Unc,
				UriFlags.UncDocumentLibrary,
				UriFlags.UncFolder
			};
			base.Writer.Write("<span dir=\"ltr\" class=\"nolnk\">\\\\</span> ");
			for (int i = 0; i < list.Count; i++)
			{
				if (flag)
				{
					base.Writer.Write(" \\ ");
				}
				else
				{
					flag = true;
				}
				this.RenderCrumb(list[i].Key.TrimEnd(new char[]
				{
					'/'
				}), list[i].Value.LocalPath.TrimEnd(new char[]
				{
					'\\'
				}), (i < array.Length) ? array[i] : UriFlags.UncFolder, false);
			}
			if (flag)
			{
				base.Writer.Write(" \\ ");
			}
			if (this.libraryFolder != null)
			{
				this.RenderCrumb(this.libraryFolder.DisplayName, new Strings.IDs?(-527057840), this.libraryFolder.Uri.ToString(), (list.Count == 2) ? UriFlags.UncDocumentLibrary : UriFlags.UncFolder, true);
				return;
			}
			this.RenderCrumb(this.documentLibrary.Title, new Strings.IDs?(477016274), this.documentLibrary.Uri.ToString(), (list.Count == 2) ? UriFlags.UncDocumentLibrary : UriFlags.UncFolder, true);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000BBDF8 File Offset: 0x000B9FF8
		private void RenderCrumb(string displayName, string uri, UriFlags libraryType, bool refreshViewer)
		{
			this.RenderCrumb(displayName, null, uri, libraryType, refreshViewer);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000BBE1C File Offset: 0x000BA01C
		private void RenderCrumb(string displayName, Strings.IDs? untitledName, string uri, UriFlags libraryType, bool refreshViewer)
		{
			if (untitledName != null && string.IsNullOrEmpty(displayName))
			{
				displayName = LocalizedStrings.GetNonEncoded(untitledName.Value);
			}
			base.Writer.Write("<span dir=\"ltr\" ");
			Utilities.RenderScriptHandler(base.Writer, "oncontextmenu", "shwBrCrMnu(_this);");
			base.Writer.Write(" ");
			Utilities.RenderScriptHandler(base.Writer, "onclick", "opnDocFldr(_this.uri," + (refreshViewer ? "1" : "0") + ");");
			base.Writer.Write(" uri=\"");
			Utilities.HtmlEncode(uri, base.Writer);
			base.Writer.Write("\" uf=");
			base.Writer.Write((int)libraryType);
			base.Writer.Write("\">");
			Utilities.HtmlEncode(displayName, base.Writer);
			base.Writer.Write("</span>");
		}

		// Token: 0x0400175E RID: 5982
		private const string ToolbarId = "divTBL";

		// Token: 0x0400175F RID: 5983
		private const string BeginBreadcrumbBar = "<td class=\"w100 lnkbr\"><img align=absmiddle height=16 width=0>&nbsp;";

		// Token: 0x04001760 RID: 5984
		private const string EndBreadcrumbBar = "</td>";

		// Token: 0x04001761 RID: 5985
		private const string SharepointDivider = " / ";

		// Token: 0x04001762 RID: 5986
		private const string UncDivider = " \\ ";

		// Token: 0x04001763 RID: 5987
		private SharepointSession sharepointSession;

		// Token: 0x04001764 RID: 5988
		private UncSession uncSession;

		// Token: 0x04001765 RID: 5989
		private IDocumentLibrary documentLibrary;

		// Token: 0x04001766 RID: 5990
		private IDocumentLibraryFolder libraryFolder;

		// Token: 0x04001767 RID: 5991
		private UriFlags libraryType = UriFlags.DocumentLibrary;

		// Token: 0x04001768 RID: 5992
		private bool isRootFolder;

		// Token: 0x04001769 RID: 5993
		private DocumentBreadcrumbBarContextMenu contextMenu;
	}
}
