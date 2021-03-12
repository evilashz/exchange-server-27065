using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000449 RID: 1097
	public class DirectoryView : ListViewSubPage, IRegistryOnlyForm
	{
		// Token: 0x06002790 RID: 10128 RVA: 0x000E0B98 File Offset: 0x000DED98
		public DirectoryView() : base(ExTraceGlobals.DirectoryCallTracer, ExTraceGlobals.DirectoryTracer)
		{
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x000E0BDA File Offset: 0x000DEDDA
		protected string AddressListName
		{
			get
			{
				return this.addressBookBase.DisplayName;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x000E0BE7 File Offset: 0x000DEDE7
		protected override int ViewWidth
		{
			get
			{
				return Math.Max(365, 325);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x000E0BF8 File Offset: 0x000DEDF8
		protected override int ViewHeight
		{
			get
			{
				return 250;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x000E0BFF File Offset: 0x000DEDFF
		protected override SortOrder DefaultSortOrder
		{
			get
			{
				return SortOrder.Ascending;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x000E0C02 File Offset: 0x000DEE02
		protected override SortOrder SortOrder
		{
			get
			{
				return this.DefaultSortOrder;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x000E0C0A File Offset: 0x000DEE0A
		protected override ColumnId DefaultSortedColumn
		{
			get
			{
				return ColumnId.DisplayNameAD;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x000E0C0E File Offset: 0x000DEE0E
		protected override ColumnId SortedColumn
		{
			get
			{
				return this.DefaultSortedColumn;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x000E0C16 File Offset: 0x000DEE16
		protected override ReadingPanePosition DefaultReadingPanePosition
		{
			get
			{
				if (this.IsPicker)
				{
					return ReadingPanePosition.Right;
				}
				return AddressBookViewState.DefaultReadingPanePosition;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x000E0C27 File Offset: 0x000DEE27
		protected override ReadingPanePosition ReadingPanePosition
		{
			get
			{
				return this.addressBookViewState.ReadingPanePosition;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x000E0C34 File Offset: 0x000DEE34
		protected override bool DefaultMultiLineSetting
		{
			get
			{
				return this.addressBookViewState.DefaultMultiLineSetting;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x000E0C41 File Offset: 0x000DEE41
		protected override bool IsMultiLine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x000E0C44 File Offset: 0x000DEE44
		protected override bool FindBarOn
		{
			get
			{
				return this.addressBookViewState.FindBarOn;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x000E0C51 File Offset: 0x000DEE51
		protected override bool AllowAdvancedSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000E0C54 File Offset: 0x000DEE54
		protected override bool RenderSearchDropDown
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x000E0C57 File Offset: 0x000DEE57
		protected AddressBookContextMenu ContextMenu
		{
			get
			{
				return this.contextMenu;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000E0C5F File Offset: 0x000DEE5F
		private bool IsPicker
		{
			get
			{
				return (this.type & DirectoryView.Type.Picker) != DirectoryView.Type.None;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000E0C6F File Offset: 0x000DEE6F
		private bool IsDirectoryPaaPicker
		{
			get
			{
				return (this.type & DirectoryView.Type.PaaPicker) != DirectoryView.Type.None;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000E0C7F File Offset: 0x000DEE7F
		private bool IsRoomView
		{
			get
			{
				return (this.type & DirectoryView.Type.Rooms) != DirectoryView.Type.None;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x000E0C8F File Offset: 0x000DEE8F
		protected override bool ShouldRenderToolbar
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000E0C92 File Offset: 0x000DEE92
		protected override void LoadViewState()
		{
			this.addressBookViewState = AddressBookViewState.Load(base.UserContext, this.IsPicker, this.IsRoomView);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000E0CB4 File Offset: 0x000DEEB4
		protected override IListView CreateListView(ColumnId sortedColumn, SortOrder sortOrder)
		{
			bool isMobile = Utilities.IsFlagSet((int)this.type, 8);
			AddressBookVirtualListView addressBookVirtualListView = new AddressBookVirtualListView(base.UserContext, "divVLV", this.IsMultiLine, this.viewType, sortedColumn, sortOrder, base.IsPersonalAutoAttendantPicker || this.IsDirectoryPaaPicker, isMobile, this.addressBookBase);
			addressBookVirtualListView.LoadData(0, 50);
			return addressBookVirtualListView;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000E0D0F File Offset: 0x000DEF0F
		protected override IListViewDataSource CreateDataSource(ListView listView)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000E0D18 File Offset: 0x000DEF18
		protected override Toolbar CreateListToolbar()
		{
			return new AddressBookViewListToolbar(this.IsMultiLine, this.ReadingPanePosition);
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000E0D38 File Offset: 0x000DEF38
		protected override Toolbar CreateActionToolbar()
		{
			return new AddressBookViewActionToolbar();
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000E0D40 File Offset: 0x000DEF40
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.contextMenu = new AddressBookContextMenu(base.UserContext, true, false);
			string text = base.OwaContext.FormsRegistryContext.Type;
			string action = base.OwaContext.FormsRegistryContext.Action;
			if (text != null)
			{
				object obj = DirectoryView.elementClassParser.Parse(text);
				if (obj != null && (DirectoryView.ElementClass)obj == DirectoryView.ElementClass.Rooms)
				{
					this.type |= DirectoryView.Type.Rooms;
					this.viewType = ViewType.RoomBrowser;
				}
			}
			if (!string.IsNullOrEmpty(action))
			{
				if (string.Equals(action, "Pick", StringComparison.OrdinalIgnoreCase))
				{
					this.viewType = ((DirectoryView.Type.Rooms == this.type) ? ViewType.RoomPicker : ViewType.DirectoryPicker);
					this.type |= DirectoryView.Type.Picker;
				}
				else if (string.Equals(action, "PickPaa", StringComparison.OrdinalIgnoreCase))
				{
					this.type |= (DirectoryView.Type.Picker | DirectoryView.Type.PaaPicker);
					this.viewType = ViewType.DirectoryPicker;
				}
				else if (string.Equals(action, "PickMobile", StringComparison.OrdinalIgnoreCase))
				{
					this.type |= DirectoryView.Type.Mobile;
					this.viewType = ViewType.DirectoryPicker;
				}
			}
			if (string.IsNullOrEmpty(base.SerializedContainerId))
			{
				if (this.IsRoomView && this.IsPicker && DirectoryAssistance.IsRoomsAddressListAvailable(base.UserContext))
				{
					this.addressBookBase = base.UserContext.AllRoomsAddressBookInfo.ToAddressBookBase();
					return;
				}
				this.addressBookBase = base.UserContext.GlobalAddressListInfo.ToAddressBookBase();
				return;
			}
			else
			{
				if (base.UserContext.GlobalAddressListInfo.Origin == GlobalAddressListInfo.GalOrigin.DefaultGlobalAddressList)
				{
					this.addressBookBase = DirectoryAssistance.FindAddressBook(base.SerializedContainerId, base.UserContext);
					return;
				}
				if (base.UserContext.GlobalAddressListInfo.Id.Equals(DirectoryAssistance.ParseADObjectId(base.SerializedContainerId)))
				{
					this.addressBookBase = base.UserContext.GlobalAddressListInfo.ToAddressBookBase();
					return;
				}
				throw new OwaInvalidRequestException("Address Book Serialized Id is unsupported " + base.SerializedContainerId);
			}
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000E0F0A File Offset: 0x000DF10A
		internal static void RenderSecondaryNavigation(TextWriter output, UserContext userContext)
		{
			DirectoryView.RenderSecondaryNavigation(output, userContext, false);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000E0F14 File Offset: 0x000DF114
		internal static void RenderSecondaryNavigation(TextWriter output, UserContext userContext, bool isRoomPicker)
		{
			SecondaryNavigationDirectoryList secondaryNavigationDirectoryList = SecondaryNavigationDirectoryList.CreateCondensedDirectoryList(userContext, isRoomPicker);
			secondaryNavigationDirectoryList.Render(output);
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x000E0F30 File Offset: 0x000DF130
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x000E0F38 File Offset: 0x000DF138
		public override SanitizedHtmlString Title
		{
			get
			{
				return new SanitizedHtmlString(this.AddressListName);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x000E0F45 File Offset: 0x000DF145
		public override string PageType
		{
			get
			{
				return "DirectoryView";
			}
		}

		// Token: 0x04001BAC RID: 7084
		private static FastEnumParser elementClassParser = new FastEnumParser(typeof(DirectoryView.ElementClass), true);

		// Token: 0x04001BAD RID: 7085
		private DirectoryView.Type type;

		// Token: 0x04001BAE RID: 7086
		private ViewType viewType = ViewType.DirectoryBrowser;

		// Token: 0x04001BAF RID: 7087
		private AddressBookViewState addressBookViewState;

		// Token: 0x04001BB0 RID: 7088
		private AddressBookBase addressBookBase;

		// Token: 0x04001BB1 RID: 7089
		private AddressBookContextMenu contextMenu;

		// Token: 0x04001BB2 RID: 7090
		private string[] externalScriptFiles = new string[]
		{
			"uview.js",
			"vlv.js"
		};

		// Token: 0x0200044A RID: 1098
		private enum ElementClass
		{
			// Token: 0x04001BB4 RID: 7092
			Recipients,
			// Token: 0x04001BB5 RID: 7093
			Rooms
		}

		// Token: 0x0200044B RID: 1099
		[Flags]
		private enum Type
		{
			// Token: 0x04001BB7 RID: 7095
			None = 0,
			// Token: 0x04001BB8 RID: 7096
			Picker = 1,
			// Token: 0x04001BB9 RID: 7097
			Rooms = 2,
			// Token: 0x04001BBA RID: 7098
			PaaPicker = 4,
			// Token: 0x04001BBB RID: 7099
			Mobile = 8
		}
	}
}
