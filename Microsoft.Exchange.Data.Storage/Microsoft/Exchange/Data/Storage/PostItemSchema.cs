using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CA5 RID: 3237
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PostItemSchema : ItemSchema
	{
		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x060070DC RID: 28892 RVA: 0x001F4D53 File Offset: 0x001F2F53
		public new static PostItemSchema Instance
		{
			get
			{
				if (PostItemSchema.instance == null)
				{
					PostItemSchema.instance = new PostItemSchema();
				}
				return PostItemSchema.instance;
			}
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x001F4D6B File Offset: 0x001F2F6B
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			base.CoreObjectUpdate(coreItem, operation);
			PostItem.CoreObjectUpdateConversationTopic(coreItem);
			PostItem.CoreObjectUpdateDraftFlag(coreItem);
		}

		// Token: 0x04004E72 RID: 20082
		[Autoload]
		public static readonly StorePropertyDefinition Flags = InternalSchema.Flags;

		// Token: 0x04004E73 RID: 20083
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition SenderAddressType = InternalSchema.SenderAddressType;

		// Token: 0x04004E74 RID: 20084
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition SenderDisplayName = InternalSchema.SenderDisplayName;

		// Token: 0x04004E75 RID: 20085
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition SenderEmailAddress = InternalSchema.SenderEmailAddress;

		// Token: 0x04004E76 RID: 20086
		[LegalTracking]
		[Autoload]
		internal static readonly StorePropertyDefinition SenderEntryId = InternalSchema.SenderEntryId;

		// Token: 0x04004E77 RID: 20087
		[Autoload]
		internal static readonly StorePropertyDefinition AutoResponseSuppress = InternalSchema.AutoResponseSuppress;

		// Token: 0x04004E78 RID: 20088
		[LegalTracking]
		internal static readonly StorePropertyDefinition ExpiryTime = InternalSchema.ExpiryTime;

		// Token: 0x04004E79 RID: 20089
		public static readonly StorePropertyDefinition IsDraft = InternalSchema.IsDraft;

		// Token: 0x04004E7A RID: 20090
		public static readonly StorePropertyDefinition MapiHasAttachment = InternalSchema.MapiHasAttachment;

		// Token: 0x04004E7B RID: 20091
		internal static readonly StorePropertyDefinition MapiPriority = InternalSchema.MapiPriority;

		// Token: 0x04004E7C RID: 20092
		internal static readonly StorePropertyDefinition MapiReplyToBlob = InternalSchema.MapiReplyToBlob;

		// Token: 0x04004E7D RID: 20093
		internal static readonly StorePropertyDefinition MapiReplyToNames = InternalSchema.MapiReplyToNames;

		// Token: 0x04004E7E RID: 20094
		public static readonly StorePropertyDefinition MessageDraft = InternalSchema.MessageDraft;

		// Token: 0x04004E7F RID: 20095
		public static readonly StorePropertyDefinition MessageHidden = InternalSchema.MessageHidden;

		// Token: 0x04004E80 RID: 20096
		public static readonly StorePropertyDefinition MessageHighlighted = InternalSchema.MessageHighlighted;

		// Token: 0x04004E81 RID: 20097
		public static readonly StorePropertyDefinition MessageTagged = InternalSchema.MessageTagged;

		// Token: 0x04004E82 RID: 20098
		public static readonly StorePropertyDefinition MID = InternalSchema.MID;

		// Token: 0x04004E83 RID: 20099
		public static readonly StorePropertyDefinition OriginalAuthorName = InternalSchema.OriginalAuthorName;

		// Token: 0x04004E84 RID: 20100
		public static readonly StorePropertyDefinition ReceivedRepresentingAddressType = InternalSchema.ReceivedRepresentingAddressType;

		// Token: 0x04004E85 RID: 20101
		[DetectCodepage]
		public static readonly StorePropertyDefinition ReceivedRepresentingDisplayName = InternalSchema.ReceivedRepresentingDisplayName;

		// Token: 0x04004E86 RID: 20102
		public static readonly StorePropertyDefinition ReceivedRepresentingEmailAddress = InternalSchema.ReceivedRepresentingEmailAddress;

		// Token: 0x04004E87 RID: 20103
		public static readonly StorePropertyDefinition ReceivedRepresentingSearchKey = InternalSchema.ReceivedRepresentingSearchKey;

		// Token: 0x04004E88 RID: 20104
		internal static readonly StorePropertyDefinition TnefCorrelationKey = InternalSchema.TnefCorrelationKey;

		// Token: 0x04004E89 RID: 20105
		private static PostItemSchema instance = null;
	}
}
