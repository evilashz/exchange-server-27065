using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001D4 RID: 468
	internal class ForwardableItemSchema : CacheSchema
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0005590E File Offset: 0x00053B0E
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return ForwardableItemSchema.cachedProperties;
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00055915 File Offset: 0x00053B15
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			MailboxItemSchema.Set(entry, recipient);
			CacheSchema.Set(ForwardableItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000AA1 RID: 2721
		public const string ForwardingAddress = "Microsoft.Exchange.Transport.DirectoryData.ForwardingAddress";

		// Token: 0x04000AA2 RID: 2722
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADRecipientSchema.ForwardingAddress, "Microsoft.Exchange.Transport.DirectoryData.ForwardingAddress"),
			new CachedProperty(IADMailStorageSchema.DeliverToMailboxAndForward, "Microsoft.Exchange.Transport.DirectoryData.DeliverToMailboxAndForward"),
			new CachedProperty(ADRecipientSchema.ThrottlingPolicy, "Microsoft.Exchange.Transport.DirectoryData.ThrottlingPolicy"),
			new CachedProperty(ADRecipientSchema.ForwardingSmtpAddress, "Microsoft.Exchange.Transport.DirectoryData.ForwardingSmtpAddress")
		};
	}
}
