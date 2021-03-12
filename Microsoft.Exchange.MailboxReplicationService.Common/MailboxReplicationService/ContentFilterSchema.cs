using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000ED RID: 237
	internal class ContentFilterSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000534 RID: 1332
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Sender = new ContentFilterSchema.ContentFilterPropertyDefinition("Sender", typeof(string), null, PropTag.SearchSender, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildRecipientRestriction));

		// Token: 0x04000535 RID: 1333
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition To = new ContentFilterSchema.ContentFilterPropertyDefinition("To", typeof(string), null, PropTag.SearchRecipientsTo, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildRecipientRestriction));

		// Token: 0x04000536 RID: 1334
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition BCC = new ContentFilterSchema.ContentFilterPropertyDefinition("BCC", typeof(string), null, PropTag.SearchRecipientsBcc, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildRecipientRestriction));

		// Token: 0x04000537 RID: 1335
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition CC = new ContentFilterSchema.ContentFilterPropertyDefinition("CC", typeof(string), null, PropTag.SearchRecipientsCc, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildRecipientRestriction));

		// Token: 0x04000538 RID: 1336
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Participants = new ContentFilterSchema.ContentFilterPropertyDefinition("Participants", typeof(string), null, PropTag.SearchRecipients, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildRecipientRestriction));

		// Token: 0x04000539 RID: 1337
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Subject = new ContentFilterSchema.ContentFilterPropertyDefinition("Subject", typeof(string), null, PropTag.Subject, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x0400053A RID: 1338
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Body = new ContentFilterSchema.ContentFilterPropertyDefinition("Body", typeof(string), null, PropTag.Body, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x0400053B RID: 1339
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Sent = new ContentFilterSchema.ContentFilterPropertyDefinition("Sent", typeof(DateTime), DateTime.MinValue, PropTag.ClientSubmitTime, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x0400053C RID: 1340
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Received = new ContentFilterSchema.ContentFilterPropertyDefinition("Received", typeof(DateTime), DateTime.MinValue, PropTag.MessageDeliveryTime, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x0400053D RID: 1341
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Attachment = new ContentFilterSchema.ContentFilterPropertyDefinition("Attachment", typeof(string), null, PropTag.SearchAttachments, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildAttachmentRestriction));

		// Token: 0x0400053E RID: 1342
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition MessageKind = new ContentFilterSchema.ContentFilterPropertyDefinition("MessageKind", typeof(MessageKindEnum), MessageKindEnum.Email, PropTag.MessageClass, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildMessageKindRestriction));

		// Token: 0x0400053F RID: 1343
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition PolicyTag = new ContentFilterSchema.ContentFilterPropertyDefinition("PolicyTag", typeof(string), null, PropTag.PolicyTag, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPolicyTagRestriction));

		// Token: 0x04000540 RID: 1344
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Expires = new ContentFilterSchema.ContentFilterPropertyDefinition("Expires", typeof(DateTime), DateTime.MinValue, PropTag.RetentionDate, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x04000541 RID: 1345
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition IsFlagged = new ContentFilterSchema.ContentFilterPropertyDefinition("IsFlagged", typeof(bool), false, (PropTag)277872643U, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildIsFlaggedRestriction));

		// Token: 0x04000542 RID: 1346
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition IsRead = new ContentFilterSchema.ContentFilterPropertyDefinition("IsRead", typeof(bool), false, PropTag.MessageFlags, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildIsReadRestriction));

		// Token: 0x04000543 RID: 1347
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Category = new ContentFilterSchema.ContentFilterPropertyDefinition("Category", typeof(string), null, new NamedPropData(WellKnownPropertySet.PublicStrings, "Keywords"), PropType.StringArray, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x04000544 RID: 1348
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Importance = new ContentFilterSchema.ContentFilterPropertyDefinition("Importance", typeof(ImportanceEnum), ImportanceEnum.Normal, PropTag.Importance, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x04000545 RID: 1349
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition Size = new ContentFilterSchema.ContentFilterPropertyDefinition("Size", typeof(ByteQuantifiedSize), ByteQuantifiedSize.MinValue, PropTag.MessageSize, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x04000546 RID: 1350
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition HasAttachment = new ContentFilterSchema.ContentFilterPropertyDefinition("HasAttachment", typeof(bool), false, PropTag.Hasattach, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x04000547 RID: 1351
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition All = new ContentFilterSchema.ContentFilterPropertyDefinition("All", typeof(string), null, PropTag.SearchAllIndexedProps, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x04000548 RID: 1352
		public static readonly ContentFilterSchema.ContentFilterPropertyDefinition MessageLocale = new ContentFilterSchema.ContentFilterPropertyDefinition("MessageLocale", typeof(CultureInfo), null, PropTag.LocaleId, new ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate(ContentFilterBuilder.BuildPropertyRestriction));

		// Token: 0x020000EE RID: 238
		public class ContentFilterPropertyDefinition : SimpleProviderPropertyDefinition
		{
			// Token: 0x06000902 RID: 2306 RVA: 0x00011FB0 File Offset: 0x000101B0
			public ContentFilterPropertyDefinition(string propertyName, Type propertyType, object defaultValue, PropTag propTagToSearch, ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate convertToRestriction) : base(propertyName, ExchangeObjectVersion.Exchange2010, propertyType, (defaultValue == null) ? PropertyDefinitionFlags.None : PropertyDefinitionFlags.PersistDefaultValue, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None)
			{
				this.propTagToSearch = propTagToSearch;
				this.namedPropToSearch = null;
				this.convertToRestriction = convertToRestriction;
			}

			// Token: 0x06000903 RID: 2307 RVA: 0x00011FEC File Offset: 0x000101EC
			public ContentFilterPropertyDefinition(string propertyName, Type propertyType, object defaultValue, NamedPropData namedPropToSearch, PropType namedPropType, ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate convertToRestriction) : base(propertyName, ExchangeObjectVersion.Exchange2010, propertyType, (defaultValue == null) ? PropertyDefinitionFlags.None : PropertyDefinitionFlags.PersistDefaultValue, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None)
			{
				this.propTagToSearch = PropTag.Null;
				this.namedPropToSearch = namedPropToSearch;
				this.namedPropType = namedPropType;
				this.convertToRestriction = convertToRestriction;
			}

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000904 RID: 2308 RVA: 0x00012038 File Offset: 0x00010238
			public PropTag PropTagToSearch
			{
				get
				{
					return this.propTagToSearch;
				}
			}

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000905 RID: 2309 RVA: 0x00012040 File Offset: 0x00010240
			public NamedPropData NamedPropToSearch
			{
				get
				{
					return this.namedPropToSearch;
				}
			}

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000906 RID: 2310 RVA: 0x00012048 File Offset: 0x00010248
			public PropType NamedPropType
			{
				get
				{
					return this.namedPropType;
				}
			}

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000907 RID: 2311 RVA: 0x00012050 File Offset: 0x00010250
			public ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate ConvertToRestriction
			{
				get
				{
					return this.convertToRestriction;
				}
			}

			// Token: 0x04000549 RID: 1353
			private PropTag propTagToSearch;

			// Token: 0x0400054A RID: 1354
			private NamedPropData namedPropToSearch;

			// Token: 0x0400054B RID: 1355
			private PropType namedPropType;

			// Token: 0x0400054C RID: 1356
			private ContentFilterSchema.ContentFilterPropertyDefinition.ConvertToRestrictionDelegate convertToRestriction;

			// Token: 0x020000EF RID: 239
			// (Invoke) Token: 0x06000909 RID: 2313
			public delegate Restriction ConvertToRestrictionDelegate(SinglePropertyFilter filter, IFilterBuilderHelper mapper);
		}
	}
}
