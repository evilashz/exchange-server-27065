using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000C0 RID: 192
	internal static class CannedSystemAddressLists
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002C6E3 File Offset: 0x0002A8E3
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0002C6EA File Offset: 0x0002A8EA
		public static Dictionary<string, QueryFilter> RecipientFilters { get; private set; } = new Dictionary<string, QueryFilter>();

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002C6F2 File Offset: 0x0002A8F2
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0002C6F9 File Offset: 0x0002A8F9
		public static Dictionary<string, bool> SystemFlags { get; private set; } = new Dictionary<string, bool>();

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002C704 File Offset: 0x0002A904
		static CannedSystemAddressLists()
		{
			CannedSystemAddressLists.RecipientFilters.Add("All Recipients(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForAllRecipientsAL));
			CannedSystemAddressLists.SystemFlags.Add("All Recipients(VLV)", true);
			CannedSystemAddressLists.RecipientFilters.Add("All Mailboxes(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForAllMailboxesAL));
			CannedSystemAddressLists.SystemFlags.Add("All Mailboxes(VLV)", true);
			CannedSystemAddressLists.RecipientFilters.Add("All Groups(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForAllGroupsAL));
			CannedSystemAddressLists.SystemFlags.Add("All Groups(VLV)", true);
			CannedSystemAddressLists.RecipientFilters.Add("All Mail Users(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForAllMailUsersAL));
			CannedSystemAddressLists.SystemFlags.Add("All Mail Users(VLV)", true);
			CannedSystemAddressLists.RecipientFilters.Add("All Contacts(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForAllContactsAL));
			CannedSystemAddressLists.SystemFlags.Add("All Contacts(VLV)", true);
			CannedSystemAddressLists.RecipientFilters.Add("Groups(VLV)", CannedSystemAddressLists.RecipientFilters["All Groups(VLV)"]);
			CannedSystemAddressLists.SystemFlags.Add("Groups(VLV)", false);
			CannedSystemAddressLists.RecipientFilters.Add("Mailboxes(VLV)", CannedSystemAddressLists.RecipientFilters["All Mailboxes(VLV)"]);
			CannedSystemAddressLists.SystemFlags.Add("Mailboxes(VLV)", false);
			CannedSystemAddressLists.RecipientFilters.Add("TeamMailboxes(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForAllTeamMailboxesAL));
			CannedSystemAddressLists.SystemFlags.Add("TeamMailboxes(VLV)", true);
			CannedSystemAddressLists.RecipientFilters.Add("PublicFolderMailboxes(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForAllPublicFolderMailboxesAL));
			CannedSystemAddressLists.SystemFlags.Add("PublicFolderMailboxes(VLV)", true);
			CannedSystemAddressLists.RecipientFilters.Add("MailPublicFolders(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForMailPublicFoldersAL));
			CannedSystemAddressLists.SystemFlags.Add("MailPublicFolders(VLV)", true);
			CannedSystemAddressLists.RecipientFilters.Add("GroupMailboxes(VLV)", CannedSystemAddressLists.GetOrFilter(CannedSystemAddressLists.RecipientTypeDetailsForGroupMailboxesAL));
			CannedSystemAddressLists.SystemFlags.Add("GroupMailboxes(VLV)", true);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002CAEC File Offset: 0x0002ACEC
		public static bool GetFilterByAddressList(string addressList, out QueryFilter queryFilter)
		{
			return CannedSystemAddressLists.RecipientFilters.TryGetValue(addressList, out queryFilter);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002CAFC File Offset: 0x0002ACFC
		private static QueryFilter GetOrFilter(RecipientTypeDetails[] recipientTypeDetails)
		{
			List<QueryFilter> list = new List<QueryFilter>(recipientTypeDetails.Length);
			foreach (RecipientTypeDetails recipientTypeDetails2 in recipientTypeDetails)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, recipientTypeDetails2));
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x040003B3 RID: 947
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForAllRecipientsAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.RoomMailbox,
			RecipientTypeDetails.LinkedRoomMailbox,
			RecipientTypeDetails.EquipmentMailbox,
			RecipientTypeDetails.LegacyMailbox,
			RecipientTypeDetails.LinkedMailbox,
			RecipientTypeDetails.UserMailbox,
			RecipientTypeDetails.MailContact,
			RecipientTypeDetails.DynamicDistributionGroup,
			RecipientTypeDetails.MailForestContact,
			RecipientTypeDetails.MailNonUniversalGroup,
			RecipientTypeDetails.MailUniversalDistributionGroup,
			RecipientTypeDetails.MailUniversalSecurityGroup,
			RecipientTypeDetails.RoomList,
			RecipientTypeDetails.MailUser,
			RecipientTypeDetails.DiscoveryMailbox,
			RecipientTypeDetails.PublicFolder,
			RecipientTypeDetails.TeamMailbox,
			RecipientTypeDetails.SharedMailbox,
			(RecipientTypeDetails)((ulong)int.MinValue),
			RecipientTypeDetails.RemoteRoomMailbox,
			RecipientTypeDetails.RemoteEquipmentMailbox,
			RecipientTypeDetails.RemoteTeamMailbox,
			RecipientTypeDetails.RemoteSharedMailbox
		};

		// Token: 0x040003B4 RID: 948
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForAllMailboxesAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.RoomMailbox,
			RecipientTypeDetails.LinkedRoomMailbox,
			RecipientTypeDetails.EquipmentMailbox,
			RecipientTypeDetails.LegacyMailbox,
			RecipientTypeDetails.LinkedMailbox,
			RecipientTypeDetails.UserMailbox,
			RecipientTypeDetails.DiscoveryMailbox,
			RecipientTypeDetails.SharedMailbox
		};

		// Token: 0x040003B5 RID: 949
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForAllGroupsAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.MailNonUniversalGroup,
			RecipientTypeDetails.MailUniversalDistributionGroup,
			RecipientTypeDetails.MailUniversalSecurityGroup,
			RecipientTypeDetails.RoomList
		};

		// Token: 0x040003B6 RID: 950
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForAllMailUsersAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.MailUser
		};

		// Token: 0x040003B7 RID: 951
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForAllContactsAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.MailContact,
			RecipientTypeDetails.MailForestContact
		};

		// Token: 0x040003B8 RID: 952
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForAllTeamMailboxesAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.TeamMailbox
		};

		// Token: 0x040003B9 RID: 953
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForAllPublicFolderMailboxesAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.PublicFolderMailbox
		};

		// Token: 0x040003BA RID: 954
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForMailPublicFoldersAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.PublicFolder
		};

		// Token: 0x040003BB RID: 955
		public static readonly RecipientTypeDetails[] RecipientTypeDetailsForGroupMailboxesAL = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.GroupMailbox
		};
	}
}
