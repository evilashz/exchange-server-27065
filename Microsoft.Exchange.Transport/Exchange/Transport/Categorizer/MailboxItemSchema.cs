using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001DE RID: 478
	internal class MailboxItemSchema : CacheSchema
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00057B00 File Offset: 0x00055D00
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return MailboxItemSchema.cachedProperties;
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00057B07 File Offset: 0x00055D07
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			DeliverableItemSchema.Set(entry, recipient);
			CacheSchema.Set(MailboxItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000AC0 RID: 2752
		public const string ServerName = "Microsoft.Exchange.Transport.DirectoryData.ServerName";

		// Token: 0x04000AC1 RID: 2753
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(IADMailStorageSchema.Database, "Microsoft.Exchange.Transport.DirectoryData.Database"),
			new CachedProperty(IADMailStorageSchema.ExchangeGuid, "Microsoft.Exchange.Transport.DirectoryData.ExchangeGuid"),
			new CachedProperty(IADMailStorageSchema.ServerName, "Microsoft.Exchange.Transport.DirectoryData.ServerName"),
			new CachedProperty(ADRecipientSchema.OpenDomainRoutingDisabled, "Microsoft.Exchange.Transport.OpenDomainRoutingDisabled"),
			new CachedProperty(ADRecipientSchema.AddressBookPolicy, "Microsoft.Exchange.Transport.MailRecipient.AddressBookPolicy"),
			new CachedProperty(ADRecipientSchema.DisplayName, "Microsoft.Exchange.Transport.MailRecipient.DisplayName")
		};
	}
}
