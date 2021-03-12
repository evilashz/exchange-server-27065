using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001ED RID: 493
	internal class ReroutableItemSchema : CacheSchema
	{
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000590BB File Offset: 0x000572BB
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return ReroutableItemSchema.cachedProperties;
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000590C2 File Offset: 0x000572C2
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			RestrictedItemSchema.Set(entry, recipient);
			CacheSchema.Set(ReroutableItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000AE1 RID: 2785
		public const string HomeMtaServerId = "Microsoft.Exchange.Transport.DirectoryData.HomeMtaServerId";

		// Token: 0x04000AE2 RID: 2786
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADGroupSchema.HomeMtaServerId, "Microsoft.Exchange.Transport.DirectoryData.HomeMtaServerId")
		};
	}
}
