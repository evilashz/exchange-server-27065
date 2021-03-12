using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000446 RID: 1094
	public class ContactView : FolderListViewSubPage, IRegistryOnlyForm
	{
		// Token: 0x06002768 RID: 10088 RVA: 0x000E05EC File Offset: 0x000DE7EC
		public ContactView() : base(ExTraceGlobals.ContactsCallTracer, ExTraceGlobals.ContactsTracer)
		{
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000E0636 File Offset: 0x000DE836
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.arrangeByMenu = new PersonViewArrangeByMenu();
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x000E064A File Offset: 0x000DE84A
		protected PersonViewArrangeByMenu ArrangeByMenu
		{
			get
			{
				return this.arrangeByMenu;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x000E0652 File Offset: 0x000DE852
		protected override string ContainerName
		{
			get
			{
				if (base.IsArchiveMailboxFolder)
				{
					return string.Format(LocalizedStrings.GetNonEncoded(-83764036), base.Folder.DisplayName, Utilities.GetMailboxOwnerDisplayName((MailboxSession)base.Folder.Session));
				}
				return base.ContainerName;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x000E0692 File Offset: 0x000DE892
		internal override StoreObjectId DefaultFolderId
		{
			get
			{
				return base.UserContext.ContactsFolderId;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x000E069F File Offset: 0x000DE89F
		protected override bool ShouldRenderELCCommentAndQuotaLink
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x000E06A2 File Offset: 0x000DE8A2
		protected override int ViewWidth
		{
			get
			{
				if (this.addressBookViewState == null)
				{
					return base.ViewWidth;
				}
				return Math.Max(365, 325);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000E06C2 File Offset: 0x000DE8C2
		protected override int ViewHeight
		{
			get
			{
				if (this.addressBookViewState != null)
				{
					return 250;
				}
				return base.ViewHeight;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x000E06D8 File Offset: 0x000DE8D8
		protected AddressBookContextMenu ContextMenu
		{
			get
			{
				if (this.contextMenu == null)
				{
					this.contextMenu = new AddressBookContextMenu(base.UserContext, this.InAddressBook, true);
				}
				return this.contextMenu;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x000E0700 File Offset: 0x000DE900
		protected override SortOrder DefaultSortOrder
		{
			get
			{
				return SortOrder.Ascending;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x000E0703 File Offset: 0x000DE903
		protected override ColumnId DefaultSortedColumn
		{
			get
			{
				return ColumnId.FileAs;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x000E0707 File Offset: 0x000DE907
		protected override ReadingPanePosition DefaultReadingPanePosition
		{
			get
			{
				return AddressBookViewState.DefaultReadingPanePosition;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x000E070E File Offset: 0x000DE90E
		protected override ReadingPanePosition ReadingPanePosition
		{
			get
			{
				if (this.addressBookViewState != null)
				{
					return this.addressBookViewState.ReadingPanePosition;
				}
				return base.ReadingPanePosition;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000E072A File Offset: 0x000DE92A
		protected override bool DefaultMultiLineSetting
		{
			get
			{
				return this.addressBookViewState == null || this.addressBookViewState.DefaultMultiLineSetting;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x000E0741 File Offset: 0x000DE941
		protected override bool IsMultiLine
		{
			get
			{
				if (this.addressBookViewState != null)
				{
					return this.addressBookViewState.IsMultiLine;
				}
				return base.IsMultiLine;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x000E075D File Offset: 0x000DE95D
		protected override bool FindBarOn
		{
			get
			{
				if (base.IsPublicFolder)
				{
					return false;
				}
				if (this.addressBookViewState != null)
				{
					return this.addressBookViewState.FindBarOn;
				}
				return base.UserContext.UserOptions.ContactsFindBarOn;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000E078D File Offset: 0x000DE98D
		protected bool InAddressBook
		{
			get
			{
				return this.location != ContactView.Location.ContactModule;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x000E079B File Offset: 0x000DE99B
		protected static int StoreObjectTypeContactsFolder
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000E079E File Offset: 0x000DE99E
		protected override void LoadViewState()
		{
			base.LoadViewState();
			if (this.InAddressBook)
			{
				this.addressBookViewState = AddressBookViewState.Load(base.UserContext, this.location == ContactView.Location.AddressBookPicker, false);
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000E07CC File Offset: 0x000DE9CC
		protected override IListView CreateListView(ColumnId sortedColumn, SortOrder sortOrder)
		{
			AddressBookVirtualListView addressBookVirtualListView = new AddressBookVirtualListView(base.UserContext, "divVLV", this.IsMultiLine, ContactView.viewType[(int)this.location], sortedColumn, sortOrder, this.isPaaPkr, this.isMobilePicker, base.Folder, ContactView.GetFilter(this.filter));
			VirtualListView2 virtualListView = addressBookVirtualListView;
			string name = "iFltr";
			int num = (int)this.filter;
			virtualListView.AddAttribute(name, num.ToString(CultureInfo.InvariantCulture));
			addressBookVirtualListView.LoadData(0, 50);
			return addressBookVirtualListView;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000E0843 File Offset: 0x000DEA43
		protected override IListViewDataSource CreateDataSource(ListView listView)
		{
			return new FolderListViewDataSource(base.UserContext, listView.Properties, base.Folder, listView.GetSortByProperties(), ContactView.GetFilter(this.filter));
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000E0870 File Offset: 0x000DEA70
		protected override Toolbar CreateListToolbar()
		{
			Toolbar result;
			if (this.addressBookViewState != null)
			{
				result = new AddressBookViewListToolbar(this.IsMultiLine, this.ReadingPanePosition);
			}
			else
			{
				result = new PersonViewListToolbar(this.IsMultiLine, base.IsPublicFolder, base.UserContext.IsWebPartRequest, this.ReadingPanePosition);
			}
			return result;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000E08BD File Offset: 0x000DEABD
		protected override Toolbar CreateActionToolbar()
		{
			if (this.addressBookViewState != null)
			{
				return new AddressBookViewActionToolbar();
			}
			return new PersonViewActionToolbar();
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000E08D4 File Offset: 0x000DEAD4
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.DetermineContext();
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "fltr", false);
			int num;
			if (string.IsNullOrEmpty(queryStringParameter) || !int.TryParse(queryStringParameter, NumberStyles.Integer, CultureInfo.InvariantCulture, out num) || num < 1 || num > 3)
			{
				this.filter = ContactNavigationType.All;
				return;
			}
			this.filter = (ContactNavigationType)num;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000E0930 File Offset: 0x000DEB30
		internal static QueryFilter GetFilter(ContactNavigationType filter)
		{
			QueryFilter result = null;
			if (ContactNavigationType.People == filter)
			{
				result = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Contact");
			}
			else if (ContactNavigationType.DistributionList == filter)
			{
				result = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.DistList");
			}
			return result;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000E096C File Offset: 0x000DEB6C
		internal static void RenderSecondaryNavigation(TextWriter output, UserContext userContext, bool isPicker)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (!isPicker)
			{
				ContactView.RenderSecondaryNavigationFilter(output, "divCntFlt");
			}
			NavigationHost.RenderNavigationTreeControl(output, userContext, NavigationModule.Contacts);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000E09A0 File Offset: 0x000DEBA0
		internal static void RenderSecondaryNavigationFilter(TextWriter output, string filterId)
		{
			SecondaryNavigationFilter secondaryNavigationFilter = new SecondaryNavigationFilter(filterId, LocalizedStrings.GetNonEncoded(-428271462), "onClkCntF(\"" + Utilities.JavascriptEncode(filterId) + "\")");
			secondaryNavigationFilter.AddFilter(LocalizedStrings.GetNonEncoded(-1069600488), 1, true);
			secondaryNavigationFilter.AddFilter(LocalizedStrings.GetNonEncoded(-1434067361), 2, false);
			secondaryNavigationFilter.AddFilter(LocalizedStrings.GetNonEncoded(171820412), 3, false);
			secondaryNavigationFilter.Render(output);
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000E0A10 File Offset: 0x000DEC10
		private void DetermineContext()
		{
			this.location = ContactView.Location.ContactModule;
			string state = base.OwaContext.FormsRegistryContext.State;
			this.action = base.OwaContext.FormsRegistryContext.Action;
			if (string.CompareOrdinal(this.action, "PAA") == 0)
			{
				this.isPaaPkr = true;
			}
			else if (string.CompareOrdinal(this.action, "Mobile") == 0)
			{
				this.isMobilePicker = true;
			}
			if (state != null)
			{
				object obj = ContactView.locationParser.Parse(state);
				this.location = ((obj != null) ? ((ContactView.Location)obj) : this.location);
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x000E0AA5 File Offset: 0x000DECA5
		protected override bool ShouldRenderToolbar
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x000E0AA8 File Offset: 0x000DECA8
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x000E0AB0 File Offset: 0x000DECB0
		public override SanitizedHtmlString Title
		{
			get
			{
				return new SanitizedHtmlString(this.ContainerName);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000E0ABD File Offset: 0x000DECBD
		public override string PageType
		{
			get
			{
				return "ContactViewPage";
			}
		}

		// Token: 0x04001B9A RID: 7066
		private const string FilterString = "fltr";

		// Token: 0x04001B9B RID: 7067
		private static readonly ViewType[] viewType = new ViewType[]
		{
			ViewType.ContactModule,
			ViewType.ContactBrowser,
			ViewType.ContactPicker
		};

		// Token: 0x04001B9C RID: 7068
		private static readonly FastEnumParser locationParser = new FastEnumParser(typeof(ContactView.Location), true);

		// Token: 0x04001B9D RID: 7069
		private static readonly PropertyDefinition[] requiredProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName,
			FolderSchema.ItemCount
		};

		// Token: 0x04001B9E RID: 7070
		private PersonViewArrangeByMenu arrangeByMenu;

		// Token: 0x04001B9F RID: 7071
		private AddressBookViewState addressBookViewState;

		// Token: 0x04001BA0 RID: 7072
		private ContactView.Location location;

		// Token: 0x04001BA1 RID: 7073
		private ContactNavigationType filter = ContactNavigationType.All;

		// Token: 0x04001BA2 RID: 7074
		private string action;

		// Token: 0x04001BA3 RID: 7075
		private bool isPaaPkr;

		// Token: 0x04001BA4 RID: 7076
		private bool isMobilePicker;

		// Token: 0x04001BA5 RID: 7077
		private AddressBookContextMenu contextMenu;

		// Token: 0x04001BA6 RID: 7078
		private string[] externalScriptFiles = new string[]
		{
			"uview.js",
			"contactvw.js",
			"vlv.js"
		};

		// Token: 0x02000447 RID: 1095
		public enum Location
		{
			// Token: 0x04001BA8 RID: 7080
			ContactModule,
			// Token: 0x04001BA9 RID: 7081
			AddressBookBrowse,
			// Token: 0x04001BAA RID: 7082
			AddressBookPicker
		}
	}
}
