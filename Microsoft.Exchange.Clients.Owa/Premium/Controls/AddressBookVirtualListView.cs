using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000313 RID: 787
	internal class AddressBookVirtualListView : VirtualListView2
	{
		// Token: 0x06001DCD RID: 7629 RVA: 0x000ACE54 File Offset: 0x000AB054
		internal AddressBookVirtualListView(UserContext userContext, string id, bool isMultiLine, ViewType viewType, ColumnId sortedColumn, SortOrder sortOrder, bool isPAA, bool isMobile, AddressBookBase addressBookBase) : this(userContext, id, isMultiLine, viewType, sortedColumn, sortOrder, isPAA, isMobile, addressBookBase, null, null, 0, Culture.GetUserCulture().LCID, null)
		{
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000ACE84 File Offset: 0x000AB084
		internal AddressBookVirtualListView(UserContext userContext, string id, bool isMultiLine, ViewType viewType, ColumnId sortedColumn, SortOrder sortOrder, bool isPAA, bool isMobile, AddressBookBase addressBookBase, string searchString, string cookie, int cookieIndex, int cookieLcid, string preferredDC) : this(userContext, id, isMultiLine, viewType, sortedColumn, sortOrder, !string.IsNullOrEmpty(searchString), isPAA, isMobile)
		{
			this.addressBookBase = addressBookBase;
			this.searchString = searchString;
			this.cookie = cookie;
			this.cookieIndex = cookieIndex;
			this.cookieLcid = cookieLcid;
			this.preferredDC = preferredDC;
			if (string.IsNullOrEmpty(this.cookie))
			{
				this.cookie = string.Empty;
				this.preferredDC = string.Empty;
				this.cookieLcid = Culture.GetUserCulture().LCID;
			}
			if (this.cookieLcid < 0)
			{
				this.cookieLcid = Culture.GetUserCulture().LCID;
			}
			if (!string.IsNullOrEmpty(this.searchString))
			{
				if (256 < this.searchString.Length)
				{
					throw new OwaInvalidInputException("Search string is longer than maximum length of " + 256);
				}
				if (this.cookieIndex < 0)
				{
					this.cookie = string.Empty;
					this.cookieIndex = 0;
					this.preferredDC = string.Empty;
					this.cookieLcid = Culture.GetUserCulture().LCID;
				}
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000ACF9C File Offset: 0x000AB19C
		internal AddressBookVirtualListView(UserContext userContext, string id, bool isMultiLine, ViewType viewType, ColumnId sortedColumn, SortOrder sortOrder, bool isPAA, bool isMobile, Folder folder, QueryFilter filter) : this(userContext, id, isMultiLine, viewType, sortedColumn, sortOrder, false, SearchScope.SelectedFolder, isPAA, isMobile, folder, filter)
		{
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000ACFC4 File Offset: 0x000AB1C4
		internal AddressBookVirtualListView(UserContext userContext, string id, bool isMultiLine, ViewType viewType, ColumnId sortedColumn, SortOrder sortOrder, bool isFiltered, SearchScope folderScope, bool isPAA, bool isMobile, Folder folder, QueryFilter filter) : this(userContext, id, isMultiLine, viewType, sortedColumn, sortOrder, isFiltered, isPAA, isMobile)
		{
			this.folderScope = folderScope;
			this.folder = folder;
			this.filter = filter;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000AD000 File Offset: 0x000AB200
		internal AddressBookVirtualListView(UserContext userContext, string id, bool isMultiLine, ViewType viewType, ColumnId sortedColumn, SortOrder sortOrder, bool isFiltered, bool isPAA, bool isMobile) : base(userContext, id, isMultiLine, sortedColumn, sortOrder, isFiltered)
		{
			this.isPAA = isPAA;
			this.isMobile = isMobile;
			AddressBookVirtualListView.ValidateViewType(viewType);
			this.viewType = viewType;
			if (isPAA)
			{
				base.AddAttribute("fPaa", "1");
			}
			if (isMobile)
			{
				base.AddAttribute("fMbl", "1");
			}
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000AD07C File Offset: 0x000AB27C
		private static void ValidateViewType(ViewType viewType)
		{
			switch (viewType)
			{
			case ViewType.ContactModule:
			case ViewType.ContactBrowser:
			case ViewType.ContactPicker:
			case ViewType.DirectoryBrowser:
			case ViewType.DirectoryPicker:
			case ViewType.RoomBrowser:
			case ViewType.RoomPicker:
				return;
			}
			throw new ArgumentException(string.Format("Invalid ViewType for AddressBookVirtualListView: {0}.Only ContactModule, ContactLookup, ContactPicker, DirectoryLookup, DirectoryPicker,RoomLookup, and RoomPicker are valid", (int)viewType));
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x000AD0CB File Offset: 0x000AB2CB
		private bool IsPicker
		{
			get
			{
				return this.viewType == ViewType.ContactPicker || this.viewType == ViewType.DirectoryPicker || this.viewType == ViewType.RoomPicker;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x000AD0EA File Offset: 0x000AB2EA
		private bool IsContactView
		{
			get
			{
				return this.viewType == ViewType.ContactModule || this.viewType == ViewType.ContactBrowser || this.viewType == ViewType.ContactPicker;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x000AD109 File Offset: 0x000AB309
		protected override Folder DataFolder
		{
			get
			{
				if (this.IsContactView)
				{
					return this.folder;
				}
				throw new NotImplementedException("DataFolder is not valid only for contact view");
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000AD124 File Offset: 0x000AB324
		public override ViewType ViewType
		{
			get
			{
				return this.viewType;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x000AD12C File Offset: 0x000AB32C
		public string Cookie
		{
			get
			{
				return this.cookie;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x000AD134 File Offset: 0x000AB334
		public int CookieLcid
		{
			get
			{
				return this.cookieLcid;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000AD13C File Offset: 0x000AB33C
		public string PreferredDC
		{
			get
			{
				return this.preferredDC;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x000AD144 File Offset: 0x000AB344
		public override string OehNamespace
		{
			get
			{
				switch (this.viewType)
				{
				case ViewType.ContactModule:
					return "CM";
				case ViewType.ContactBrowser:
					return "CB";
				case ViewType.ContactPicker:
					return "CP";
				case ViewType.DirectoryBrowser:
					return "DB";
				case ViewType.DirectoryPicker:
					return "DP";
				case ViewType.RoomBrowser:
					return "RB";
				case ViewType.RoomPicker:
					return "RP";
				}
				throw new ArgumentException(string.Format("Invalid ViewType for AddressBookVirtualListView: {0}.Only ContactModule, ContactLookup, ContactPicker, DirectoryLookup, DirectoryPicker,RoomLookup, and RoomPicker are valid", (int)this.viewType));
			}
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000AD1C6 File Offset: 0x000AB3C6
		public override bool GetDidLastSearchFail()
		{
			return this.IsContactView && base.GetDidLastSearchFail();
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000AD1D8 File Offset: 0x000AB3D8
		public override void LoadData(int startRange, int rowCount)
		{
			base.LoadData(startRange, rowCount);
			this.UpdateCookieInformation();
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000AD1E8 File Offset: 0x000AB3E8
		public override void LoadData(ObjectId seekRowId, SeekDirection seekDirection, int rowCount)
		{
			if (!this.IsContactView)
			{
				throw new NotImplementedException();
			}
			base.LoadData(seekRowId, seekDirection, rowCount);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000AD201 File Offset: 0x000AB401
		public override void LoadData(string seekValue, int rowCount)
		{
			base.LoadData(seekValue, rowCount);
			this.UpdateCookieInformation();
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000AD211 File Offset: 0x000AB411
		public override void LoadAdjacent(ObjectId adjacentId, SeekDirection seekDirection, int rowCount)
		{
			if (!this.IsContactView)
			{
				throw new NotImplementedException();
			}
			base.LoadAdjacent(adjacentId, seekDirection, rowCount);
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x000AD22C File Offset: 0x000AB42C
		protected override ListViewContents2 CreateListViewContents()
		{
			ViewDescriptor viewDescriptor = AddressBookViewDescriptors.GetViewDescriptor(this.IsMultiLine, this.IsContactView ? Utilities.IsJapanese : base.UserContext.IsPhoneticNamesEnabled, this.isMobile, this.ViewType);
			return new AddressBookMultiLineList2(viewDescriptor, this.IsContactView, this.IsPicker, base.SortedColumn, base.SortOrder, base.UserContext, this.folderScope, this.isPAA, this.isMobile);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x000AD2A4 File Offset: 0x000AB4A4
		protected override IListViewDataSource CreateDataSource(Hashtable properties)
		{
			if (this.IsContactView)
			{
				return new FolderListViewDataSource(base.UserContext, properties, this.folder, this.GetSortByProperties(), this.filter);
			}
			if (string.IsNullOrEmpty(this.searchString))
			{
				if (string.IsNullOrEmpty(this.cookie))
				{
					return ADListViewDataSource.CreateForBrowse(properties, this.addressBookBase, base.UserContext);
				}
				return ADListViewDataSource.CreateForBrowse(properties, this.addressBookBase, this.cookie, this.cookieLcid, this.preferredDC, base.UserContext);
			}
			else
			{
				if (string.IsNullOrEmpty(this.cookie))
				{
					return ADListViewDataSource.CreateForSearch(properties, this.addressBookBase, this.searchString, base.UserContext);
				}
				return ADListViewDataSource.CreateForSearch(properties, this.addressBookBase, this.searchString, this.cookie, this.cookieIndex, this.cookieLcid, this.preferredDC, base.UserContext);
			}
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000AD380 File Offset: 0x000AB580
		protected override void OnBeforeRender()
		{
			if (!this.IsContactView)
			{
				base.AddAttribute("sCki", this.Cookie);
				base.AddAttribute("iLcid", this.CookieLcid.ToString(CultureInfo.InvariantCulture));
				base.AddAttribute("sPfdDC", this.PreferredDC);
			}
			base.MakePropertyPublic("ea");
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000AD3E0 File Offset: 0x000AB5E0
		protected override void InternalRenderData(TextWriter writer)
		{
			base.InternalRenderData(writer);
			if (!this.IsContactView)
			{
				VirtualListView2.RenderAttribute(writer, "sCki", this.Cookie);
				VirtualListView2.RenderAttribute(writer, "iLcid", this.CookieLcid);
				VirtualListView2.RenderAttribute(writer, "sPfdDC", this.PreferredDC);
			}
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000AD430 File Offset: 0x000AB630
		private SortBy[] GetSortByProperties()
		{
			SortBy[] array;
			if (base.SortedColumn == ColumnId.FileAs)
			{
				array = new SortBy[]
				{
					new SortBy(ContactBaseSchema.FileAs, base.SortOrder)
				};
			}
			else if (base.SortedColumn == ColumnId.ContactFlagDueDate)
			{
				array = new SortBy[]
				{
					new SortBy(ItemSchema.FlagStatus, base.SortOrder),
					new SortBy(ItemSchema.UtcDueDate, (base.SortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending),
					new SortBy(ItemSchema.ItemColor, base.SortOrder),
					new SortBy(ContactBaseSchema.FileAs, base.SortOrder)
				};
			}
			else if (base.SortedColumn == ColumnId.ContactFlagStartDate)
			{
				array = new SortBy[]
				{
					new SortBy(ItemSchema.FlagStatus, base.SortOrder),
					new SortBy(ItemSchema.UtcStartDate, (base.SortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending),
					new SortBy(ItemSchema.ItemColor, base.SortOrder),
					new SortBy(ContactBaseSchema.FileAs, base.SortOrder)
				};
			}
			else
			{
				array = new SortBy[2];
				Column column = ListViewColumns.GetColumn(base.SortedColumn);
				array[0] = new SortBy(column[0], base.SortOrder);
				array[1] = new SortBy(ContactBaseSchema.FileAs, base.SortOrder);
			}
			return array;
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000AD570 File Offset: 0x000AB770
		private void UpdateCookieInformation()
		{
			if (!this.IsContactView)
			{
				ADListViewDataSource adlistViewDataSource = (ADListViewDataSource)base.Contents.DataSource;
				this.cookie = adlistViewDataSource.Cookie;
				this.cookieLcid = adlistViewDataSource.Lcid;
				this.preferredDC = adlistViewDataSource.PreferredDC;
			}
		}

		// Token: 0x04001638 RID: 5688
		private const int MaxSearchStringLength = 256;

		// Token: 0x04001639 RID: 5689
		private const string InvalidViewTypeMessage = "Invalid ViewType for AddressBookVirtualListView: {0}.Only ContactModule, ContactLookup, ContactPicker, DirectoryLookup, DirectoryPicker,RoomLookup, and RoomPicker are valid";

		// Token: 0x0400163A RID: 5690
		private ViewType viewType;

		// Token: 0x0400163B RID: 5691
		private SearchScope folderScope;

		// Token: 0x0400163C RID: 5692
		private bool isPAA;

		// Token: 0x0400163D RID: 5693
		private bool isMobile;

		// Token: 0x0400163E RID: 5694
		private AddressBookBase addressBookBase;

		// Token: 0x0400163F RID: 5695
		private string searchString;

		// Token: 0x04001640 RID: 5696
		private string cookie = string.Empty;

		// Token: 0x04001641 RID: 5697
		private int cookieIndex;

		// Token: 0x04001642 RID: 5698
		private int cookieLcid;

		// Token: 0x04001643 RID: 5699
		private string preferredDC = string.Empty;

		// Token: 0x04001644 RID: 5700
		private Folder folder;

		// Token: 0x04001645 RID: 5701
		private QueryFilter filter;
	}
}
