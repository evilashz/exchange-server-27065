using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200049B RID: 1179
	[OwaEventObjectId(typeof(OwaStoreObjectId))]
	[OwaEventNamespace("DumpsterVLV")]
	internal sealed class DumpsterVirtualListViewEventHandler : VirtualListViewEventHandler2
	{
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000FFC58 File Offset: 0x000FDE58
		protected override VirtualListViewState ListViewState
		{
			get
			{
				return (FolderVirtualListViewState)base.GetParameter("St");
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000FFC6A File Offset: 0x000FDE6A
		private Folder DataFolder
		{
			get
			{
				return this.searchFolder ?? this.folder;
			}
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000FFC7C File Offset: 0x000FDE7C
		protected override VirtualListView2 GetListView()
		{
			this.BindToFolder();
			return new DumpsterVirtualListView(base.UserContext, "divVLV", this.ListViewState.SortedColumn, this.ListViewState.SortOrder, this.DataFolder);
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000FFCB0 File Offset: 0x000FDEB0
		private void BindToFolder()
		{
			if (this.folder == null)
			{
				OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
				if (owaStoreObjectId == null)
				{
					owaStoreObjectId = (OwaStoreObjectId)this.ListViewState.SourceContainerId;
				}
				this.folder = Utilities.GetFolder<Folder>(base.UserContext, owaStoreObjectId, new PropertyDefinition[]
				{
					FolderSchema.ItemCount,
					FolderSchema.ExtendedFolderFlags,
					ViewStateProperties.ViewFilter,
					ViewStateProperties.SortColumn
				});
				if (base.IsParameterSet("srchf") && (base.UserContext.IsInMyMailbox(this.folder) || Utilities.IsInArchiveMailbox(this.folder)))
				{
					FolderVirtualListViewSearchFilter folderVirtualListViewSearchFilter = (FolderVirtualListViewSearchFilter)base.GetParameter("srchf");
					if (folderVirtualListViewSearchFilter.SearchString != null && 256 < folderVirtualListViewSearchFilter.SearchString.Length)
					{
						throw new OwaInvalidRequestException("Search string is longer than maximum length of " + 256);
					}
					FolderSearch folderSearch = new FolderSearch();
					string searchString = folderVirtualListViewSearchFilter.SearchString;
					this.searchFolder = folderSearch.Execute(base.UserContext, this.folder, folderVirtualListViewSearchFilter.Scope, searchString, folderVirtualListViewSearchFilter.ReExecuteSearch, folderVirtualListViewSearchFilter.IsAsyncSearchEnabled);
				}
			}
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000FFDDA File Offset: 0x000FDFDA
		protected override void EndProcessEvent()
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
			if (this.searchFolder != null)
			{
				this.searchFolder.Dispose();
				this.searchFolder = null;
			}
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000FFE10 File Offset: 0x000FE010
		private void RenderErrorInfobarMessage(Strings.IDs errID)
		{
			this.Writer.Write("<div id=eib>");
			this.Writer.Write(LocalizedStrings.GetHtmlEncoded(errID));
			this.Writer.Write("</div>");
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000FFE43 File Offset: 0x000FE043
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventSegmentation(Feature.Dumpster)]
		[OwaEvent("LoadFresh")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		public override void LoadFresh()
		{
			base.InternalLoadFresh();
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000FFE4B File Offset: 0x000FE04B
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEvent("LoadNext")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventSegmentation(Feature.Dumpster)]
		public override void LoadNext()
		{
			base.InternalLoadNext();
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000FFE53 File Offset: 0x000FE053
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEvent("LoadPrevious")]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventSegmentation(Feature.Dumpster)]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		public override void LoadPrevious()
		{
			base.InternalLoadPrevious();
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000FFE5B File Offset: 0x000FE05B
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		[OwaEventSegmentation(Feature.Dumpster)]
		[OwaEvent("SeekNext")]
		public override void SeekNext()
		{
			base.InternalSeekNext();
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000FFE63 File Offset: 0x000FE063
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("SeekPrevious")]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		[OwaEventSegmentation(Feature.Dumpster)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		public override void SeekPrevious()
		{
			base.InternalSeekPrevious();
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000FFE6B File Offset: 0x000FE06B
		[OwaEvent("Sort")]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventSegmentation(Feature.Dumpster)]
		[OwaEventVerb(OwaEventVerb.Post)]
		public override void Sort()
		{
			base.InternalSort();
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000FFE73 File Offset: 0x000FE073
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventSegmentation(Feature.Dumpster)]
		[OwaEvent("SetML")]
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		public override void SetMultiLineState()
		{
			base.InternalSetMultiLineState();
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000FFE7B File Offset: 0x000FE07B
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("td", typeof(string))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventSegmentation(Feature.Dumpster)]
		[OwaEvent("TypeDown")]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		public override void TypeDownSearch()
		{
			base.InternalTypeDownSearch();
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000FFE84 File Offset: 0x000FE084
		[OwaEventSegmentation(Feature.Dumpster)]
		[OwaEventParameter("Itms", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEvent("PermanentDelete")]
		public void PermanentDelete()
		{
			try
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "DumpsterVirtualListViewEventHandler.PermanentDelete");
				OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
				this.folder = Utilities.GetFolderForContent<Folder>(base.UserContext, folderId, null);
				OperationResult operationResult = this.DoDelete();
				if (operationResult == OperationResult.PartiallySucceeded)
				{
					this.RenderErrorInfobarMessage(1086565410);
				}
				else if (operationResult == OperationResult.Failed)
				{
					this.RenderErrorInfobarMessage(1546128956);
				}
			}
			finally
			{
				this.EndProcessEvent();
			}
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000FFF0C File Offset: 0x000FE10C
		private OperationResult DoDelete()
		{
			StoreObjectId[] dumpsterIds = this.GetDumpsterIds();
			return this.folder.DeleteObjects(DeleteItemFlags.HardDelete, dumpsterIds).OperationResult;
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000FFF32 File Offset: 0x000FE132
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(DumpsterVirtualListViewEventHandler));
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000FFF44 File Offset: 0x000FE144
		private StoreObjectId[] GetDumpsterIds()
		{
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("Itms");
			if (array.Length > 500)
			{
				throw new OwaInvalidOperationException(string.Format("Operating on {0} item(s) in a single request is not supported", array.Length));
			}
			StoreObjectId[] array2 = new StoreObjectId[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i].StoreObjectId;
			}
			return array2;
		}

		// Token: 0x04001E1F RID: 7711
		public const string EventNamespace = "DumpsterVLV";

		// Token: 0x04001E20 RID: 7712
		public const string MethodPermanentDelete = "PermanentDelete";

		// Token: 0x04001E21 RID: 7713
		public const string Items = "Itms";

		// Token: 0x04001E22 RID: 7714
		public const string FolderId = "fId";

		// Token: 0x04001E23 RID: 7715
		public const string SearchFilter = "srchf";

		// Token: 0x04001E24 RID: 7716
		private const int MaxItemsPerRequest = 500;

		// Token: 0x04001E25 RID: 7717
		private Folder folder;

		// Token: 0x04001E26 RID: 7718
		private Folder searchFolder;
	}
}
