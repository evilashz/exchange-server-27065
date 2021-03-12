using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006DA RID: 1754
	[Serializable]
	public sealed class SystemAddressList : AddressListBase
	{
		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x06005138 RID: 20792 RVA: 0x0012CA34 File Offset: 0x0012AC34
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SystemAddressList.schema;
			}
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x0012CA3B File Offset: 0x0012AC3B
		public SystemAddressList()
		{
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x0012CA43 File Offset: 0x0012AC43
		public SystemAddressList(AddressBookBase dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x0600513B RID: 20795 RVA: 0x0012CA4C File Offset: 0x0012AC4C
		public static string[] AddressLists
		{
			get
			{
				return new string[]
				{
					"All Recipients(VLV)",
					"All Mailboxes(VLV)",
					"All Mail Users(VLV)",
					"All Contacts(VLV)",
					"All Groups(VLV)",
					"Mailboxes(VLV)",
					"Groups(VLV)",
					"TeamMailboxes(VLV)",
					"GroupMailboxes(VLV)",
					"PublicFolderMailboxes(VLV)",
					"MailPublicFolders(VLV)"
				};
			}
		}

		// Token: 0x04003708 RID: 14088
		public const string AllRecipients = "All Recipients(VLV)";

		// Token: 0x04003709 RID: 14089
		public const string AllMailboxes = "All Mailboxes(VLV)";

		// Token: 0x0400370A RID: 14090
		public const string AllMailUsers = "All Mail Users(VLV)";

		// Token: 0x0400370B RID: 14091
		public const string AllContacts = "All Contacts(VLV)";

		// Token: 0x0400370C RID: 14092
		public const string AllGroups = "All Groups(VLV)";

		// Token: 0x0400370D RID: 14093
		public const string Mailboxes = "Mailboxes(VLV)";

		// Token: 0x0400370E RID: 14094
		public const string Groups = "Groups(VLV)";

		// Token: 0x0400370F RID: 14095
		public const string TeamMailboxes = "TeamMailboxes(VLV)";

		// Token: 0x04003710 RID: 14096
		public const string PublicFolderMailboxes = "PublicFolderMailboxes(VLV)";

		// Token: 0x04003711 RID: 14097
		public const string MailPublicFolders = "MailPublicFolders(VLV)";

		// Token: 0x04003712 RID: 14098
		public const string GroupMailboxes = "GroupMailboxes(VLV)";

		// Token: 0x04003713 RID: 14099
		private static SystemAddressListSchema schema = ObjectSchema.GetInstance<SystemAddressListSchema>();

		// Token: 0x04003714 RID: 14100
		public static readonly ADObjectId RdnSystemAddressListContainerToOrganization = new ADObjectId("CN=All System Address Lists,CN=Address Lists Container");
	}
}
