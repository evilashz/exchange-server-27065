using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C9B RID: 3227
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecipientSchema : Schema
	{
		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x060070B1 RID: 28849 RVA: 0x001F30DD File Offset: 0x001F12DD
		public new static RecipientSchema Instance
		{
			get
			{
				if (RecipientSchema.instance == null)
				{
					RecipientSchema.instance = new RecipientSchema();
				}
				return RecipientSchema.instance;
			}
		}

		// Token: 0x04004DD2 RID: 19922
		[Autoload]
		public static readonly StorePropertyDefinition EmailAddress = InternalSchema.EmailAddress;

		// Token: 0x04004DD3 RID: 19923
		[Autoload]
		public static readonly StorePropertyDefinition EmailAddrType = InternalSchema.AddrType;

		// Token: 0x04004DD4 RID: 19924
		[DetectCodepage]
		public static readonly StorePropertyDefinition EmailDisplayName = InternalSchema.DisplayName;

		// Token: 0x04004DD5 RID: 19925
		[Autoload]
		public static readonly StorePropertyDefinition RecipientFlags = InternalSchema.RecipientFlags;

		// Token: 0x04004DD6 RID: 19926
		public static readonly StorePropertyDefinition RecipientTrackStatus = InternalSchema.RecipientTrackStatus;

		// Token: 0x04004DD7 RID: 19927
		[Autoload]
		public static readonly StorePropertyDefinition RecipientTrackStatusTime = InternalSchema.RecipientTrackStatusTime;

		// Token: 0x04004DD8 RID: 19928
		public static readonly StorePropertyDefinition RecipientType = InternalSchema.RecipientType;

		// Token: 0x04004DD9 RID: 19929
		[Autoload]
		internal static readonly StorePropertyDefinition RowId = InternalSchema.RowId;

		// Token: 0x04004DDA RID: 19930
		internal static readonly StorePropertyDefinition RecipientEntryId = InternalSchema.RecipientEntryId;

		// Token: 0x04004DDB RID: 19931
		internal static readonly StorePropertyDefinition DisplayTypeEx = InternalSchema.DisplayTypeEx;

		// Token: 0x04004DDC RID: 19932
		public static readonly StorePropertyDefinition DisplayType = InternalSchema.DisplayType;

		// Token: 0x04004DDD RID: 19933
		public static readonly StorePropertyDefinition DisplayName7Bit = InternalSchema.DisplayName7Bit;

		// Token: 0x04004DDE RID: 19934
		[Autoload]
		public static readonly StorePropertyDefinition EntryId = InternalSchema.EntryId;

		// Token: 0x04004DDF RID: 19935
		[Autoload]
		public static readonly StorePropertyDefinition SearchKey = InternalSchema.SearchKey;

		// Token: 0x04004DE0 RID: 19936
		[Autoload]
		public static readonly StorePropertyDefinition SmtpAddress = InternalSchema.SmtpAddress;

		// Token: 0x04004DE1 RID: 19937
		[Autoload]
		public static readonly StorePropertyDefinition SendRichInfo = InternalSchema.SendRichInfo;

		// Token: 0x04004DE2 RID: 19938
		[Autoload]
		public static readonly StorePropertyDefinition Responsibility = InternalSchema.Responsibility;

		// Token: 0x04004DE3 RID: 19939
		[Autoload]
		public static readonly StorePropertyDefinition SipUri = InternalSchema.SipUri;

		// Token: 0x04004DE4 RID: 19940
		[Autoload]
		public static readonly StorePropertyDefinition ParticipantSID = InternalSchema.ParticipantSID;

		// Token: 0x04004DE5 RID: 19941
		[Autoload]
		public static readonly StorePropertyDefinition ParticipantGuid = InternalSchema.ParticipantGuid;

		// Token: 0x04004DE6 RID: 19942
		public static readonly StorePropertyDefinition TransmittableDisplayName = InternalSchema.TransmitableDisplayName;

		// Token: 0x04004DE7 RID: 19943
		[Autoload]
		public static readonly StorePropertyDefinition RecipientOrder = InternalSchema.RecipientOrder;

		// Token: 0x04004DE8 RID: 19944
		public static readonly StorePropertyDefinition OriginatorRequestedAlternateRecipientEntryId = InternalSchema.OriginatorRequestedAlternateRecipientEntryId;

		// Token: 0x04004DE9 RID: 19945
		public static readonly StorePropertyDefinition RedirectionHistory = InternalSchema.RedirectionHistory;

		// Token: 0x04004DEA RID: 19946
		private static RecipientSchema instance = null;
	}
}
