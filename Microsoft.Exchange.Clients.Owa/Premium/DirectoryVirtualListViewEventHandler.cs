using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000492 RID: 1170
	internal abstract class DirectoryVirtualListViewEventHandler : VirtualListViewEventHandler2
	{
		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002D34 RID: 11572
		protected abstract ViewType ViewType { get; }

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x000FDF5C File Offset: 0x000FC15C
		protected override VirtualListViewState ListViewState
		{
			get
			{
				return this.DirectoryVirtualListViewState;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x000FDF64 File Offset: 0x000FC164
		private DirectoryVirtualListViewState DirectoryVirtualListViewState
		{
			get
			{
				return (DirectoryVirtualListViewState)base.GetParameter("St");
			}
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000FDF78 File Offset: 0x000FC178
		public static void Register()
		{
			OwaEventRegistry.RegisterStruct(typeof(DirectoryVirtualListViewState));
			OwaEventRegistry.RegisterHandler(typeof(DirectoryBrowserEventHandler));
			OwaEventRegistry.RegisterHandler(typeof(DirectoryPickerEventHandler));
			OwaEventRegistry.RegisterHandler(typeof(RoomBrowserEventHandler));
			OwaEventRegistry.RegisterHandler(typeof(RoomPickerEventHandler));
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000FDFD0 File Offset: 0x000FC1D0
		[OwaEvent("LoadFresh")]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(DirectoryVirtualListViewState))]
		[OwaEventParameter("fltr", typeof(string), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		public override void LoadFresh()
		{
			base.InternalLoadFresh();
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000FDFD8 File Offset: 0x000FC1D8
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEvent("LoadNext")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(DirectoryVirtualListViewState))]
		[OwaEventParameter("fltr", typeof(string), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		public override void LoadNext()
		{
			base.InternalLoadNext();
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000FDFE0 File Offset: 0x000FC1E0
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("LoadPrevious")]
		[OwaEventParameter("AId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(DirectoryVirtualListViewState))]
		[OwaEventParameter("fltr", typeof(string), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		public override void LoadPrevious()
		{
			base.InternalLoadPrevious();
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000FDFE8 File Offset: 0x000FC1E8
		[OwaEvent("SeekNext")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(DirectoryVirtualListViewState))]
		[OwaEventParameter("fltr", typeof(string), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		public override void SeekNext()
		{
			base.InternalSeekNext();
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000FDFF0 File Offset: 0x000FC1F0
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId))]
		[OwaEvent("SeekPrevious")]
		[OwaEventParameter("St", typeof(DirectoryVirtualListViewState))]
		[OwaEventParameter("fltr", typeof(string), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEventParameter("nwSel", typeof(bool), false, true)]
		public override void SeekPrevious()
		{
			base.InternalSeekPrevious();
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000FDFF8 File Offset: 0x000FC1F8
		[OwaEventParameter("St", typeof(DirectoryVirtualListViewState))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEvent("Sort")]
		[OwaEventParameter("SId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("fltr", typeof(string), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		public override void Sort()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000FDFFF File Offset: 0x000FC1FF
		[OwaEvent("SetML")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("SR", typeof(int))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventParameter("St", typeof(DirectoryVirtualListViewState))]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEventParameter("mbl", typeof(string), false, true)]
		public override void SetMultiLineState()
		{
			base.InternalSetMultiLineState();
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000FE007 File Offset: 0x000FC207
		[OwaEventParameter("mbl", typeof(string), false, true)]
		[OwaEventParameter("St", typeof(DirectoryVirtualListViewState))]
		[OwaEventParameter("RC", typeof(int))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("td", typeof(string))]
		[OwaEventParameter("fltr", typeof(string), false, true)]
		[OwaEventParameter("paa", typeof(int), false, true)]
		[OwaEvent("TypeDown")]
		public override void TypeDownSearch()
		{
			base.InternalTypeDownSearch();
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000FE010 File Offset: 0x000FC210
		protected void BindToAddressBook()
		{
			if (this.addressBookBase == null)
			{
				ADObjectId adobjectId = (ADObjectId)this.ListViewState.SourceContainerId;
				this.addressBookBase = DirectoryAssistance.FindAddressBook(adobjectId.ObjectGuid, base.UserContext);
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000FE050 File Offset: 0x000FC250
		protected override VirtualListView2 GetListView()
		{
			this.BindToAddressBook();
			bool isPAA = base.IsParameterSet("paa");
			bool isMobile = base.IsParameterSet("mbl");
			string cookie = this.DirectoryVirtualListViewState.Cookie;
			string preferredDC = this.DirectoryVirtualListViewState.PreferredDC;
			int cookieLcid = this.DirectoryVirtualListViewState.CookieLcid;
			string searchString = base.GetParameter("fltr") as string;
			int cookieIndex = this.DirectoryVirtualListViewState.CookieIndex;
			return new AddressBookVirtualListView(base.UserContext, "divVLV", this.ListViewState.IsMultiLine, this.ViewType, ColumnId.DisplayNameAD, SortOrder.Ascending, isPAA, isMobile, this.addressBookBase, searchString, cookie, cookieIndex, cookieLcid, preferredDC);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000FE0F3 File Offset: 0x000FC2F3
		[OwaEventParameter("s", typeof(ReadingPanePosition))]
		[OwaEvent("PersistReadingPane")]
		public void PersistReadingPane()
		{
			if (!base.UserContext.IsWebPartRequest)
			{
				this.PersistReadingPane((ReadingPanePosition)base.GetParameter("s"));
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000FE118 File Offset: 0x000FC318
		[OwaEventSegmentation(Feature.AddressLists)]
		[OwaEvent("AllAddressList")]
		public void GetAllAddressList()
		{
			this.RenderAllAddressBooks();
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000FE120 File Offset: 0x000FC320
		protected virtual void PersistReadingPane(ReadingPanePosition readingPanePosition)
		{
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000FE124 File Offset: 0x000FC324
		private void RenderAllAddressBooks()
		{
			SecondaryNavigationDirectoryList secondaryNavigationDirectoryList = SecondaryNavigationDirectoryList.CreateExtendedDirectoryList(base.UserContext);
			secondaryNavigationDirectoryList.RenderEntries(this.Writer);
		}

		// Token: 0x04001DE6 RID: 7654
		public const string MethodPersistReadingPane = "PersistReadingPane";

		// Token: 0x04001DE7 RID: 7655
		public const string MethodGetAllAddressList = "AllAddressList";

		// Token: 0x04001DE8 RID: 7656
		private AddressBookBase addressBookBase;
	}
}
