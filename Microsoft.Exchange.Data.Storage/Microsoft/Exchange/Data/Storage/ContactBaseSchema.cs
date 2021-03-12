using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C47 RID: 3143
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContactBaseSchema : ItemSchema
	{
		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x06006F31 RID: 28465 RVA: 0x001DE56B File Offset: 0x001DC76B
		public new static ContactBaseSchema Instance
		{
			get
			{
				if (ContactBaseSchema.instance == null)
				{
					ContactBaseSchema.instance = new ContactBaseSchema();
				}
				return ContactBaseSchema.instance;
			}
		}

		// Token: 0x06006F32 RID: 28466 RVA: 0x001DE583 File Offset: 0x001DC783
		protected override void CoreObjectUpdateAllAttachmentsHidden(CoreItem coreItem)
		{
			Contact.CoreObjectUpdateAllAttachmentsHidden(coreItem);
		}

		// Token: 0x0400423F RID: 16959
		private static ContactBaseSchema instance = null;

		// Token: 0x04004240 RID: 16960
		[Autoload]
		public static readonly StorePropertyDefinition FileAs = InternalSchema.FileAsString;

		// Token: 0x04004241 RID: 16961
		[DetectCodepage]
		[Autoload]
		public static readonly StorePropertyDefinition DisplayNameFirstLast = InternalSchema.DisplayNameFirstLast;

		// Token: 0x04004242 RID: 16962
		[Autoload]
		[DetectCodepage]
		public static readonly StorePropertyDefinition DisplayNameLastFirst = InternalSchema.DisplayNameLastFirst;

		// Token: 0x04004243 RID: 16963
		[DetectCodepage]
		[Autoload]
		public static readonly StorePropertyDefinition DisplayNamePriority = InternalSchema.DisplayNamePriority;

		// Token: 0x04004244 RID: 16964
		public static readonly StorePropertyDefinition AnrViewParticipant = InternalSchema.AnrViewParticipant;

		// Token: 0x04004245 RID: 16965
		public static readonly StorePropertyDefinition GALObjectId = InternalSchema.GALObjectId;

		// Token: 0x04004246 RID: 16966
		public static readonly StorePropertyDefinition GALRecipientType = InternalSchema.GALRecipientType;

		// Token: 0x04004247 RID: 16967
		public static readonly StorePropertyDefinition GALHiddenFromAddressListsEnabled = InternalSchema.GALHiddenFromAddressListsEnabled;

		// Token: 0x04004248 RID: 16968
		public static readonly StorePropertyDefinition GALCurrentSpeechGrammarVersion = InternalSchema.GALCurrentSpeechGrammarVersion;

		// Token: 0x04004249 RID: 16969
		public static readonly StorePropertyDefinition GALPreviousSpeechGrammarVersion = InternalSchema.GALPreviousSpeechGrammarVersion;

		// Token: 0x0400424A RID: 16970
		public static readonly StorePropertyDefinition GALCurrentUMDtmfMapVersion = InternalSchema.GALCurrentUMDtmfMapVersion;

		// Token: 0x0400424B RID: 16971
		public static readonly StorePropertyDefinition GALPreviousUMDtmfMapVersion = InternalSchema.GALPreviousUMDtmfMapVersion;

		// Token: 0x0400424C RID: 16972
		public static readonly StorePropertyDefinition GALSpeechNormalizedNamesForDisplayName = InternalSchema.GALSpeechNormalizedNamesForDisplayName;

		// Token: 0x0400424D RID: 16973
		public static readonly StorePropertyDefinition GALSpeechNormalizedNamesForPhoneticDisplayName = InternalSchema.GALSpeechNormalizedNamesForPhoneticDisplayName;
	}
}
