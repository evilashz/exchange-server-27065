using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200040E RID: 1038
	internal class SecondaryNavigationDirectoryList : SecondaryNavigationList
	{
		// Token: 0x0600257D RID: 9597 RVA: 0x000D8E9C File Offset: 0x000D709C
		public static SecondaryNavigationDirectoryList CreateCondensedDirectoryList(UserContext userContext, bool isRoomPicker)
		{
			SecondaryNavigationDirectoryList secondaryNavigationDirectoryList = new SecondaryNavigationDirectoryList(userContext);
			secondaryNavigationDirectoryList.AddEntry(userContext.GlobalAddressListInfo.DisplayName, userContext.GlobalAddressListInfo.ToBase64String(), !isRoomPicker, false);
			if (DirectoryAssistance.IsRoomsAddressListAvailable(userContext) && userContext.AllRoomsAddressBookInfo != null && !userContext.AllRoomsAddressBookInfo.IsEmpty)
			{
				secondaryNavigationDirectoryList.AddEntry(userContext.AllRoomsAddressBookInfo.DisplayName, userContext.AllRoomsAddressBookInfo.ToBase64String(), isRoomPicker, true);
			}
			return secondaryNavigationDirectoryList;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000D8F10 File Offset: 0x000D7110
		public static SecondaryNavigationDirectoryList CreateExtendedDirectoryList(UserContext userContext)
		{
			SecondaryNavigationDirectoryList secondaryNavigationDirectoryList = new SecondaryNavigationDirectoryList(userContext);
			AddressBookBase[] allAddressBooks = DirectoryAssistance.GetAllAddressBooks(userContext);
			for (int i = 0; i < allAddressBooks.Length; i++)
			{
				if (!string.Equals(allAddressBooks[i].Base64Guid, userContext.GlobalAddressListInfo.ToBase64String(), StringComparison.Ordinal) && (userContext.AllRoomsAddressBookInfo == null || !string.Equals(allAddressBooks[i].Base64Guid, userContext.AllRoomsAddressBookInfo.ToBase64String(), StringComparison.Ordinal)))
				{
					secondaryNavigationDirectoryList.AddEntry(allAddressBooks[i].DisplayName, allAddressBooks[i].Base64Guid, false, false);
				}
			}
			return secondaryNavigationDirectoryList;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000D8F90 File Offset: 0x000D7190
		private SecondaryNavigationDirectoryList(UserContext userContext) : base("divDirLst")
		{
			this.userContext = userContext;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000D8FB0 File Offset: 0x000D71B0
		private static void RenderMoreOrLess(TextWriter output, UserContext userContext, bool moreOrLess, ThemeFileId image)
		{
			output.Write("<div id=\"");
			output.Write(moreOrLess ? "divABMore" : "divABLess");
			output.Write("\" class=\"abMoreLessWrap\" _fMrLs=\"1\"");
			if (!moreOrLess)
			{
				output.Write(" style=\"display:none\"");
			}
			output.Write("><span class=\"abMoreLess\">");
			output.Write(LocalizedStrings.GetHtmlEncoded(moreOrLess ? 1132752106 : -584522130));
			output.Write("</span>&nbsp;");
			userContext.RenderThemeImage(output, image, "abMoreLessImg", new object[0]);
			output.Write("</div>");
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000D9044 File Offset: 0x000D7244
		private void AddEntry(string displayString, string containerId, bool isSelected, bool isRoom)
		{
			SecondaryNavigationDirectoryList.DirectoryListEntryInfo item;
			item.IsSelected = isSelected;
			item.IsRoom = isRoom;
			item.DisplayString = displayString;
			item.ContainerId = containerId;
			this.entries.Add(item);
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x000D907E File Offset: 0x000D727E
		protected override int Count
		{
			get
			{
				return this.entries.Count;
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000D908C File Offset: 0x000D728C
		protected override void RenderEntryOnClickHandler(TextWriter output, int entryIndex)
		{
			Utilities.HtmlEncode("onClkABFld(\"", output);
			Utilities.HtmlEncode(Utilities.JavascriptEncode(this.entries[entryIndex].ContainerId), output);
			Utilities.HtmlEncode("\",\"", output);
			Utilities.HtmlEncode(this.entries[entryIndex].IsRoom ? "Rooms" : "Recipients", output);
			Utilities.HtmlEncode("\")", output);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000D90FB File Offset: 0x000D72FB
		protected override void RenderEntryAttributes(TextWriter output, int entryIndex)
		{
			if (this.entries[entryIndex].IsSelected)
			{
				output.Write("_sel=\"1\"");
			}
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000D911B File Offset: 0x000D731B
		protected override void RenderEntryIcon(TextWriter output, int entryIndex)
		{
			this.userContext.RenderThemeImage(output, ThemeFileId.AddressBook, "snlADImg", new object[0]);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000D9136 File Offset: 0x000D7336
		protected override string GetEntryText(int entryIndex)
		{
			return this.entries[entryIndex].DisplayString;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000D914C File Offset: 0x000D734C
		protected override void RenderFooter(TextWriter output)
		{
			if (this.userContext.IsFeatureEnabled(Feature.AddressLists) && this.userContext.GlobalAddressListInfo.Origin == GlobalAddressListInfo.GalOrigin.DefaultGlobalAddressList)
			{
				SecondaryNavigationDirectoryList.RenderMoreOrLess(output, this.userContext, true, ThemeFileId.Expand);
				SecondaryNavigationDirectoryList.RenderMoreOrLess(output, this.userContext, false, ThemeFileId.Collapse);
				output.Write("<div id=\"divAllAL\" style=\"display:none\"></div>");
			}
		}

		// Token: 0x040019ED RID: 6637
		private UserContext userContext;

		// Token: 0x040019EE RID: 6638
		private List<SecondaryNavigationDirectoryList.DirectoryListEntryInfo> entries = new List<SecondaryNavigationDirectoryList.DirectoryListEntryInfo>();

		// Token: 0x0200040F RID: 1039
		private struct DirectoryListEntryInfo
		{
			// Token: 0x040019EF RID: 6639
			public string DisplayString;

			// Token: 0x040019F0 RID: 6640
			public string ContainerId;

			// Token: 0x040019F1 RID: 6641
			public bool IsSelected;

			// Token: 0x040019F2 RID: 6642
			public bool IsRoom;
		}
	}
}
