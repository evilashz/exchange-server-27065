using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C6 RID: 198
	internal class ExternalSharingContactsType : ExternalSharingType
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0003A029 File Offset: 0x00038229
		public override string FolderTypeName
		{
			get
			{
				return "ExternalSharingContacts";
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0003A030 File Offset: 0x00038230
		public override PropertyDefinition CounterProperty
		{
			get
			{
				return MailboxSchema.ExternalSharingContactSubscriptionCount;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0003A037 File Offset: 0x00038237
		protected override bool MatchesContainerClass(string containerClass)
		{
			return ObjectClass.IsContactsFolder(containerClass);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0003A03F File Offset: 0x0003823F
		public override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.ContactsFolder;
			}
		}
	}
}
