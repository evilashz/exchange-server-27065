using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000815 RID: 2069
	internal class SyncAccountSchema : SyncObjectSchema
	{
		// Token: 0x17002414 RID: 9236
		// (get) Token: 0x06006641 RID: 26177 RVA: 0x001697AD File Offset: 0x001679AD
		public override DirectoryObjectClass DirectoryObjectClass
		{
			get
			{
				return DirectoryObjectClass.Account;
			}
		}

		// Token: 0x040043A4 RID: 17316
		public static SyncPropertyDefinition DisplayName = new SyncPropertyDefinition(ADRecipientSchema.DisplayName, "DisplayName", typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);
	}
}
