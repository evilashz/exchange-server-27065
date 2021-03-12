using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001D7 RID: 471
	internal class GroupItemSchema : CacheSchema
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00055DB9 File Offset: 0x00053FB9
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return GroupItemSchema.cachedProperties;
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00055DC0 File Offset: 0x00053FC0
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			ReroutableItemSchema.Set(entry, recipient);
			CacheSchema.Set(GroupItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000AA4 RID: 2724
		public const string SendDeliveryReportsTo = "Microsoft.Exchange.Transport.DirectoryData.SendDeliveryReportsTo";

		// Token: 0x04000AA5 RID: 2725
		public const string SendOofMessageToOriginator = "Microsoft.Exchange.Transport.DirectoryData.SendOofMessageToOriginator";

		// Token: 0x04000AA6 RID: 2726
		public const string ManagedBy = "Microsoft.Exchange.Transport.DirectoryData.ManagedBy";

		// Token: 0x04000AA7 RID: 2727
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(IADDistributionListSchema.SendDeliveryReportsTo, "Microsoft.Exchange.Transport.DirectoryData.SendDeliveryReportsTo"),
			new CachedProperty(ADGroupSchema.SendOofMessageToOriginatorEnabled, "Microsoft.Exchange.Transport.DirectoryData.SendOofMessageToOriginator"),
			new CachedProperty(ADGroupSchema.ManagedBy, "Microsoft.Exchange.Transport.DirectoryData.ManagedBy")
		};
	}
}
