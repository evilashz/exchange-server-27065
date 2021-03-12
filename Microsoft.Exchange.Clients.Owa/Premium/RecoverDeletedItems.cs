using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000475 RID: 1141
	public class RecoverDeletedItems : ListViewSubPage, IRegistryOnlyForm
	{
		// Token: 0x06002B93 RID: 11155 RVA: 0x000F4188 File Offset: 0x000F2388
		public RecoverDeletedItems() : base(ExTraceGlobals.MailCallTracer, ExTraceGlobals.MailTracer)
		{
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000F41E1 File Offset: 0x000F23E1
		protected DumpsterViewArrangeByMenu ArrangeByMenu
		{
			get
			{
				return this.arrangeByMenu;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000F41E9 File Offset: 0x000F23E9
		protected override int ViewWidth
		{
			get
			{
				return this.viewWidth;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x000F41F1 File Offset: 0x000F23F1
		protected override int ViewHeight
		{
			get
			{
				return this.viewHeight;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x000F41F9 File Offset: 0x000F23F9
		protected DumpsterContextMenu ContextMenu
		{
			get
			{
				if (this.contextMenu == null)
				{
					this.contextMenu = new DumpsterContextMenu(base.UserContext);
				}
				return this.contextMenu;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000F421A File Offset: 0x000F241A
		protected override SortOrder DefaultSortOrder
		{
			get
			{
				return SortOrder.Descending;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x000F421D File Offset: 0x000F241D
		protected override SortOrder SortOrder
		{
			get
			{
				return this.DefaultSortOrder;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x000F4225 File Offset: 0x000F2425
		protected override ColumnId DefaultSortedColumn
		{
			get
			{
				return ColumnId.DeletedOnTime;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000F4229 File Offset: 0x000F2429
		protected override ColumnId SortedColumn
		{
			get
			{
				return this.DefaultSortedColumn;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x000F4231 File Offset: 0x000F2431
		protected override ReadingPanePosition DefaultReadingPanePosition
		{
			get
			{
				return ReadingPanePosition.Min;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x000F4234 File Offset: 0x000F2434
		protected override ReadingPanePosition ReadingPanePosition
		{
			get
			{
				return ReadingPanePosition.Min;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x000F4237 File Offset: 0x000F2437
		protected override bool DefaultMultiLineSetting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x000F423A File Offset: 0x000F243A
		protected override bool IsMultiLine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000F423D File Offset: 0x000F243D
		protected override bool FindBarOn
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x000F4240 File Offset: 0x000F2440
		protected override bool AllowAdvancedSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x000F4243 File Offset: 0x000F2443
		protected override bool ShouldRenderToolbar
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000F4246 File Offset: 0x000F2446
		protected override bool RenderSearchDropDown
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000F424C File Offset: 0x000F244C
		protected override void LoadViewState()
		{
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(Utilities.GetQueryStringParameter(base.Request, "id"));
			MailboxSession mailboxSession = (MailboxSession)owaStoreObjectId.GetSession(base.UserContext);
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions);
			if (defaultFolderId == null)
			{
				DumpsterFolderHelper.CheckAndCreateFolder(mailboxSession);
				defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions);
			}
			OwaStoreObjectId folderId = OwaStoreObjectId.CreateFromStoreObjectId(defaultFolderId, owaStoreObjectId);
			this.folder = Utilities.GetFolderForContent<Folder>(base.UserContext, folderId, RecoverDeletedItems.folderProperties);
			this.viewWidth = Utilities.GetFolderProperty<int>(this.folder, ViewStateProperties.ViewWidth, 450);
			this.viewHeight = Utilities.GetFolderProperty<int>(this.folder, ViewStateProperties.ViewHeight, 250);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000F42F4 File Offset: 0x000F24F4
		protected override IListView CreateListView(ColumnId sortedColumn, SortOrder sortOrder)
		{
			DumpsterVirtualListView dumpsterVirtualListView = new DumpsterVirtualListView(base.UserContext, "divVLV", sortedColumn, sortOrder, this.folder);
			dumpsterVirtualListView.LoadData(0, 50);
			return dumpsterVirtualListView;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000F4324 File Offset: 0x000F2524
		protected override IListViewDataSource CreateDataSource(ListView listView)
		{
			SortBy[] sortByProperties = listView.GetSortByProperties();
			return new FolderListViewDataSource(base.UserContext, listView.Properties, this.folder, sortByProperties);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000F4350 File Offset: 0x000F2550
		protected void RenderFolderCount()
		{
			string text = string.Format(CultureInfo.InvariantCulture, "<span id=spnIC>{0}</span>", new object[]
			{
				this.folder.ItemCount
			});
			text = string.Format(LocalizedStrings.GetHtmlEncoded(1648193418), text);
			base.Writer.Write("<span class=spnFST>");
			base.Writer.Write("<span id=spnDIC>");
			base.Writer.Write(text);
			base.Writer.Write("</span>");
			base.Writer.Write("</span>");
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000F43E8 File Offset: 0x000F25E8
		protected void RenderDumpsterListView()
		{
			base.Writer.Write("\t\t<div id=divLVContainer class=dmpLvContainer style=\"width:100%\">\r\n");
			base.Writer.Write("\t\t\t<div id=\"divToolbarStrip\" class=\"dmpTs\">");
			Toolbar toolbar = this.CreateListToolbar();
			toolbar.Render(base.Writer);
			base.Writer.Write("</div>");
			base.RenderSearch(this.folder);
			base.Writer.Write("\r\n\t\t\t<div id=divLV class=dmpLvPos style=\"top:");
			base.Writer.Write(this.ListViewTop);
			base.Writer.Write("px;\">");
			base.RenderListView();
			base.Writer.Write("</div>\r\n");
			base.Writer.Write("\t\t</div>\r\n");
			base.Writer.Write("<div class=\"dmpIC\">");
			this.RenderFolderCount();
			base.Writer.Write("</div>\r\n");
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000F44C1 File Offset: 0x000F26C1
		protected void RenderJavascriptEncodedFolderId()
		{
			Utilities.JavascriptEncode(Utilities.GetIdAsString(this.folder), base.Response.Output);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000F44DE File Offset: 0x000F26DE
		protected override Toolbar CreateListToolbar()
		{
			return new DumpsterViewListToolbar();
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000F44E5 File Offset: 0x000F26E5
		protected override Toolbar CreateActionToolbar()
		{
			return null;
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000F44E8 File Offset: 0x000F26E8
		protected override void OnUnload(EventArgs e)
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000F4504 File Offset: 0x000F2704
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.arrangeByMenu = new DumpsterViewArrangeByMenu(this.folder);
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x000F451E File Offset: 0x000F271E
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000F4526 File Offset: 0x000F2726
		public override SanitizedHtmlString Title
		{
			get
			{
				return SanitizedHtmlString.FromStringId(369288321);
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000F4532 File Offset: 0x000F2732
		public override string PageType
		{
			get
			{
				return "DumpsterViewPage";
			}
		}

		// Token: 0x04001CF6 RID: 7414
		private static readonly PropertyDefinition[] folderProperties = new PropertyDefinition[]
		{
			FolderSchema.DisplayName,
			FolderSchema.ItemCount,
			FolderSchema.UnreadCount,
			ViewStateProperties.ViewWidth,
			ViewStateProperties.ViewHeight,
			ViewStateProperties.SortColumn,
			ViewStateProperties.SortOrder
		};

		// Token: 0x04001CF7 RID: 7415
		private int viewWidth = 450;

		// Token: 0x04001CF8 RID: 7416
		private int viewHeight = 250;

		// Token: 0x04001CF9 RID: 7417
		private Folder folder;

		// Token: 0x04001CFA RID: 7418
		private DumpsterViewArrangeByMenu arrangeByMenu;

		// Token: 0x04001CFB RID: 7419
		private DumpsterContextMenu contextMenu;

		// Token: 0x04001CFC RID: 7420
		private string[] externalScriptFiles = new string[]
		{
			"uview.js",
			"dumpstervw.js",
			"vlv.js"
		};
	}
}
