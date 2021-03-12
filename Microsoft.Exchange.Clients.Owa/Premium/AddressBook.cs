using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000437 RID: 1079
	public class AddressBook : NavigationHost, IRegistryOnlyForm
	{
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x000DE762 File Offset: 0x000DC962
		public bool IsRoomPicker
		{
			get
			{
				return this.picker == AddressBook.pickers[3];
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x000DE774 File Offset: 0x000DC974
		public RecipientBlockType RecipientBlockType
		{
			get
			{
				if (this.picker == AddressBook.pickers[8] || this.picker == AddressBook.pickers[9] || this.picker == AddressBook.pickers[12])
				{
					return RecipientBlockType.DL;
				}
				if (this.picker == AddressBook.pickers[14])
				{
					return RecipientBlockType.PDL;
				}
				return RecipientBlockType.None;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x000DE7C5 File Offset: 0x000DC9C5
		public bool IsPersonalAutoAttendantPicker
		{
			get
			{
				return this.picker == AddressBook.pickers[8] || this.picker == AddressBook.pickers[9];
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x000DE7E8 File Offset: 0x000DC9E8
		public bool IsPicker
		{
			get
			{
				return this.picker != null;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x000DE7F6 File Offset: 0x000DC9F6
		protected bool IsMobileNumberPicker
		{
			get
			{
				return this.picker == AddressBook.pickers[12] || this.picker == AddressBook.pickers[11];
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x000DE81C File Offset: 0x000DCA1C
		protected bool IsSingleRecipientWell
		{
			get
			{
				return this.picker == AddressBook.pickers[7] || this.picker == AddressBook.pickers[9] || this.picker == AddressBook.pickers[14] || this.picker == AddressBook.pickers[6] || this.picker == AddressBook.pickers[13];
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x000DE879 File Offset: 0x000DCA79
		protected bool ShouldRenderContactsInSecondaryNavigation
		{
			get
			{
				return this.picker != AddressBook.pickers[7] && this.picker != AddressBook.pickers[9] && this.picker != AddressBook.pickers[15];
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x000DE8AF File Offset: 0x000DCAAF
		protected AddressBookRecipientPicker Picker
		{
			get
			{
				return this.picker;
			}
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000DE8B7 File Offset: 0x000DCAB7
		protected override NavigationModule SelectNavagationModule()
		{
			return NavigationModule.AddressBook;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000DE8BC File Offset: 0x000DCABC
		protected override void OnInit(EventArgs e)
		{
			ExTraceGlobals.ContactsCallTracer.TraceDebug(0L, "AddressBook.OnInit");
			this.DeterminePicker();
			if (this.IsPicker)
			{
				this.lastModuleMappingAction = (this.IsMobileNumberPicker ? "PickMobile" : "Pick");
			}
			base.OnInit(e);
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000DE90C File Offset: 0x000DCB0C
		private void DeterminePicker()
		{
			string action = base.OwaContext.FormsRegistryContext.Action;
			if (action != null)
			{
				object obj = AddressBook.actionParser.Parse(action);
				this.picker = ((obj != null) ? AddressBook.pickers[(int)obj] : null);
			}
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000DE951 File Offset: 0x000DCB51
		protected void RenderJavascriptEncodedContactsFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.ContactsFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x04001B48 RID: 6984
		private const string PickAction = "Pick";

		// Token: 0x04001B49 RID: 6985
		private const string PickMobileAction = "PickMobile";

		// Token: 0x04001B4A RID: 6986
		private const int BaseWidth = 990;

		// Token: 0x04001B4B RID: 6987
		protected const int NavigationPaneWidth = 184;

		// Token: 0x04001B4C RID: 6988
		protected const int VlvMinWidth = 325;

		// Token: 0x04001B4D RID: 6989
		public const int PickerDialogWidth = 990;

		// Token: 0x04001B4E RID: 6990
		public const int BrowseWindowWidth = 990;

		// Token: 0x04001B4F RID: 6991
		public const int WindowHeight = 641;

		// Token: 0x04001B50 RID: 6992
		private static readonly AddressBookRecipientPicker[] pickers = new AddressBookRecipientPicker[]
		{
			AddressBookRecipientPicker.Recipients,
			AddressBookRecipientPicker.Attendees,
			AddressBookRecipientPicker.DistributionListMember,
			AddressBookRecipientPicker.Rooms,
			AddressBookRecipientPicker.FromRecipients,
			AddressBookRecipientPicker.ToRecipients,
			AddressBookRecipientPicker.SendFromRecipients,
			AddressBookRecipientPicker.SelectOtherMailboxRecipient,
			AddressBookRecipientPicker.PersonalAutoAttendantCallers,
			AddressBookRecipientPicker.PersonalAutoAttendantOneCaller,
			AddressBookRecipientPicker.ChatParticipants,
			AddressBookRecipientPicker.ToMobileNumberOrDL,
			AddressBookRecipientPicker.ToMobileNumber,
			AddressBookRecipientPicker.AddBuddy,
			AddressBookRecipientPicker.Filter,
			AddressBookRecipientPicker.UsersAndGroups
		};

		// Token: 0x04001B51 RID: 6993
		private static readonly FastEnumParser actionParser = new FastEnumParser(typeof(AddressBook.Action), true);

		// Token: 0x04001B52 RID: 6994
		private AddressBookRecipientPicker picker;

		// Token: 0x02000438 RID: 1080
		private enum Action
		{
			// Token: 0x04001B54 RID: 6996
			PickRecipients,
			// Token: 0x04001B55 RID: 6997
			PickAttendees,
			// Token: 0x04001B56 RID: 6998
			PickMembers,
			// Token: 0x04001B57 RID: 6999
			PickRooms,
			// Token: 0x04001B58 RID: 7000
			PickFrom,
			// Token: 0x04001B59 RID: 7001
			PickTo,
			// Token: 0x04001B5A RID: 7002
			PickSendFrom,
			// Token: 0x04001B5B RID: 7003
			PickSelectMailbox,
			// Token: 0x04001B5C RID: 7004
			PickPAACallers,
			// Token: 0x04001B5D RID: 7005
			PickPAAOneCaller,
			// Token: 0x04001B5E RID: 7006
			PickParticipants,
			// Token: 0x04001B5F RID: 7007
			PickMobileOrDL,
			// Token: 0x04001B60 RID: 7008
			PickMobile,
			// Token: 0x04001B61 RID: 7009
			PickBuddy,
			// Token: 0x04001B62 RID: 7010
			PickFilter,
			// Token: 0x04001B63 RID: 7011
			PickUsersAndGroups
		}
	}
}
