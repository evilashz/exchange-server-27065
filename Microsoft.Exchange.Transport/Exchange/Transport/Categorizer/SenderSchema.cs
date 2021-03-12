using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001F9 RID: 505
	internal class SenderSchema : CacheSchema
	{
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x0005BCEE File Offset: 0x00059EEE
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return SenderSchema.cachedProperties;
			}
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0005BCF5 File Offset: 0x00059EF5
		public static void Set(ADRawEntry entry, TransportMailItem mailItem)
		{
			CacheSchema.Set(SenderSchema.cachedProperties, entry, mailItem);
		}

		// Token: 0x04000B48 RID: 2888
		public const string RecipientType = "Microsoft.Exchange.Transport.DirectoryData.RecipientType";

		// Token: 0x04000B49 RID: 2889
		public const string Id = "Microsoft.Exchange.Transport.DirectoryData.Sender.Id";

		// Token: 0x04000B4A RID: 2890
		public const string DistinguishedName = "Microsoft.Exchange.Transport.DirectoryData.Sender.DistinguishedName";

		// Token: 0x04000B4B RID: 2891
		public const string MaxSendSize = "Microsoft.Exchange.Transport.DirectoryData.Sender.MaxSendSize";

		// Token: 0x04000B4C RID: 2892
		public const string RecipientLimits = "Microsoft.Exchange.Transport.DirectoryData.Sender.RecipientLimits";

		// Token: 0x04000B4D RID: 2893
		public const string ExternalOofOptions = "Microsoft.Exchange.Transport.DirectoryData.Sender.ExternalOofOptions";

		// Token: 0x04000B4E RID: 2894
		public const string Database = "Microsoft.Exchange.Transport.DirectoryData.Database";

		// Token: 0x04000B4F RID: 2895
		public const string ExternalEmailAddress = "Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress";

		// Token: 0x04000B50 RID: 2896
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADRecipientSchema.RecipientType, "Microsoft.Exchange.Transport.DirectoryData.RecipientType"),
			new CachedProperty(ADObjectSchema.Id, "Microsoft.Exchange.Transport.DirectoryData.Sender.Id"),
			new CachedProperty(ADObjectSchema.DistinguishedName, "Microsoft.Exchange.Transport.DirectoryData.Sender.DistinguishedName"),
			new CachedProperty(ADRecipientSchema.MaxSendSize, "Microsoft.Exchange.Transport.DirectoryData.Sender.MaxSendSize"),
			new CachedProperty(ADRecipientSchema.RecipientLimits, "Microsoft.Exchange.Transport.DirectoryData.Sender.RecipientLimits"),
			new CachedProperty(IADMailStorageSchema.ExternalOofOptions, "Microsoft.Exchange.Transport.DirectoryData.Sender.ExternalOofOptions"),
			new CachedProperty(IADMailStorageSchema.Database, "Microsoft.Exchange.Transport.DirectoryData.Database"),
			new CachedProperty(ADRecipientSchema.ExternalEmailAddress, "Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress")
		};
	}
}
