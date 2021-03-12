using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C9A RID: 3226
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ParticipantSchema : Schema
	{
		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x060070AE RID: 28846 RVA: 0x001F2F56 File Offset: 0x001F1156
		public new static ParticipantSchema Instance
		{
			get
			{
				if (ParticipantSchema.instance == null)
				{
					ParticipantSchema.instance = new ParticipantSchema();
				}
				return ParticipantSchema.instance;
			}
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x001F2F6E File Offset: 0x001F116E
		protected ParticipantSchema()
		{
		}

		// Token: 0x04004DBA RID: 19898
		public static readonly StorePropertyDefinition DisplayName = InternalSchema.EmailDisplayName;

		// Token: 0x04004DBB RID: 19899
		public static readonly StorePropertyDefinition SimpleDisplayName = InternalSchema.DisplayName7Bit;

		// Token: 0x04004DBC RID: 19900
		public static readonly StorePropertyDefinition EmailAddress = InternalSchema.EmailAddress;

		// Token: 0x04004DBD RID: 19901
		public static readonly StorePropertyDefinition RoutingType = InternalSchema.EmailRoutingType;

		// Token: 0x04004DBE RID: 19902
		public static readonly StorePropertyDefinition OriginalDisplayName = InternalSchema.EmailOriginalDisplayName;

		// Token: 0x04004DBF RID: 19903
		public static readonly StorePropertyDefinition EmailAddressForDisplay = InternalSchema.EmailAddressForDisplay;

		// Token: 0x04004DC0 RID: 19904
		public static readonly StorePropertyDefinition SmtpAddress = InternalSchema.SmtpAddress;

		// Token: 0x04004DC1 RID: 19905
		internal static readonly StorePropertyDefinition LegacyExchangeDN = InternalSchema.LegacyExchangeDN;

		// Token: 0x04004DC2 RID: 19906
		public static readonly StorePropertyDefinition OriginItemId = InternalSchema.ParticipantOriginItemId;

		// Token: 0x04004DC3 RID: 19907
		public static readonly StorePropertyDefinition IsDistributionList = InternalSchema.IsDistributionList;

		// Token: 0x04004DC4 RID: 19908
		public static readonly StorePropertyDefinition IsRoom = InternalSchema.IsRoom;

		// Token: 0x04004DC5 RID: 19909
		public static readonly StorePropertyDefinition IsResource = InternalSchema.IsResource;

		// Token: 0x04004DC6 RID: 19910
		public static readonly StorePropertyDefinition IsGroupMailbox = InternalSchema.IsGroupMailbox;

		// Token: 0x04004DC7 RID: 19911
		public static readonly StorePropertyDefinition IsMailboxUser = InternalSchema.IsMailboxUser;

		// Token: 0x04004DC8 RID: 19912
		[Autoload]
		public static readonly StorePropertyDefinition DisplayTypeEx = InternalSchema.DisplayTypeEx;

		// Token: 0x04004DC9 RID: 19913
		[Autoload]
		public static readonly StorePropertyDefinition DisplayType = InternalSchema.DisplayType;

		// Token: 0x04004DCA RID: 19914
		public static readonly StorePropertyDefinition Alias = InternalSchema.Alias;

		// Token: 0x04004DCB RID: 19915
		internal static readonly StorePropertyDefinition SendRichInfo = InternalSchema.SendRichInfo;

		// Token: 0x04004DCC RID: 19916
		internal static readonly StorePropertyDefinition SendInternetEncoding = InternalSchema.SendInternetEncoding;

		// Token: 0x04004DCD RID: 19917
		public static readonly StorePropertyDefinition SipUri = InternalSchema.SipUri;

		// Token: 0x04004DCE RID: 19918
		public static readonly StorePropertyDefinition ParticipantSID = InternalSchema.ParticipantSID;

		// Token: 0x04004DCF RID: 19919
		public static readonly StorePropertyDefinition ParticipantGuid = InternalSchema.ParticipantGuid;

		// Token: 0x04004DD0 RID: 19920
		private static ParticipantSchema instance = null;

		// Token: 0x04004DD1 RID: 19921
		public static ReadOnlyCollection<ADPropertyDefinition> SupportedADProperties = new ReadOnlyCollection<ADPropertyDefinition>(new ADPropertyDefinition[]
		{
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.PrimarySmtpAddress,
			ADObjectSchema.ObjectClass,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientTypeDetails,
			ADRecipientSchema.RecipientDisplayType,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.SimpleDisplayName,
			ADMailboxRecipientSchema.Sid,
			ADObjectSchema.Guid,
			ADRecipientSchema.Alias
		});
	}
}
