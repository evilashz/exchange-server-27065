using System;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004BF RID: 1215
	internal abstract class ListViewEventHandler : OwaEventHandlerBase
	{
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002E56 RID: 11862
		protected abstract ListViewState ListViewState { get; }

		// Token: 0x06002E57 RID: 11863 RVA: 0x00108DDA File Offset: 0x00106FDA
		protected virtual void PreRefresh()
		{
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x00108DDC File Offset: 0x00106FDC
		protected virtual void EndProcessEvent()
		{
		}

		// Token: 0x06002E59 RID: 11865
		protected abstract ListView GetListView();

		// Token: 0x06002E5A RID: 11866
		protected abstract IListViewDataSource GetDataSource(ListView listView);

		// Token: 0x06002E5B RID: 11867 RVA: 0x00108DDE File Offset: 0x00106FDE
		protected virtual void PersistFilter()
		{
		}

		// Token: 0x06002E5C RID: 11868
		protected abstract IListViewDataSource GetDataSource(ListView listView, bool newSearch);

		// Token: 0x06002E5D RID: 11869 RVA: 0x00108DE0 File Offset: 0x00106FE0
		public static void Register()
		{
			OwaEventRegistry.RegisterEnum(typeof(ColumnId));
			OwaEventRegistry.RegisterEnum(typeof(SortOrder));
			OwaEventRegistry.RegisterEnum(typeof(ReadingPanePosition));
			OwaEventRegistry.RegisterEnum(typeof(SearchScope));
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x00108E20 File Offset: 0x00107020
		public virtual void Refresh()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewEventHandler.InternalRefresh");
				this.Refresh(ListView.RenderFlags.Contents, this.ListViewState.StartRange - 1);
				if (base.IsParameterSet("fltr"))
				{
					this.PersistFilter();
				}
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.MailViewRefreshes.Increment();
				}
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x00108E98 File Offset: 0x00107098
		public virtual void TypeDown()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewEventHandler.TypeDown");
				this.PreRefresh();
				ListView listView = this.GetListView();
				Column column = ListViewColumns.GetColumn(this.ListViewState.SortedColumnId);
				if (!column.IsTypeDownCapable)
				{
					throw new OwaInvalidOperationException("Type down search is not supported");
				}
				IListViewDataSource dataSource = this.GetDataSource(listView);
				dataSource.Load((string)base.GetParameter("td"), base.UserContext.UserOptions.ViewRowCount);
				listView.DataSource = dataSource;
				this.WriteResponse(ListView.RenderFlags.Contents, listView);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x00108F44 File Offset: 0x00107144
		public virtual void ToggleMultiVsSingleLine()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewEventHandler.ToggleMultiVsSingleLine");
				this.PersistMultiLineState(this.ListViewState.IsMultiLine);
				this.Refresh(ListView.RenderFlags.Contents | ListView.RenderFlags.Headers, this.ListViewState.StartRange - 1);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x00108FA8 File Offset: 0x001071A8
		protected virtual void PersistMultiLineState(bool isMultiLine)
		{
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x00108FAC File Offset: 0x001071AC
		public virtual void FirstPage()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewEventHandler.FirstPage");
				this.Refresh(ListView.RenderFlags.Contents, 0);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x00108FF0 File Offset: 0x001071F0
		public virtual void PreviousPage()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewEventHandler.PreviousPage");
				int startRange = this.ListViewState.StartRange - base.UserContext.UserOptions.ViewRowCount - 1;
				this.Refresh(ListView.RenderFlags.Contents, startRange);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x00109054 File Offset: 0x00107254
		public virtual void NextPage()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewEventHandler.NextPage");
				this.Refresh(ListView.RenderFlags.Contents, this.ListViewState.EndRange);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x001090A4 File Offset: 0x001072A4
		public virtual void LastPage()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewEventHandler.LastPage");
				int startRange = this.ListViewState.TotalCount - base.UserContext.UserOptions.ViewRowCount;
				this.Refresh(ListView.RenderFlags.Contents, startRange);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x00109108 File Offset: 0x00107308
		public virtual void Sort()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewEventHandler.Sort");
				bool flag = this.ShouldSeekToItemOnSort(this.ListViewState.SortedColumnId);
				this.PersistSort(this.ListViewState.SortedColumnId, this.ListViewState.SortDirection);
				ObjectId objectId = base.GetParameter("mId") as ObjectId;
				if (objectId != null && flag)
				{
					this.PreRefresh();
					ListView listView = this.GetListView();
					IListViewDataSource dataSource = this.GetDataSource(listView);
					dataSource.Load(objectId, SeekDirection.Next, base.UserContext.UserOptions.ViewRowCount);
					listView.DataSource = dataSource;
					this.WriteResponse(ListView.RenderFlags.Contents | ListView.RenderFlags.Headers, listView);
				}
				else
				{
					this.Refresh(ListView.RenderFlags.Contents | ListView.RenderFlags.Headers, 0);
				}
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x001091D0 File Offset: 0x001073D0
		public virtual void Find()
		{
			try
			{
				string text = (string)base.GetParameter("srch");
				if (255 < text.Length)
				{
					throw new OwaInvalidOperationException("Search string excedes the maximum supported length");
				}
				ListView listView = this.GetListView();
				IListViewDataSource dataSource = this.GetDataSource(listView, true);
				dataSource.Load(0, base.UserContext.UserOptions.ViewRowCount);
				listView.DataSource = dataSource;
				this.WriteResponse(ListView.RenderFlags.Contents, listView);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x00109258 File Offset: 0x00107458
		protected virtual void PersistSort(ColumnId sortedColumn, SortOrder sortOrder)
		{
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x0010925A File Offset: 0x0010745A
		protected virtual bool ShouldSeekToItemOnSort(ColumnId newSortColumn)
		{
			return true;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x0010925D File Offset: 0x0010745D
		private void Refresh(ListView.RenderFlags renderFlags, int startRange)
		{
			this.Refresh(renderFlags, startRange, SeekDirection.Next);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x00109268 File Offset: 0x00107468
		private void Refresh(ListView.RenderFlags renderFlags, int startRange, SeekDirection direction)
		{
			this.PreRefresh();
			if (startRange < 0)
			{
				startRange = 0;
			}
			ListView listView = this.GetListView();
			IListViewDataSource dataSource = this.GetDataSource(listView);
			this.LoadDataSource(dataSource, startRange, base.UserContext.UserOptions.ViewRowCount, direction);
			listView.DataSource = dataSource;
			this.WriteResponse(renderFlags, listView);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x001092B9 File Offset: 0x001074B9
		protected virtual void LoadDataSource(IListViewDataSource listViewDataSource, int startRange, int itemCount, SeekDirection direction)
		{
			listViewDataSource.Load(startRange, itemCount);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x001092C4 File Offset: 0x001074C4
		protected void WriteResponse(ListView.RenderFlags renderFlags, ListView listView)
		{
			if (listView.IsFilteredView)
			{
				this.Writer.Write("<span id=spnSR>");
				this.RenderSearchResultsHeader(listView.DataSource);
				this.Writer.Write("</span>");
			}
			this.Writer.Write("<div id=\"data\" sR=\"");
			this.Writer.Write((0 < listView.DataSource.TotalCount) ? (listView.StartRange + 1) : 0);
			this.Writer.Write("\" eR=\"");
			this.Writer.Write((0 < listView.DataSource.TotalCount) ? (listView.EndRange + 1) : 0);
			this.Writer.Write("\" tC=\"");
			this.Writer.Write(listView.DataSource.TotalCount);
			this.Writer.Write("\" sCki=\"");
			this.Writer.Write(listView.Cookie);
			this.Writer.Write("\" iLcid=\"");
			this.Writer.Write(listView.CookieLcid);
			this.Writer.Write("\" sPfdDC=\"");
			this.Writer.Write(Utilities.HtmlEncode(listView.PreferredDC));
			this.Writer.Write("\" uC=\"");
			this.Writer.Write(listView.DataSource.UnreadCount);
			this.Writer.Write("\"></div>");
			listView.Render(this.Writer, renderFlags);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x0010943E File Offset: 0x0010763E
		protected virtual void RenderSearchResultsHeader(IListViewDataSource dataSource)
		{
			this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-774873536), dataSource.TotalCount);
		}

		// Token: 0x04002008 RID: 8200
		public const string MethodRefresh = "Refresh";

		// Token: 0x04002009 RID: 8201
		public const string MethodTypeDown = "TypeDown";

		// Token: 0x0400200A RID: 8202
		public const string MethodToggleMultiVsSingleLine = "ToggleMultiVsSingleLine";

		// Token: 0x0400200B RID: 8203
		public const string MethodFirstPage = "FirstPage";

		// Token: 0x0400200C RID: 8204
		public const string MethodPreviousPage = "PreviousPage";

		// Token: 0x0400200D RID: 8205
		public const string MethodNextPage = "NextPage";

		// Token: 0x0400200E RID: 8206
		public const string MethodLastPage = "LastPage";

		// Token: 0x0400200F RID: 8207
		public const string MethodSort = "Sort";

		// Token: 0x04002010 RID: 8208
		public const string MethodNewMessageToPeople = "MsgToPeople";

		// Token: 0x04002011 RID: 8209
		public const string MethodMove = "Move";

		// Token: 0x04002012 RID: 8210
		public const string MethodCopy = "Copy";

		// Token: 0x04002013 RID: 8211
		public const string MethodPersistSearchScope = "PSS";

		// Token: 0x04002014 RID: 8212
		public const string MethodSeekItem = "SeekItem";

		// Token: 0x04002015 RID: 8213
		public const string MItemId = "mId";

		// Token: 0x04002016 RID: 8214
		public const string Id = "id";

		// Token: 0x04002017 RID: 8215
		public const string Conversations = "cnvs";

		// Token: 0x04002018 RID: 8216
		public const string Width = "w";

		// Token: 0x04002019 RID: 8217
		public const string Height = "h";

		// Token: 0x0400201A RID: 8218
		public const string ReadingPanePosition = "s";

		// Token: 0x0400201B RID: 8219
		public const string TypeDownSearch = "td";

		// Token: 0x0400201C RID: 8220
		public const string SearchString = "srch";

		// Token: 0x0400201D RID: 8221
		public const string DestinationFolderId = "destId";

		// Token: 0x0400201E RID: 8222
		public const string FilterString = "fltr";

		// Token: 0x0400201F RID: 8223
		public const string ExpandedConversations = "cnvs";

		// Token: 0x04002020 RID: 8224
		public const string VisibleReadItems = "vR";

		// Token: 0x04002021 RID: 8225
		public const string State = "st";

		// Token: 0x04002022 RID: 8226
		public const string PaaPicker = "paa";

		// Token: 0x04002023 RID: 8227
		public const string MobilePicker = "mbl";

		// Token: 0x04002024 RID: 8228
		private static readonly PropertyDefinition[] addressProperties = new PropertyDefinition[]
		{
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email1AddrType,
			ContactSchema.Email1DisplayName,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email2AddrType,
			ContactSchema.Email2DisplayName,
			ContactSchema.Email3EmailAddress,
			ContactSchema.Email3AddrType,
			ContactSchema.Email3DisplayName
		};

		// Token: 0x020004C0 RID: 1216
		private enum EmailAddressProperty
		{
			// Token: 0x04002026 RID: 8230
			EmailAddress,
			// Token: 0x04002027 RID: 8231
			AddressType,
			// Token: 0x04002028 RID: 8232
			DisplayName,
			// Token: 0x04002029 RID: 8233
			Count
		}
	}
}
