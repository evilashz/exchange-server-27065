using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001E8 RID: 488
	internal class PublicFolderItemSchema : CacheSchema
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x000589AB File Offset: 0x00056BAB
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return PublicFolderItemSchema.cachedProperties;
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000589B2 File Offset: 0x00056BB2
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			ForwardableItemSchema.Set(entry, recipient);
			CacheSchema.Set(PublicFolderItemSchema.cachedProperties, entry, recipient);
			if (PublicFolderItem.IsRemoteRecipient(recipient))
			{
				ContactItemSchema.Set(entry, recipient);
				recipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.IsRemoteRecipient", true);
			}
		}

		// Token: 0x04000AD9 RID: 2777
		public const string IsOneOffRecipient = "Microsoft.Exchange.Transport.IsOneOffRecipient";

		// Token: 0x04000ADA RID: 2778
		public const string IsRemoteRecipient = "Microsoft.Exchange.Transport.IsRemoteRecipient";

		// Token: 0x04000ADB RID: 2779
		public const string EntryId = "Microsoft.Exchange.Transport.DirectoryData.EntryId";

		// Token: 0x04000ADC RID: 2780
		public const string ContentMailbox = "Microsoft.Exchange.Transport.DirectoryData.ContentMailbox";

		// Token: 0x04000ADD RID: 2781
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADPublicFolderSchema.EntryId, "Microsoft.Exchange.Transport.DirectoryData.EntryId"),
			new CachedProperty(ADRecipientSchema.DefaultPublicFolderMailbox, "Microsoft.Exchange.Transport.DirectoryData.ContentMailbox"),
			new CachedProperty(ADRecipientSchema.ExternalEmailAddress, "Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress"),
			new CachedProperty(ADRecipientSchema.EmailAddresses, "Microsoft.Exchange.Transport.DirectoryData.EmailAddresses")
		};
	}
}
