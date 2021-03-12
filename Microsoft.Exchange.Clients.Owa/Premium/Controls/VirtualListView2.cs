using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000312 RID: 786
	internal abstract class VirtualListView2 : IListView
	{
		// Token: 0x06001DA3 RID: 7587 RVA: 0x000AC471 File Offset: 0x000AA671
		public VirtualListView2(UserContext userContext, string id, bool isMultiLine, ColumnId sortedColumn, SortOrder sortOrder) : this(userContext, id, isMultiLine, sortedColumn, sortOrder, false)
		{
			this.userContext = userContext;
			this.id = id;
			this.sortedColumn = sortedColumn;
			this.sortOrder = sortOrder;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000AC4A0 File Offset: 0x000AA6A0
		public VirtualListView2(UserContext userContext, string id, bool isMultiLine, ColumnId sortedColumn, SortOrder sortOrder, bool isFiltered)
		{
			this.userContext = userContext;
			this.id = id;
			this.sortedColumn = sortedColumn;
			this.sortOrder = sortOrder;
			this.isFiltered = isFiltered;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x000AC4FB File Offset: 0x000AA6FB
		protected virtual bool IsInSearch
		{
			get
			{
				return this.IsFiltered;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001DA6 RID: 7590
		protected abstract Folder DataFolder { get; }

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001DA7 RID: 7591
		public abstract ViewType ViewType { get; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001DA8 RID: 7592
		public abstract string OehNamespace { get; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x000AC503 File Offset: 0x000AA703
		public int TotalCount
		{
			get
			{
				return this.Contents.DataSource.TotalCount;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x000AC515 File Offset: 0x000AA715
		public IListViewDataSource DataSource
		{
			get
			{
				return this.contents.DataSource;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x000AC522 File Offset: 0x000AA722
		protected virtual bool IsMultiLine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x000AC525 File Offset: 0x000AA725
		protected ColumnId SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x000AC52D File Offset: 0x000AA72D
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x000AC535 File Offset: 0x000AA735
		protected bool IsFiltered
		{
			get
			{
				return this.isFiltered;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x000AC53D File Offset: 0x000AA73D
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x000AC545 File Offset: 0x000AA745
		protected ListViewContents2 Contents
		{
			get
			{
				return this.contents;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x000AC54D File Offset: 0x000AA74D
		protected Strings.IDs EmptyViewStringId
		{
			get
			{
				if (this.IsSearchInProgress())
				{
					return -1057914178;
				}
				if (this.IsFiltered)
				{
					return 417836457;
				}
				return -474826895;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x000AC570 File Offset: 0x000AA770
		protected virtual bool ShouldSubscribeForFolderContentChanges
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x000AC574 File Offset: 0x000AA774
		private bool IsSearchInProgress()
		{
			bool result = false;
			FolderListViewDataSource folderListViewDataSource = this.contents.DataSource as FolderListViewDataSource;
			if (folderListViewDataSource != null && folderListViewDataSource.IsSearchInProgress())
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000AC5A4 File Offset: 0x000AA7A4
		public virtual bool GetDidLastSearchFail()
		{
			if (!this.IsInSearch)
			{
				return false;
			}
			if (this.didLastSearchFail != null)
			{
				return this.didLastSearchFail.Value;
			}
			SearchFolder searchFolder = this.DataFolder as SearchFolder;
			if (searchFolder != null && this.UserContext.IsPushNotificationsEnabled)
			{
				bool flag2;
				bool flag = this.UserContext.MapiNotificationManager.HasCurrentSearchCompleted((MailboxSession)searchFolder.Session, searchFolder.StoreObjectId, out flag2);
				if (flag && flag2)
				{
					this.didLastSearchFail = new bool?((searchFolder.GetSearchCriteria().SearchState & SearchState.Failed) != SearchState.None);
				}
				if (this.didLastSearchFail != null && this.didLastSearchFail == true)
				{
					SearchPerformanceData searchPerformanceData = this.UserContext.MapiNotificationManager.GetSearchPerformanceData((MailboxSession)searchFolder.Session);
					searchPerformanceData.SearchFailed();
				}
			}
			return this.didLastSearchFail != null && this.didLastSearchFail.Value;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000AC6A8 File Offset: 0x000AA8A8
		public void Render(TextWriter writer)
		{
			this.OnBeforeRender();
			writer.Write("<div");
			VirtualListView2.RenderAttribute(writer, "id", this.id);
			VirtualListView2.RenderAttribute(writer, "class", "absFill");
			VirtualListView2.RenderAttribute(writer, "iT", (int)this.ViewType);
			VirtualListView2.RenderAttribute(writer, "sEvtNS", this.OehNamespace);
			VirtualListView2.RenderAttribute(writer, "sSid", this.contents.DataSource.ContainerId);
			VirtualListView2.RenderAttribute(writer, "fML", this.IsMultiLine ? 1 : 0);
			VirtualListView2.RenderAttribute(writer, "iSC", (int)this.sortedColumn);
			VirtualListView2.RenderAttribute(writer, "iSO", (int)this.sortOrder);
			VirtualListView2.RenderAttribute(writer, "iTC", this.contents.DataSource.TotalCount);
			VirtualListView2.RenderAttribute(writer, "iTIC", this.contents.DataSource.TotalItemCount);
			VirtualListView2.RenderAttribute(writer, "iNsDir", (int)this.userContext.UserOptions.NextSelection);
			VirtualListView2.RenderAttribute(writer, "sPbPrps", this.publicProperties.ToString());
			VirtualListView2.RenderAttribute(writer, "fTD", ListViewColumns.GetColumn(this.sortedColumn).IsTypeDownCapable ? 1 : 0);
			VirtualListView2.RenderAttribute(writer, "L_BigSel", LocalizedStrings.GetNonEncoded(719114324));
			VirtualListView2.RenderAttribute(writer, "L_Ldng", LocalizedStrings.GetNonEncoded(-695375226));
			VirtualListView2.RenderAttribute(writer, "L_Srchng", LocalizedStrings.GetNonEncoded(-1057914178));
			VirtualListView2.RenderAttribute(writer, "L_Fltrng", LocalizedStrings.GetNonEncoded(320310349));
			foreach (string text in this.extraAttributes.Keys)
			{
				VirtualListView2.RenderAttribute(writer, text, this.extraAttributes[text]);
			}
			writer.Write(">");
			this.RenderHeaders(writer);
			writer.Write("<a href=\"#\" id=\"linkVLV\" class=\"offscreen\">&nbsp;</a>");
			writer.Write("<div id=\"divHeaderSpacer\">&nbsp;</div>");
			if (this.HasInlineControl)
			{
				this.RenderInlineControl(writer);
			}
			writer.Write("<div id=\"divList\"");
			this.RenderListViewClasses(writer);
			writer.Write(">");
			writer.Write("<div id=\"divViewport\" draggable=\"true\">");
			this.RenderChunk(writer);
			writer.Write("</div>");
			writer.Write("<div id=\"divScrollbar\"><div id=\"divScrollRegion\"></div></div>");
			writer.Write("</div>");
			writer.Write("<div id=\"divPrgBg\" style=\"display:none\"></div><div id=\"divPrgrs\" style=\"display:none\"><img src=\"");
			this.userContext.RenderThemeFileUrl(writer, ThemeFileId.ProgressSmall);
			writer.Write("\"><span id=\"spnTxt\"></span></div>");
			writer.Write("</div>");
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x000AC94C File Offset: 0x000AAB4C
		public void RenderForCompactWebPart(TextWriter writer)
		{
			this.AddAttribute("iWP", "1");
			this.Render(writer);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000AC968 File Offset: 0x000AAB68
		public virtual void LoadData(int startRange, int rowCount)
		{
			this.contents = this.CreateListViewContents();
			if (this.GetDidLastSearchFail())
			{
				this.contents.DataSource = new FolderListViewEmptyDataSource(this.DataFolder, this.contents.Properties);
			}
			else
			{
				this.contents.DataSource = this.CreateDataSource(this.contents.Properties);
			}
			this.contents.DataSource.Load(startRange, rowCount);
			this.SubscribeForFolderContentChanges();
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000AC9E0 File Offset: 0x000AABE0
		public virtual void LoadData(ObjectId seekRowId, SeekDirection seekDirection, int rowCount)
		{
			this.contents = this.CreateListViewContents();
			this.contents.DataSource = this.CreateDataSource(this.contents.Properties);
			this.contents.DataSource.Load(seekRowId, seekDirection, rowCount);
			this.SubscribeForFolderContentChanges();
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000ACA30 File Offset: 0x000AAC30
		public virtual void LoadData(string seekValue, int rowCount)
		{
			this.contents = this.CreateListViewContents();
			this.contents.DataSource = this.CreateDataSource(this.contents.Properties);
			this.contents.DataSource.Load(seekValue, rowCount);
			this.SubscribeForFolderContentChanges();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000ACA80 File Offset: 0x000AAC80
		public virtual void LoadAdjacent(ObjectId adjacentId, SeekDirection seekDirection, int rowCount)
		{
			this.contents = this.CreateListViewContents();
			this.contents.DataSource = this.CreateDataSource(this.contents.Properties);
			if (!this.contents.DataSource.LoadAdjacent(adjacentId, seekDirection, rowCount))
			{
				this.forceRefresh = true;
			}
			this.SubscribeForFolderContentChanges();
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000ACAD8 File Offset: 0x000AACD8
		public void RenderChunk(TextWriter writer)
		{
			if (this.contents == null)
			{
				throw new OwaInvalidOperationException("LoadData must be called before calling RenderChunk");
			}
			if (this.GetDidLastSearchFail())
			{
				this.InternalRenderChunk(writer, true, "fNoRws", new Strings.IDs?(1073923836));
			}
			if (!this.contents.DataSource.UserHasRightToLoad)
			{
				this.InternalRenderChunk(writer, true, "fNoRws", new Strings.IDs?(-593658721));
				return;
			}
			if (this.contents.DataSource.TotalCount == 0)
			{
				this.InternalRenderChunk(writer, true, "fNoRws", new Strings.IDs?(this.EmptyViewStringId));
				return;
			}
			if (this.contents.DataSource.RangeCount == 0)
			{
				this.InternalRenderChunk(writer, true, "fNull", null);
				return;
			}
			this.InternalRenderChunk(writer, false, null, null);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000ACBA7 File Offset: 0x000AADA7
		public void RenderData(TextWriter writer)
		{
			writer.Write("<div id=\"data\"");
			this.InternalRenderData(writer);
			writer.Write("></div>");
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x000ACBC8 File Offset: 0x000AADC8
		public void RenderHeaders(TextWriter writer)
		{
			ListViewHeaders2 listViewHeaders = new ArrangeByHeaders2(this.SortedColumn, this.SortOrder, this.userContext);
			writer.Write("<div id=\"divHeaders\">");
			listViewHeaders.Render(writer);
			writer.Write("</div>");
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x000ACC0A File Offset: 0x000AAE0A
		public virtual void RenderListViewClasses(TextWriter writer)
		{
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x000ACC0C File Offset: 0x000AAE0C
		public void AddAttribute(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name is null or empty.");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.extraAttributes.Add(name, value);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000ACC3C File Offset: 0x000AAE3C
		protected static void RenderAttribute(TextWriter writer, string name, string value)
		{
			writer.Write(" ");
			writer.Write(name);
			writer.Write("=\"");
			Utilities.HtmlEncode(value, writer);
			writer.Write("\"");
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000ACC6D File Offset: 0x000AAE6D
		protected static void RenderAttribute(TextWriter writer, string name, int value)
		{
			writer.Write(" ");
			writer.Write(name);
			writer.Write("=");
			writer.Write(value);
		}

		// Token: 0x06001DC2 RID: 7618
		protected abstract ListViewContents2 CreateListViewContents();

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000ACC94 File Offset: 0x000AAE94
		protected ListViewContents2 CreateGroupByList(ListViewContents2 originalContents)
		{
			Column column = ListViewColumns.GetColumn(this.SortedColumn);
			if (column.GroupType == GroupType.Expanded)
			{
				return new GroupByList2(this.sortedColumn, this.sortOrder, (ItemList2)originalContents, this.userContext);
			}
			return originalContents;
		}

		// Token: 0x06001DC4 RID: 7620
		protected abstract IListViewDataSource CreateDataSource(Hashtable properties);

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000ACCD5 File Offset: 0x000AAED5
		protected virtual void OnBeforeRender()
		{
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000ACCD7 File Offset: 0x000AAED7
		protected void MakePropertyPublic(string attribute)
		{
			if (this.publicProperties.Length > 0)
			{
				this.publicProperties.Append(";");
			}
			this.publicProperties.Append(attribute);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000ACD08 File Offset: 0x000AAF08
		protected virtual void InternalRenderData(TextWriter writer)
		{
			VirtualListView2.RenderAttribute(writer, "iTC", this.contents.DataSource.TotalCount);
			VirtualListView2.RenderAttribute(writer, "iTIC", this.contents.DataSource.TotalItemCount);
			VirtualListView2.RenderAttribute(writer, "sDataFId", this.contents.DataSource.ContainerId);
			if (this.IsSearchInProgress())
			{
				VirtualListView2.RenderAttribute(writer, "fSrchInProg", 1);
			}
			if (this.forceRefresh)
			{
				VirtualListView2.RenderAttribute(writer, "fR", 1);
			}
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000ACD8E File Offset: 0x000AAF8E
		protected virtual void RenderInlineControl(TextWriter writer)
		{
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x000ACD90 File Offset: 0x000AAF90
		protected virtual bool HasInlineControl
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000ACD94 File Offset: 0x000AAF94
		private void InternalRenderChunk(TextWriter writer, bool error, string errorFlag, Strings.IDs? errorString)
		{
			writer.Write("<div id=\"divChnk\" class=\"vlvChnk\"");
			if (!error)
			{
				VirtualListView2.RenderAttribute(writer, "iSR", this.contents.DataSource.StartRange);
				VirtualListView2.RenderAttribute(writer, "iER", this.contents.DataSource.EndRange);
			}
			else if (!string.IsNullOrEmpty(errorFlag))
			{
				VirtualListView2.RenderAttribute(writer, errorFlag, 1);
			}
			writer.Write(">");
			if (!error)
			{
				this.contents.RenderForVirtualListView(writer);
			}
			else if (errorString != null)
			{
				writer.Write("<div id=\"divNI\">");
				writer.Write(LocalizedStrings.GetHtmlEncoded(errorString.Value));
				writer.Write("</div>");
			}
			writer.Write("</div>");
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000ACE4F File Offset: 0x000AB04F
		protected virtual void SubscribeForFolderContentChanges()
		{
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000ACE51 File Offset: 0x000AB051
		internal virtual void UnSubscribeForFolderContentChanges()
		{
		}

		// Token: 0x0400162C RID: 5676
		public const int DefaultRowsLoaded = 50;

		// Token: 0x0400162D RID: 5677
		public const string ContainerId = "divVLV";

		// Token: 0x0400162E RID: 5678
		private UserContext userContext;

		// Token: 0x0400162F RID: 5679
		private string id;

		// Token: 0x04001630 RID: 5680
		private ColumnId sortedColumn;

		// Token: 0x04001631 RID: 5681
		private SortOrder sortOrder;

		// Token: 0x04001632 RID: 5682
		private ListViewContents2 contents;

		// Token: 0x04001633 RID: 5683
		private Dictionary<string, string> extraAttributes = new Dictionary<string, string>();

		// Token: 0x04001634 RID: 5684
		private StringBuilder publicProperties = new StringBuilder();

		// Token: 0x04001635 RID: 5685
		private bool forceRefresh;

		// Token: 0x04001636 RID: 5686
		private bool isFiltered;

		// Token: 0x04001637 RID: 5687
		private bool? didLastSearchFail = null;
	}
}
