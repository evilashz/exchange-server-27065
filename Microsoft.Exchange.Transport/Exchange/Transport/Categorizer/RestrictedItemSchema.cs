using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001F8 RID: 504
	internal class RestrictedItemSchema : CacheSchema
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0005BB49 File Offset: 0x00059D49
		internal static CachedProperty[] CachedProperties
		{
			get
			{
				return RestrictedItemSchema.cachedProperties;
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0005BB50 File Offset: 0x00059D50
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			DirectoryItemSchema.Set(entry, recipient);
			CacheSchema.Set(RestrictedItemSchema.cachedProperties, entry, recipient);
		}

		// Token: 0x04000B3A RID: 2874
		public const string AcceptMessagesOnlyFrom = "Microsoft.Exchange.Transport.DirectoryData.AcceptMessagesOnlyFrom";

		// Token: 0x04000B3B RID: 2875
		public const string AcceptMessagesOnlyFromDLMembers = "Microsoft.Exchange.Transport.DirectoryData.AcceptMessagesOnlyFromDLMembers";

		// Token: 0x04000B3C RID: 2876
		public const string RejectMessagesFrom = "Microsoft.Exchange.Transport.DirectoryData.RejectMessagesFrom";

		// Token: 0x04000B3D RID: 2877
		public const string RejectMessagesFromDLMembers = "Microsoft.Exchange.Transport.DirectoryData.RejectMessagesFromDLMembers";

		// Token: 0x04000B3E RID: 2878
		public const string BypassModerationFrom = "Microsoft.Exchange.Transport.DirectoryData.BypassModerationFrom";

		// Token: 0x04000B3F RID: 2879
		public const string BypassModerationFromDLMembers = "Microsoft.Exchange.Transport.DirectoryData.BypassModerationFromDLMembers";

		// Token: 0x04000B40 RID: 2880
		public const string RequireAllSendersAreAuthenticated = "Microsoft.Exchange.Transport.DirectoryData.RequireAllSendersAreAuthenticated";

		// Token: 0x04000B41 RID: 2881
		public const string MaxReceiveSize = "Microsoft.Exchange.Transport.DirectoryData.MaxReceiveSize";

		// Token: 0x04000B42 RID: 2882
		public const string ModerationEnabled = "Microsoft.Exchange.Transport.DirectoryData.ModerationEnabled";

		// Token: 0x04000B43 RID: 2883
		public const string ModeratedBy = "Microsoft.Exchange.Transport.DirectoryData.ModeratedBy";

		// Token: 0x04000B44 RID: 2884
		public const string ArbitrationMailbox = "Microsoft.Exchange.Transport.DirectoryData.ArbitrationMailbox";

		// Token: 0x04000B45 RID: 2885
		public const string SendModerationNotifications = "Microsoft.Exchange.Transport.DirectoryData.SendModerationNotifications";

		// Token: 0x04000B46 RID: 2886
		public const string BypassNestedModerationEnabled = "Microsoft.Exchange.Transport.DirectoryData.BypassNestedModerationEnabled";

		// Token: 0x04000B47 RID: 2887
		private static CachedProperty[] cachedProperties = new CachedProperty[]
		{
			new CachedProperty(ADRecipientSchema.AcceptMessagesOnlyFrom, "Microsoft.Exchange.Transport.DirectoryData.AcceptMessagesOnlyFrom"),
			new CachedProperty(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers, "Microsoft.Exchange.Transport.DirectoryData.AcceptMessagesOnlyFromDLMembers"),
			new CachedProperty(ADRecipientSchema.RejectMessagesFrom, "Microsoft.Exchange.Transport.DirectoryData.RejectMessagesFrom"),
			new CachedProperty(ADRecipientSchema.RejectMessagesFromDLMembers, "Microsoft.Exchange.Transport.DirectoryData.RejectMessagesFromDLMembers"),
			new CachedProperty(ADRecipientSchema.RequireAllSendersAreAuthenticated, "Microsoft.Exchange.Transport.DirectoryData.RequireAllSendersAreAuthenticated"),
			new CachedProperty(ADRecipientSchema.MaxReceiveSize, "Microsoft.Exchange.Transport.DirectoryData.MaxReceiveSize"),
			new CachedProperty(ADRecipientSchema.ModerationEnabled, "Microsoft.Exchange.Transport.DirectoryData.ModerationEnabled"),
			new CachedProperty(ADRecipientSchema.ModeratedBy, "Microsoft.Exchange.Transport.DirectoryData.ModeratedBy"),
			new CachedProperty(ADRecipientSchema.ArbitrationMailbox, "Microsoft.Exchange.Transport.DirectoryData.ArbitrationMailbox"),
			new CachedProperty(ADRecipientSchema.BypassModerationFrom, "Microsoft.Exchange.Transport.DirectoryData.BypassModerationFrom"),
			new CachedProperty(ADRecipientSchema.BypassModerationFromDLMembers, "Microsoft.Exchange.Transport.DirectoryData.BypassModerationFromDLMembers"),
			new CachedProperty(ADRecipientSchema.SendModerationNotifications, "Microsoft.Exchange.Transport.DirectoryData.SendModerationNotifications"),
			new CachedProperty(ADRecipientSchema.BypassNestedModerationEnabled, "Microsoft.Exchange.Transport.DirectoryData.BypassNestedModerationEnabled")
		};
	}
}
