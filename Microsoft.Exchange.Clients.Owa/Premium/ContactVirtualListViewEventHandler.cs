using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000489 RID: 1161
	[OwaEventSegmentation(Feature.Contacts)]
	internal abstract class ContactVirtualListViewEventHandler : FolderVirtualListViewEventHandler2
	{
		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002D04 RID: 11524
		protected abstract ViewType ViewType { get; }

		// Token: 0x06002D05 RID: 11525 RVA: 0x000FD370 File Offset: 0x000FB570
		public new static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(ContactModuleEventHandler));
			OwaEventRegistry.RegisterHandler(typeof(ContactBrowserEventHandler));
			OwaEventRegistry.RegisterHandler(typeof(ContactPickerEventHandler));
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000FD39F File Offset: 0x000FB59F
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEvent("LoadFresh")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		public override void LoadFresh()
		{
			base.InternalLoadFresh();
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000FD3A7 File Offset: 0x000FB5A7
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEvent("LoadNext")]
		public override void LoadNext()
		{
			base.InternalLoadNext();
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000FD3AF File Offset: 0x000FB5AF
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEvent("LoadPrevious")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		public override void LoadPrevious()
		{
			base.InternalLoadPrevious();
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000FD3B7 File Offset: 0x000FB5B7
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		[OwaEvent("SeekNext")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		public override void SeekNext()
		{
			base.InternalSeekNext();
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000FD3BF File Offset: 0x000FB5BF
		[OwaEventParameter("RC", typeof(int))]
		[OwaEvent("SeekPrevious")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		public override void SeekPrevious()
		{
			base.InternalSeekPrevious();
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000FD3C7 File Offset: 0x000FB5C7
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEvent("Sort")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		public override void Sort()
		{
			base.InternalSort();
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000FD3CF File Offset: 0x000FB5CF
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEvent("SetML")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		public override void SetMultiLineState()
		{
			base.InternalSetMultiLineState();
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000FD3D7 File Offset: 0x000FB5D7
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("td", typeof(string))]
		[OwaEvent("TypeDown")]
		[OwaEventParameter("St", typeof(FolderVirtualListViewState))]
		[OwaEventParameter("srchf", typeof(FolderVirtualListViewSearchFilter), false, true)]
		[OwaEventParameter("fltr", typeof(int), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		public override void TypeDownSearch()
		{
			base.InternalTypeDownSearch();
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000FD3E0 File Offset: 0x000FB5E0
		protected override VirtualListView2 GetListView()
		{
			base.BindToFolder();
			bool isPAA = false;
			bool isMobile = false;
			if (base.IsParameterSet("paa"))
			{
				isPAA = true;
			}
			if (base.IsParameterSet("mbl"))
			{
				isMobile = true;
			}
			return new AddressBookVirtualListView(base.UserContext, "divVLV", this.ListViewState.IsMultiLine, this.ViewType, this.ListViewState.SortedColumn, this.ListViewState.SortOrder, base.IsFiltered, base.SearchScope, isPAA, isMobile, base.DataFolder, this.GetViewFilter());
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000FD468 File Offset: 0x000FB668
		protected override QueryFilter GetViewFilter()
		{
			ContactNavigationType filter = ContactNavigationType.All;
			if (base.IsParameterSet("fltr"))
			{
				filter = (ContactNavigationType)base.GetParameter("fltr");
			}
			base.FolderQueryFilter = ContactView.GetFilter(filter);
			return base.GetViewFilter();
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000FD4A8 File Offset: 0x000FB6A8
		protected override void RenderSearchInPublicFolder(TextWriter writer)
		{
			AdvancedFindComponents advancedFindComponents = AdvancedFindComponents.Categories | AdvancedFindComponents.SearchButton | AdvancedFindComponents.SearchTextInName;
			base.RenderAdvancedFind(this.Writer, advancedFindComponents, null);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000FD4C6 File Offset: 0x000FB6C6
		protected override void RenderAdvancedFind(TextWriter writer, OwaStoreObjectId folderId)
		{
			base.RenderAdvancedFind(writer, AdvancedFindComponents.Categories, folderId);
		}
	}
}
