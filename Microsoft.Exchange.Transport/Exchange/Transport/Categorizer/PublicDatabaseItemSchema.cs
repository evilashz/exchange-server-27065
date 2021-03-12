using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001E6 RID: 486
	internal class PublicDatabaseItemSchema : CacheSchema
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x0005890B File Offset: 0x00056B0B
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return PublicDatabaseItemSchema.cachedProperties;
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00058912 File Offset: 0x00056B12
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			DeliverableItemSchema.Set(entry, recipient);
			CacheSchema.Set(PublicDatabaseItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000AD6 RID: 2774
		public const string DistinguishedName = "Microsoft.Exchange.Transport.DirectoryData.DistinguishedName";

		// Token: 0x04000AD7 RID: 2775
		public const string Id = "Microsoft.Exchange.Transport.DirectoryData.Id";

		// Token: 0x04000AD8 RID: 2776
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADObjectSchema.DistinguishedName, "Microsoft.Exchange.Transport.DirectoryData.DistinguishedName"),
			new CachedProperty(ADObjectSchema.Id, "Microsoft.Exchange.Transport.DirectoryData.Id")
		};
	}
}
