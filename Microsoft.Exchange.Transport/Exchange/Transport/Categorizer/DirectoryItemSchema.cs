using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001D1 RID: 465
	internal abstract class DirectoryItemSchema : CacheSchema
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0005559B File Offset: 0x0005379B
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return DirectoryItemSchema.cachedProperties;
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000555A2 File Offset: 0x000537A2
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			CacheSchema.Set(DirectoryItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000A95 RID: 2709
		public const string RecipientType = "Microsoft.Exchange.Transport.DirectoryData.RecipientType";

		// Token: 0x04000A96 RID: 2710
		public const string IsResource = "Microsoft.Exchange.Transport.DirectoryData.IsResource";

		// Token: 0x04000A97 RID: 2711
		public const string RecipientTypeDetailsRaw = "Microsoft.Exchange.Transport.DirectoryData.RecipientTypeDetailsRaw";

		// Token: 0x04000A98 RID: 2712
		public const string ObjectGuid = "Microsoft.Exchange.Transport.DirectoryData.ObjectGuid";

		// Token: 0x04000A99 RID: 2713
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADRecipientSchema.RecipientType, "Microsoft.Exchange.Transport.DirectoryData.RecipientType"),
			new CachedProperty(ADObjectSchema.Guid, "Microsoft.Exchange.Transport.DirectoryData.ObjectGuid"),
			new CachedProperty(ADRecipientSchema.IsResource, "Microsoft.Exchange.Transport.DirectoryData.IsResource"),
			new CachedProperty(ADRecipientSchema.RecipientTypeDetailsRaw, "Microsoft.Exchange.Transport.DirectoryData.RecipientTypeDetailsRaw")
		};
	}
}
