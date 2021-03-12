using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001CE RID: 462
	internal class DeliverableItemSchema : CacheSchema
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000550D8 File Offset: 0x000532D8
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return DeliverableItemSchema.cachedProperties;
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000550DF File Offset: 0x000532DF
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			RestrictedItemSchema.Set(entry, recipient);
			CacheSchema.Set(DeliverableItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000A8B RID: 2699
		public const string LegacyExchangeDN = "Microsoft.Exchange.Transport.DirectoryData.LegacyExchangeDN";

		// Token: 0x04000A8C RID: 2700
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADRecipientSchema.LegacyExchangeDN, "Microsoft.Exchange.Transport.DirectoryData.LegacyExchangeDN")
		};
	}
}
