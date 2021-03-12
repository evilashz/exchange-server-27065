using System;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000486 RID: 1158
	internal abstract class VirtualListViewEventHandler2 : OwaEventHandlerBase
	{
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002C97 RID: 11415
		protected abstract VirtualListViewState ListViewState { get; }

		// Token: 0x06002C98 RID: 11416
		public abstract void LoadFresh();

		// Token: 0x06002C99 RID: 11417
		public abstract void LoadNext();

		// Token: 0x06002C9A RID: 11418
		public abstract void LoadPrevious();

		// Token: 0x06002C9B RID: 11419
		public abstract void SeekNext();

		// Token: 0x06002C9C RID: 11420
		public abstract void SeekPrevious();

		// Token: 0x06002C9D RID: 11421
		public abstract void Sort();

		// Token: 0x06002C9E RID: 11422
		public abstract void SetMultiLineState();

		// Token: 0x06002C9F RID: 11423
		public abstract void TypeDownSearch();

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000FA0EA File Offset: 0x000F82EA
		protected void InternalLoadFresh()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "VirtualListViewEventHandler.InternalLoadFresh");
			this.InternalLoadFresh(false);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000FA109 File Offset: 0x000F8309
		protected void InternalLoadNext()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "VirtualListViewEventHandler.InternalLoadNext");
			this.LoadNextOrPrevious(SeekDirection.Next);
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000FA128 File Offset: 0x000F8328
		protected void InternalLoadPrevious()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "VirtualListViewEventHandler.InternalLoadPrevious");
			this.LoadNextOrPrevious(SeekDirection.Previous);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000FA147 File Offset: 0x000F8347
		protected void InternalSeekNext()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "VirtualListViewEventHandler.InternalSeekNext");
			this.SeekNextOrPrevious(SeekDirection.Next);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000FA166 File Offset: 0x000F8366
		protected void InternalSeekPrevious()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "VirtualListViewEventHandler.InternalSeekPrevious");
			this.SeekNextOrPrevious(SeekDirection.Previous);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000FA188 File Offset: 0x000F8388
		protected void InternalSort()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "VirtualListViewEventHandler.InternalSort");
				int rowCount = this.GetRowCount();
				this.PersistSort();
				VirtualListView2 listView = this.GetListView();
				listView.UnSubscribeForFolderContentChanges();
				if (base.IsParameterSet("SId"))
				{
					OwaStoreObjectId seekId = this.GetSeekId();
					listView.LoadData(seekId, SeekDirection.Next, rowCount);
				}
				else
				{
					listView.LoadData(0, rowCount);
				}
				listView.RenderData(this.Writer);
				listView.RenderChunk(this.Writer);
				this.RenderExtraData(listView);
				listView.RenderHeaders(this.Writer);
				this.RenderNewSelection(listView);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000FA238 File Offset: 0x000F8438
		protected void InternalSetMultiLineState()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "VirtualListViewEventHandler.InternalSetMultiLineState");
			this.PersistMultiLineState();
			this.InternalLoadFresh(true);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000FA260 File Offset: 0x000F8460
		protected void InternalTypeDownSearch()
		{
			try
			{
				int rowCount = this.GetRowCount();
				string text = (string)base.GetParameter("td");
				if (text.Equals(string.Empty, StringComparison.Ordinal))
				{
					throw new OwaInvalidRequestException("Type down search string cannot be empty.");
				}
				Column column = ListViewColumns.GetColumn(this.ListViewState.SortedColumn);
				if (!column.IsTypeDownCapable)
				{
					throw new OwaInvalidRequestException("Type down search is not supported.");
				}
				VirtualListView2 listView = this.GetListView();
				listView.LoadData(text, rowCount);
				listView.RenderData(this.Writer);
				listView.RenderChunk(this.Writer);
				this.RenderExtraData(listView);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002CA8 RID: 11432
		protected abstract VirtualListView2 GetListView();

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000FA30C File Offset: 0x000F850C
		protected virtual void RenderExtraData(VirtualListView2 listView)
		{
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000FA30E File Offset: 0x000F850E
		protected virtual void PersistFilter()
		{
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000FA310 File Offset: 0x000F8510
		protected virtual OwaStoreObjectId GetSeekId()
		{
			return (OwaStoreObjectId)base.GetParameter("SId");
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000FA322 File Offset: 0x000F8522
		protected virtual OwaStoreObjectId GetNewSelection()
		{
			return null;
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000FA325 File Offset: 0x000F8525
		protected virtual void PersistSort()
		{
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000FA327 File Offset: 0x000F8527
		protected virtual void PersistMultiLineState()
		{
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000FA329 File Offset: 0x000F8529
		protected virtual void EndProcessEvent()
		{
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000FA32C File Offset: 0x000F852C
		private void InternalLoadFresh(bool renderHeaders)
		{
			try
			{
				int num = (int)base.GetParameter("SR");
				int rowCount = this.GetRowCount();
				if (num < 0)
				{
					throw new OwaInvalidRequestException("StartRange cannot be less than 0");
				}
				VirtualListView2 listView = this.GetListView();
				listView.LoadData(num, rowCount);
				listView.RenderData(this.Writer);
				listView.RenderChunk(this.Writer);
				this.RenderExtraData(listView);
				if (renderHeaders)
				{
					listView.RenderHeaders(this.Writer);
				}
				if (base.IsParameterSet("fltr"))
				{
					this.PersistFilter();
				}
			}
			finally
			{
				this.EndProcessEvent();
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.MailViewRefreshes.Increment();
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000FA3DC File Offset: 0x000F85DC
		private void LoadNextOrPrevious(SeekDirection seekDirection)
		{
			try
			{
				ObjectId adjacentId = (ObjectId)base.GetParameter("AId");
				int rowCount = this.GetRowCount();
				VirtualListView2 listView = this.GetListView();
				listView.LoadAdjacent(adjacentId, seekDirection, rowCount);
				listView.RenderData(this.Writer);
				listView.RenderChunk(this.Writer);
				this.RenderExtraData(listView);
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000FA44C File Offset: 0x000F864C
		private void SeekNextOrPrevious(SeekDirection seekDirection)
		{
			try
			{
				ObjectId seekRowId = (ObjectId)base.GetParameter("SId");
				int rowCount = this.GetRowCount();
				VirtualListView2 listView = this.GetListView();
				listView.LoadData(seekRowId, seekDirection, rowCount);
				listView.RenderData(this.Writer);
				listView.RenderChunk(this.Writer);
				this.RenderExtraData(listView);
				if (base.IsParameterSet("nwSel") && (bool)base.GetParameter("nwSel"))
				{
					this.RenderNewSelection(listView);
				}
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000FA4E0 File Offset: 0x000F86E0
		private void RenderNewSelection(VirtualListView2 listView)
		{
			MessageVirtualListView2 messageVirtualListView = listView as MessageVirtualListView2;
			OwaStoreObjectId owaStoreObjectId;
			if (messageVirtualListView != null && messageVirtualListView.GetNewSelectionId() != null)
			{
				owaStoreObjectId = messageVirtualListView.GetNewSelectionId();
			}
			else
			{
				owaStoreObjectId = this.GetNewSelection();
			}
			if (owaStoreObjectId != null)
			{
				this.Writer.Write("<div id=\"nwSel\" sId=\"");
				Utilities.HtmlEncode(owaStoreObjectId.ToString(), this.Writer);
				this.Writer.Write("\"></div>");
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000FA548 File Offset: 0x000F8748
		private int GetRowCount()
		{
			int num = (int)base.GetParameter("RC");
			if (num < 1)
			{
				throw new OwaInvalidRequestException("RowCount must be at least 1");
			}
			if (num > 100)
			{
				throw new OwaInvalidRequestException("RowCount cannot be more than " + 100.ToString());
			}
			return num;
		}

		// Token: 0x04001D8B RID: 7563
		public const int MaxSelectionSize = 500;

		// Token: 0x04001D8C RID: 7564
		public const int MaxChunkSize = 100;

		// Token: 0x04001D8D RID: 7565
		public const string MethodLoadFresh = "LoadFresh";

		// Token: 0x04001D8E RID: 7566
		public const string MethodLoadNext = "LoadNext";

		// Token: 0x04001D8F RID: 7567
		public const string MethodLoadPrevious = "LoadPrevious";

		// Token: 0x04001D90 RID: 7568
		public const string MethodSeekNext = "SeekNext";

		// Token: 0x04001D91 RID: 7569
		public const string MethodSeekPrevious = "SeekPrevious";

		// Token: 0x04001D92 RID: 7570
		public const string MethodSort = "Sort";

		// Token: 0x04001D93 RID: 7571
		public const string MethodSetMultiLineState = "SetML";

		// Token: 0x04001D94 RID: 7572
		public const string MethodTypeDown = "TypeDown";

		// Token: 0x04001D95 RID: 7573
		public const string State = "St";

		// Token: 0x04001D96 RID: 7574
		public const string StartRange = "SR";

		// Token: 0x04001D97 RID: 7575
		public const string RowCount = "RC";

		// Token: 0x04001D98 RID: 7576
		public const string AdjacentId = "AId";

		// Token: 0x04001D99 RID: 7577
		public const string SeekId = "SId";

		// Token: 0x04001D9A RID: 7578
		public const string ReturnNewSelection = "nwSel";

		// Token: 0x04001D9B RID: 7579
		public const string Id = "id";

		// Token: 0x04001D9C RID: 7580
		public const string TypeDownSearchString = "td";

		// Token: 0x04001D9D RID: 7581
		public const string ReadingPanePosition = "s";

		// Token: 0x04001D9E RID: 7582
		public const string FilterString = "fltr";

		// Token: 0x04001D9F RID: 7583
		public const string PaaPicker = "paa";

		// Token: 0x04001DA0 RID: 7584
		public const string MobilePicker = "mbl";

		// Token: 0x04001DA1 RID: 7585
		public const string IsNewestOnTop = "isNewestOnTop";

		// Token: 0x04001DA2 RID: 7586
		public const string ShowTreeInListView = "showTreeInListView";
	}
}
