using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C53 RID: 3155
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreItemSchema : CoreObjectSchema
	{
		// Token: 0x06006F61 RID: 28513 RVA: 0x001DF997 File Offset: 0x001DDB97
		private CoreItemSchema()
		{
		}

		// Token: 0x17001E0E RID: 7694
		// (get) Token: 0x06006F62 RID: 28514 RVA: 0x001DF99F File Offset: 0x001DDB9F
		public new static CoreItemSchema Instance
		{
			get
			{
				if (CoreItemSchema.instance == null)
				{
					CoreItemSchema.instance = new CoreItemSchema();
				}
				return CoreItemSchema.instance;
			}
		}

		// Token: 0x04004360 RID: 17248
		private static CoreItemSchema instance = null;

		// Token: 0x04004361 RID: 17249
		public static readonly StorePropertyDefinition ClientSubmittedSecurely = InternalSchema.ClientSubmittedSecurely;

		// Token: 0x04004362 RID: 17250
		[Autoload]
		public static readonly StorePropertyDefinition Codepage = InternalSchema.Codepage;

		// Token: 0x04004363 RID: 17251
		[Autoload]
		internal static readonly StorePropertyDefinition DavSubmitData = InternalSchema.DavSubmitData;

		// Token: 0x04004364 RID: 17252
		[Autoload]
		public static readonly StorePropertyDefinition Flags = InternalSchema.Flags;

		// Token: 0x04004365 RID: 17253
		[Autoload]
		internal static readonly StorePropertyDefinition IsResend = InternalSchema.IsResend;

		// Token: 0x04004366 RID: 17254
		[Autoload]
		internal static readonly StorePropertyDefinition NeedSpecialRecipientProcessing = InternalSchema.NeedSpecialRecipientProcessing;

		// Token: 0x04004367 RID: 17255
		[Autoload]
		public static readonly StorePropertyDefinition Id = InternalSchema.ItemId;

		// Token: 0x04004368 RID: 17256
		[Autoload]
		public static readonly StorePropertyDefinition ItemClass = InternalSchema.ItemClass;

		// Token: 0x04004369 RID: 17257
		[Autoload]
		public static readonly StorePropertyDefinition LinkedUrl = InternalSchema.LinkedUrl;

		// Token: 0x0400436A RID: 17258
		[Autoload]
		public static readonly StorePropertyDefinition LinkedId = InternalSchema.LinkedId;

		// Token: 0x0400436B RID: 17259
		[Autoload]
		public static readonly StorePropertyDefinition MapiHasAttachment = InternalSchema.MapiHasAttachment;

		// Token: 0x0400436C RID: 17260
		[Autoload]
		public static readonly StorePropertyDefinition MessageInConflict = InternalSchema.MessageInConflict;

		// Token: 0x0400436D RID: 17261
		[Autoload]
		public static readonly StorePropertyDefinition MessageStatus = InternalSchema.MessageStatus;

		// Token: 0x0400436E RID: 17262
		[Autoload]
		public static readonly StorePropertyDefinition NativeBodyInfo = InternalSchema.NativeBodyInfo;

		// Token: 0x0400436F RID: 17263
		[Autoload]
		public static readonly StorePropertyDefinition NormalizedSubject = InternalSchema.NormalizedSubject;

		// Token: 0x04004370 RID: 17264
		[Autoload]
		public static readonly StorePropertyDefinition PropertyExistenceTracker = InternalSchema.PropertyExistenceTracker;

		// Token: 0x04004371 RID: 17265
		[Autoload]
		public static readonly StorePropertyDefinition FavLevelMask = InternalSchema.FavLevelMask;

		// Token: 0x04004372 RID: 17266
		[Autoload]
		public static readonly StorePropertyDefinition MapiSensitivity = InternalSchema.MapiSensitivity;

		// Token: 0x04004373 RID: 17267
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition ReceivedTime = InternalSchema.ReceivedTime;

		// Token: 0x04004374 RID: 17268
		public static readonly StorePropertyDefinition RenewTime = InternalSchema.RenewTime;

		// Token: 0x04004375 RID: 17269
		public static readonly StorePropertyDefinition ReceivedOrRenewTime = InternalSchema.ReceivedOrRenewTime;

		// Token: 0x04004376 RID: 17270
		public static readonly StorePropertyDefinition RichContent = InternalSchema.RichContent;

		// Token: 0x04004377 RID: 17271
		public static readonly StorePropertyDefinition MailboxGuid = InternalSchema.MailboxGuidGuid;

		// Token: 0x04004378 RID: 17272
		[Autoload]
		public static readonly StorePropertyDefinition Size = InternalSchema.Size;

		// Token: 0x04004379 RID: 17273
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Subject = InternalSchema.Subject;

		// Token: 0x0400437A RID: 17274
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition SubjectPrefix = InternalSchema.SubjectPrefix;

		// Token: 0x0400437B RID: 17275
		public static readonly PropertyTagPropertyDefinition XMsExchOrganizationAVStampMailbox = InternalSchema.XMsExchOrganizationAVStampMailbox;

		// Token: 0x0400437C RID: 17276
		internal static readonly StorePropertyDefinition XMsExchOrganizationOriginalClientIPAddress = InternalSchema.XMsExchOrganizationOriginalClientIPAddress;

		// Token: 0x0400437D RID: 17277
		internal static readonly StorePropertyDefinition XMsExchOrganizationOriginalServerIPAddress = InternalSchema.XMsExchOrganizationOriginalServerIPAddress;

		// Token: 0x0400437E RID: 17278
		internal static readonly StorePropertyDefinition AnnotationToken = InternalSchema.AnnotationToken;

		// Token: 0x0400437F RID: 17279
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition ReadCnNew = InternalSchema.ReadCnNew;
	}
}
