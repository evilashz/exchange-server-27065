using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000310 RID: 784
	internal class AddressBookViewState
	{
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x000AC273 File Offset: 0x000AA473
		internal static ReadingPanePosition DefaultReadingPanePosition
		{
			get
			{
				return ReadingPanePosition.Right;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x000AC276 File Offset: 0x000AA476
		internal ReadingPanePosition ReadingPanePosition
		{
			get
			{
				return this.readingPanePosition;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x000AC27E File Offset: 0x000AA47E
		internal bool DefaultMultiLineSetting
		{
			get
			{
				return !this.isRoomView;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x000AC289 File Offset: 0x000AA489
		internal bool IsMultiLine
		{
			get
			{
				return this.isMultiLine;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x000AC291 File Offset: 0x000AA491
		internal bool FindBarOn
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000AC294 File Offset: 0x000AA494
		internal static AddressBookViewState Load(UserContext userContext, bool isPicker, bool isRoomView)
		{
			PropertyDefinition[] propsToReturn = isPicker ? AddressBookViewState.pickerProperties : AddressBookViewState.browseProperties;
			PropertyDefinition propertyDefinition = isPicker ? ViewStateProperties.AddressBookPickerMultiLine : ViewStateProperties.AddressBookLookupMultiLine;
			AddressBookViewState addressBookViewState = new AddressBookViewState();
			addressBookViewState.isRoomView = isRoomView;
			if (userContext.IsWebPartRequest)
			{
				return addressBookViewState;
			}
			using (Folder folder = Folder.Bind(userContext.MailboxSession, DefaultFolderType.Root, propsToReturn))
			{
				addressBookViewState.isMultiLine = Utilities.GetFolderProperty<bool>(folder, propertyDefinition, !isRoomView);
				if (isPicker)
				{
					return addressBookViewState;
				}
				addressBookViewState.readingPanePosition = Utilities.GetFolderProperty<ReadingPanePosition>(folder, ViewStateProperties.AddressBookLookupReadingPanePosition, ReadingPanePosition.Right);
				if (!FolderViewStates.ValidateReadingPanePosition(addressBookViewState.readingPanePosition))
				{
					addressBookViewState.readingPanePosition = ReadingPanePosition.Right;
				}
			}
			return addressBookViewState;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000AC348 File Offset: 0x000AA548
		public static void PersistMultiLineState(UserContext userContext, bool isMultiLine, bool isPicker)
		{
			if (!userContext.IsWebPartRequest)
			{
				PropertyDefinition propertyDefinition = isPicker ? ViewStateProperties.AddressBookPickerMultiLine : ViewStateProperties.AddressBookLookupMultiLine;
				Folder folder = Folder.Bind(userContext.MailboxSession, DefaultFolderType.Root);
				using (folder)
				{
					folder[propertyDefinition] = isMultiLine;
					folder.Save();
				}
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000AC3B0 File Offset: 0x000AA5B0
		public static void PersistReadingPane(UserContext userContext, ReadingPanePosition readingPanePosition)
		{
			if (!userContext.IsWebPartRequest)
			{
				ExTraceGlobals.ContactsTracer.TraceDebug(0L, "AdderssBookViewState.PersistAddressBookReadingPane");
				Folder folder = Folder.Bind(userContext.MailboxSession, DefaultFolderType.Root);
				using (folder)
				{
					folder[ViewStateProperties.AddressBookLookupReadingPanePosition] = readingPanePosition;
					folder.Save();
				}
			}
		}

		// Token: 0x04001623 RID: 5667
		private const bool FindBarOnValue = true;

		// Token: 0x04001624 RID: 5668
		private const ReadingPanePosition ReadingPaneDefaultPosition = ReadingPanePosition.Right;

		// Token: 0x04001625 RID: 5669
		internal const int ViewWidth = 365;

		// Token: 0x04001626 RID: 5670
		internal const int ViewHeight = 250;

		// Token: 0x04001627 RID: 5671
		private static readonly PropertyDefinition[] browseProperties = new PropertyDefinition[]
		{
			ViewStateProperties.AddressBookLookupMultiLine,
			ViewStateProperties.AddressBookLookupReadingPanePosition
		};

		// Token: 0x04001628 RID: 5672
		private static readonly PropertyDefinition[] pickerProperties = new PropertyDefinition[]
		{
			ViewStateProperties.AddressBookPickerMultiLine
		};

		// Token: 0x04001629 RID: 5673
		private bool isMultiLine = true;

		// Token: 0x0400162A RID: 5674
		private ReadingPanePosition readingPanePosition = ReadingPanePosition.Right;

		// Token: 0x0400162B RID: 5675
		private bool isRoomView;
	}
}
